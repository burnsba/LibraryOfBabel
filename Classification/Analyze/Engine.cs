using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using Catalyst;
using Classification.Composition;
using Classification.Extensions;
using Classification.Lang;
using Mosaik.Core;
using Newtonsoft.Json.Linq;
using UnitsNet;
using UnitsNet.Units;

namespace Classification.Analyze
{
    public class Engine
    {
        private static Regex _alphaNum = new Regex("\\p{L}|\\p{N}");

        // incoming text may not be correctly split on the single emdash character
        // if there is not adjacent whitespace.
        // Double hyphen should already be handled correctly.
        private static Regex _seperateEmDashRegex = new Regex("(\\p{L})" + Symbol.EmDash1 + "(\\p{L})");
        private static string _seperateEmDashReplace = "$1 " + Symbol.EmDash2 + " $2";
    
        private static readonly HashSet<PartOfSpeech> _tokenChainAllowedPart = new HashSet<PartOfSpeech>()
        {
            PartOfSpeech.ADJ,
            PartOfSpeech.ADP,
            PartOfSpeech.ADV,
            PartOfSpeech.AUX,
            PartOfSpeech.CCONJ,
            PartOfSpeech.DET,
            PartOfSpeech.INTJ,
            PartOfSpeech.NOUN,
            PartOfSpeech.NUM,
            PartOfSpeech.PART,
            PartOfSpeech.PRON,
            PartOfSpeech.PROPN,
            PartOfSpeech.SCONJ,
            PartOfSpeech.VERB,
        };

        private int _wordChainLength = 2;
        private int _partChainLength = 2;

        private bool _hasEngineContent = false;

        private TextBodyComposition? _composition;

        public Engine() { }

        public Engine(int wordChainLength, int partChainLength)
        {
            _wordChainLength = wordChainLength;
            _partChainLength = partChainLength;
        }

        public async Task Process(string text)
        {
            //You need to pre-register each language (and install the respective NuGet Packages)
            Catalyst.Models.English.Register();
            Storage.Current = new DiskStorage("catalyst-models");

            var nlp = await Pipeline.ForAsync(Language.English);

            var preprared = PrepareTextForProcessing(text);
            var doc = new Document(preprared, Language.English);

            nlp.ProcessSingle(doc);

            _composition = new TextBodyComposition();

            var sentencePartCounts = new Dictionary<WordType, List<int>>();

            var allWordTypes = Enum.GetValues(typeof(WordType)).Cast<WordType>();
            foreach (var wt in allWordTypes)
            {
                _composition.TokenList.Add(wt, new HashSet<WordToken>());

                sentencePartCounts.Add(wt, new List<int>());
            }

            Queue<WordToken> tokenQueue = new Queue<WordToken>();
            Queue<PartOfSpeech> partQueue = new Queue<PartOfSpeech>();

            bool ignoreSquare = false;

            List<MetaPhrase> sentences = new List<MetaPhrase>();
            MetaPhrase currentSentence = new MetaPhrase();
            bool trackSentences = false;
            List<MetaWordValue> pendingConjunctions = new List<MetaWordValue>();

            foreach (var tg in doc.TokensData)
            {
                foreach (var t in tg)
                {
                    var len = 1 + t.UpperBound - t.LowerBound;
                    var orig = doc.Value.Substring(t.LowerBound, len);

                    var wordType = PartOfSpeechToWordType(t.Tag);

                    // Ignore everything between square brackets.
                    // This will remove editor notes and wikipedia sources.
                    if (ignoreSquare)
                    {
                        if (orig == "]")
                        {
                            ignoreSquare = false;
                        }

                        continue;
                    }

                    if (orig == "[")
                    {
                        ignoreSquare = true;
                        continue;
                    }

                    // some odd cases
                    // Ignore PUNCT tokens that contain alpha numeric text
                    if (wordType == WordType.Punctuation)
                    {
                        if (_alphaNum.IsMatch(orig))
                        {
                            continue;
                        }
                    }

                    var lower = orig.ToLower();
                    string normalizedCase = lower;
                    if (wordType == WordType.ProperNoun && lower.Length > 1)
                    {
                        normalizedCase = lower.FirstCharToUpper();
                    }

                    if (trackSentences)
                    {
                        if (wordType == WordType.Punctuation)
                        {
                            if (orig == ";" || orig == "." || orig == "?" || orig == "!")
                            {
                                foreach (var conj in pendingConjunctions)
                                {
                                    currentSentence.Words.Add(conj);
                                }
                                pendingConjunctions.Clear();
                                currentSentence.Words.Add(new MetaWordPunctuation(orig));
                                sentences.Add(currentSentence);
                                currentSentence = new MetaPhrase();
                            }
                        }
                        else if (wordType == WordType.CoordinatingConjunction)
                        {
                            // Coordinating conjunctions can cause a sentence to ramble on and on, split on the
                            // conjunction to accurately track the stats for each "phrase." But keep track of the word,
                            // just stick them on the end of the sentence to get an accuracte count for total
                            // conjunctions.
                            var conj = new MetaWordValue(new WordToken(normalizedCase, wordType, TermType.TWord));
                            pendingConjunctions.Add(conj);
                            sentences.Add(currentSentence);
                            currentSentence = new MetaPhrase();
                        }
                        else
                        {
                            currentSentence.Words.Add(new MetaWordValue(new WordToken(normalizedCase, wordType, TermType.TWord)));
                        }
                    }

                    // Sometimes a single token contains an emdash; this should be two words.
                    // em-dash, not minus sign.
                    if (normalizedCase.Contains(Symbol.EmDash1) && wordType != WordType.Punctuation)
                    {
                        var splits = normalizedCase.Split(Symbol.EmDash1, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var s in splits.Where(x => x.Length > 1))
                        {
                            var wt = new WordToken(s, wordType, TermType.TWord);
                            _composition.TokenList[wordType].Add(wt);

                            if (_tokenChainAllowedPart.Contains(t.Tag))
                            {
                                tokenQueue.Enqueue(wt);
                            }
                        }
                    }
                    else
                    {
                        var wt = new WordToken(normalizedCase, wordType, TermType.TWord);
                        _composition.TokenList[wordType].Add(wt);

                        if (_tokenChainAllowedPart.Contains(t.Tag))
                        {
                            tokenQueue.Enqueue(wt);
                        }
                    }

                    partQueue.Enqueue(t.Tag);

                    while (tokenQueue.Count > _wordChainLength)
                    {
                        var chain = tokenQueue.ToList().Take(_wordChainLength + 1).ToList();

                        var last = chain.Last();
                        chain.RemoveAt(chain.Count - 1);

                        var key = new MarkovChain<WordToken>(chain);

                        if (_composition.MarkovChainByToken.ContainsKey(key))
                        {
                            _composition.MarkovChainByToken[key].Add(last);
                        }
                        else
                        {
                            _composition.MarkovChainByToken.Add(key, new List<WordToken> { last });
                        }

                        tokenQueue.Dequeue();
                    }

                    while (partQueue.Count > _partChainLength + 1)
                    {
                        var chain = partQueue.Select(PartOfSpeechToWordType).ToList().Take(_partChainLength + 1).ToList();

                        var last = chain.Last();
                        chain.RemoveAt(chain.Count - 1);

                        var key = new MarkovChain<WordType>(chain);

                        if (_composition.MarkovChainByPart.ContainsKey(key))
                        {
                            var dict = _composition.MarkovChainByPart[key];
                            if (dict.ContainsKey(last))
                            {
                                dict[last]++;
                            }
                            else
                            {
                                dict.Add(last, 1);
                            }
                        }
                        else
                        {
                            var dict = new Dictionary<WordType, int>();
                            dict.Add(last, 1);

                            _composition.MarkovChainByPart.Add(key, dict);
                        }

                        partQueue.Dequeue();
                    }

                }
            }

            // misc cleanup

            _composition.RemoveTokens(WordType.Punctuation, wt =>
            {
                if (wt.Value.Contains("[") || wt.Value.Contains("]"))
                {
                    return true;
                }
                else if (wt.Value.Contains("{") || wt.Value.Contains("}"))
                {
                    return true;
                }
                else if (wt.Value == Symbol.EmDash1)
                {
                    return true;
                }

                return false;
            });

            // parsing/reading done.

            if (trackSentences)
            {
                foreach (var sent in sentences)
                {
                    foreach (var wt in allWordTypes)
                    {
                        int count = sent.Words.Where(x => x is MetaWordValue).Cast<MetaWordValue>().Count(x => x.PartOfSpeech == wt);

                        sentencePartCounts[wt].Add(count);
                    }
                }

                foreach (var wt in allWordTypes)
                {
                    var stat = new Stat<int>();

                    stat.Min = sentencePartCounts[wt].Min();
                    stat.Max = sentencePartCounts[wt].Max();
                    stat.Average = sentencePartCounts[wt].Average();
                    _composition.AverageSentencePartCount[wt] = stat;
                } 
            }

            _hasEngineContent = true;
        }

        public TextBodyComposition GetComposition()
        {
            if (!_hasEngineContent)
            {
                throw new Exception("No engine content yet");
            }

            return _composition!;
        }

        private string PrepareTextForProcessing(string text)
        {
            string cleaned = text;

            cleaned = _seperateEmDashRegex.Replace(text, _seperateEmDashReplace);

            cleaned = cleaned.Replace("“", "\""); // styled open quote with double quote
            cleaned = cleaned.Replace("”", "\""); // styled close quote with double quote
            cleaned = cleaned.Replace("‘", "'"); // styled single open quote with single quote
            cleaned = cleaned.Replace("’", "'"); // styled single close quote with single quote

            return cleaned;
        }

        private static WordType PartOfSpeechToWordType(PartOfSpeech part)
        {
            switch (part)
            {
                case PartOfSpeech.NONE: return WordType.DefaultUnknown;
                case PartOfSpeech.ADJ: return WordType.Adjective;
                case PartOfSpeech.ADP: return WordType.Adposition;
                case PartOfSpeech.ADV: return WordType.Adverb;
                case PartOfSpeech.AUX: return WordType.Auxiliary;
                case PartOfSpeech.CCONJ: return WordType.CoordinatingConjunction;
                case PartOfSpeech.DET: return WordType.Determiner;
                case PartOfSpeech.INTJ: return WordType.Interjection;
                case PartOfSpeech.NOUN: return WordType.Noun;
                case PartOfSpeech.NUM: return WordType.Numeral;
                case PartOfSpeech.PART: return WordType.Particle;
                case PartOfSpeech.PRON: return WordType.Pronoun;
                case PartOfSpeech.PROPN: return WordType.ProperNoun;
                case PartOfSpeech.PUNCT: return WordType.Punctuation;
                case PartOfSpeech.SCONJ: return WordType.SubordinatingConjunction;
                case PartOfSpeech.SYM: return WordType.Symbol;
                case PartOfSpeech.VERB: return WordType.Verb;
                case PartOfSpeech.X: return WordType.Other;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}
