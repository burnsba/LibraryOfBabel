using Catalyst;
using Classification.Analyze;
using Classification.Extensions;
using Classification.Rng;
using Microsoft.ML.Transforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class TextBodyComposition
    {
        private const int PunctuationSpace = 10;
        private const int DefaultSentenceWordCount = 25;
        private const int ChanceRandomPunctation = 10;
        private const int ChanceSemiColon = 3;
        private const int ChanceEndSentence = 25;
        private const int ChanceEndParagraph = 2;

        private const int SingleYearOffset = 10; // half the range is negative. (shift by half)
        private const int RangeYearOffset = 20; // half the range is negative. (shift by half)
        private const int GeologicYearsAgoPercentOffset = 10;
        private const int GeologicYearsAgoRangePercentOffset = 20;

        // max precision digits (subctract one from this)
        private const int GeologicDatePrecisionRand = 3;
        // max precision digits (subctract one from this)
        private const int DistancePrecisionRand = 4;
        private const int TemperaturePrecisionRand = 2;

        private const int DistancePercentOffset = 20;
        private const int TemperaturePercentOffset = 20;

        private const int GenerateQuoteLength = 10;
        private const int GenerateParenLength = 10;

        private const int _quoteLenLower = 1;
        private const int _quoteLenUpper = 30;
        private const int _parenLenLower = 1;
        private const int _parenLenUpper = 30;

        private static readonly List<string> _continuationWords = new List<string>()
        {
            "a", "an", "the",
            "and",
            "as",
            "at",
            "by",
            "in",
            "its",
            "for",
            "my",
            "of",
            "or",
            "from",
            "to",
            "with",
        };

        private static readonly HashSet<WordType> _tokenChainAllowedPart = new HashSet<WordType>()
        {
            WordType.Adjective,
            WordType.Adposition,
            WordType.Adverb,
            WordType.Auxiliary,
            WordType.CoordinatingConjunction,
            WordType.Determiner,
            WordType.Interjection,
            WordType.Noun,
            WordType.Numeral,
            WordType.Particle,
            WordType.Pronoun,
            WordType.ProperNoun,
            WordType.SubordinatingConjunction,
            WordType.Verb,
        };

        private static readonly Dictionary<string, List<string>> _bannedSequence = new Dictionary<string, List<string>>()
        {
            { "the", new List<string>() { "of" } },
        };

        private StandardRandom _rand;

        public List<int> WordsPerParagraph { get; set; } = new List<int>();

        public double AverageWordsPerParagraph { get; set; }

        internal Dictionary<MarkovChain<WordType>, Dictionary<WordType, int>> MarkovChainByPart { get; set; } = new Dictionary<MarkovChain<WordType>, Dictionary<WordType, int>>();
        internal Dictionary<MarkovChain<WordToken>, List<WordToken>> MarkovChainByToken { get; set; } = new Dictionary<MarkovChain<WordToken>, List<WordToken>>();
        internal Dictionary<WordType, HashSet<WordToken>> TokenList { get; set; } = new Dictionary<WordType, HashSet<WordToken>>();

        public TextBodyComposition()
        {
            _rand = new StandardRandom();
        }

        public TextBodyComposition(int seed)
        {
            _rand = new StandardRandom(seed);
        }

        public void SetSeed(int seed)
        {
            _rand = new StandardRandom(seed);
        }

        public MetaPhrase GeneratePhrase(bool completeSentence, int maxSentenceWordCount)
        {
            var mpartKey = MarkovChainByPart.Keys.ToList()[(int)_rand.Next(MarkovChainByPart.Keys.Count)];
            MarkovChain<WordToken> mtokenKey = MarkovChainByToken.Keys.ToList()[(int)_rand.Next(MarkovChainByToken.Keys.Count)];

            //var sb = new StringBuilder();
            int nexactMatchCount = 0;

            int matchType1 = 0;
            int matchType2 = 0;
            int matchType3 = 0;
            int matchType4 = 0;

            int allowExactMatchChance = 2;
            bool inQuote = false;
            int runningQuoteLength = 0;
            bool inParen = false;
            int runningParenLength = 0;

            int activeQuoteLength = 0;
            int activeParenLength = 0;
            int sentenceLength = 0;

            MetaPhrase phrase = new MetaPhrase()
            {
                CompleteSentence = completeSentence,
            };

            bool endPunctuation = false;

            bool timeToQuit = false;
            bool safeEnd = false;
            WordToken? previousToken = null;
            string? previousWord = null;

            for (int i = 0; timeToQuit == false || safeEnd == false; i++)
            {
                WordToken nextToken = null;
                bool foundNextToken = false;

                if (i > maxSentenceWordCount)
                {
                    timeToQuit = true;
                }
                else
                {
                    timeToQuit = false;
                }

                int tryAgain = 10;

                for (; tryAgain > 0; tryAgain--)
                {
                    // If mtokenKey matches known chain, see if there's a random value that can be used.
                    if (MarkovChainByToken.ContainsKey(mtokenKey))
                    {
                        var tokenListMatchingNextPart = MarkovChainByToken[mtokenKey].ToList();

                        var count = tokenListMatchingNextPart.Count;
                        bool branch1 = false;

                        if (count == 1)
                        {
                            if (_rand.Next(allowExactMatchChance) == 0)
                            {
                                branch1 = true;
                            }
                        }
                        else if (count > 1)
                        {
                            branch1 = true;
                        }

                        if (branch1)
                        {
                            nextToken = tokenListMatchingNextPart[(int)_rand.Next(tokenListMatchingNextPart.Count())];

                            if (_tokenChainAllowedPart.Contains(nextToken.PartOfSpeech))
                            {
                                nexactMatchCount++;
                                matchType1++;

                                foundNextToken = true;
                            }
                        }
                    }

                    // Couldn't find above.
                    // Try to find a similar key that ends with the same token
                    if (foundNextToken == false)
                    {
                        var mtokenKeyChainLast = mtokenKey.Chain.Last();
                        var similarKeys = MarkovChainByToken.Keys.Where(x =>
                            x != mtokenKey
                            && x.Chain.Contains(mtokenKeyChainLast)).ToList();

                        if (similarKeys.Any())
                        {
                            // attempt to find an allowed part of speech
                            for (int j = 0; j < 5; j++)
                            {
                                var similarKey = similarKeys[(int)_rand.Next(similarKeys.Count)];
                                var listy = MarkovChainByToken[similarKey];

                                if (listy.Count > 0)
                                {
                                    nextToken = listy[(int)_rand.Next(listy.Count)];

                                    if (_tokenChainAllowedPart.Contains(nextToken.PartOfSpeech))
                                    {
                                        nexactMatchCount++;
                                        matchType2++;

                                        foundNextToken = true;

                                        break;
                                    }
                                }
                            }
                        }
                    }

                    // Couldn't find above.
                    // Extract the parts-of-speech from the mtokenKey, and check to see if that
                    // matches a known chain in MarkovChainByPart. If so, pick a random possible
                    // value from parts of speech, then pick a random token of that type.
                    if (foundNextToken == false)
                    {
                        var tokenChainParts = mtokenKey.Chain.Select(x => x.PartOfSpeech).ToList();
                        var tokenChainPartKey = new MarkovChain<WordType>(tokenChainParts);
                        if (MarkovChainByPart.ContainsKey(tokenChainPartKey))
                        {
                            var possible = MarkovChainByPart[mpartKey].Keys.ToList();
                            var nextPart = possible[(int)_rand.Next(possible.Count)];

                            var listy = TokenList[nextPart].ToList();

                            if (listy.Count > 0)
                            {
                                // attempt to find an allowed part of speech
                                for (int j = 0; j < 5; j++)
                                {
                                    nextToken = listy[(int)_rand.Next(listy.Count)];

                                    if (_tokenChainAllowedPart.Contains(nextToken.PartOfSpeech))
                                    {
                                        nexactMatchCount++;
                                        matchType3++;

                                        foundNextToken = true;

                                        break;
                                    }
                                }
                            }
                        }
                    }

                    // Couldn't find above.
                    // Fallback to pick any random allowed token.
                    // Also if the "tryAgain" counter failed too many times above.
                    if (tryAgain < 4 || foundNextToken == false)
                    {
                        // attempt to find an allowed part of speech
                        for (int j = 0; j < 5; j++)
                        {
                            nextToken = Classification.Extensions.CollectionRandom.RandDictValue(TokenList, _rand);

                            if (_tokenChainAllowedPart.Contains(nextToken.PartOfSpeech))
                            {
                                nexactMatchCount++;
                                matchType4++;

                                foundNextToken = true;

                                break;
                            }
                        }
                    }

                    if (foundNextToken == false)
                    {
                        throw new Exception("Could not find token to append");
                    }

                    bool doTheTryAgain = false;

                    // dont allow the phrase to start with "'" or "'s".
                    if (object.ReferenceEquals(null, previousToken) && nextToken!.PartOfSpeech == WordType.Particle && nextToken.Value.StartsWith("'"))
                    {
                        doTheTryAgain = true;
                    }
                    else if (!object.ReferenceEquals(null, previousToken))
                    {
                        if (_bannedSequence.ContainsKey(previousToken.Value))
                        {
                            var maybeBanned = _bannedSequence[previousToken.Value];

                            if (maybeBanned.Contains(nextToken!.Value))
                            {
                                doTheTryAgain = true;
                            }
                        }

                        if (nextToken!.PartOfSpeech == WordType.Particle && nextToken.Value.StartsWith("'"))
                        {
                            if (previousToken.PartOfSpeech == WordType.Noun
                                || previousToken.PartOfSpeech == WordType.ProperNoun
                                || previousToken.PartOfSpeech == WordType.ProperNoun)
                            {
                                // allowed
                            }
                            else
                            {
                                doTheTryAgain = true;
                            }
                        }
                    }
                    else if (previousWord == nextToken.Value)
                    {
                        // don't allow duplicates, except for adjectives or adverbs
                        if (previousToken.PartOfSpeech == nextToken.PartOfSpeech
                            && (
                                previousToken.PartOfSpeech == WordType.Adjective
                                || previousToken.PartOfSpeech == WordType.Adverb))
                        {
                            // allow
                        }
                        else
                        {
                            doTheTryAgain = true;
                        }
                    }

                    if (!doTheTryAgain)
                    {
                        break;
                    }
                }

                if (tryAgain <= 0)
                {
                    throw new Exception("Could not find token to append (repeat too many times)");
                }

                int commonPunctuationCount = 0;
                double puncPercent = 0;
                var checkChainParts = mtokenKey.Chain.Select(x => x.PartOfSpeech).ToList();
                var checkChainPartKey = new MarkovChain<WordType>(checkChainParts);
                if (MarkovChainByPart.ContainsKey(checkChainPartKey))
                {
                    var parent = MarkovChainByPart[checkChainPartKey];
                    if (parent.ContainsKey(WordType.Punctuation))
                    {
                        commonPunctuationCount = parent[WordType.Punctuation];
                    }

                    var allCount = parent.Sum(x => x.Value);
                    if (!parent.ContainsKey(WordType.Punctuation) || allCount < 2)
                    {
                        puncPercent = 0;
                    }
                    else
                    {
                        puncPercent = (double)parent[WordType.Punctuation] / (double)allCount;
                    }
                }

                // make visual studio happy.
                // Even though this will throw if (foundNextToken == false) above.
                if (object.ReferenceEquals(null, nextToken))
                {
                    throw new NullReferenceException();
                }

                var addedTerm = new MetaWordValue(nextToken);
                phrase.Words.Add(addedTerm);
                sentenceLength++;

                if (timeToQuit && !ExpectsFollowing(addedTerm))
                {
                    safeEnd = true;
                }
                else
                {
                    safeEnd = false;
                }

                bool closeQuote = false;
                bool closeParen = false;
                bool fullStop = false;
                bool chainStop = false;

                if (!_continuationWords.Contains(nextToken.Value)
                    && _rand.NextFloat() < puncPercent)
                {
                    bool ignore = false;
                    var randomPunct = CollectionRandom.RandHashSetValue(TokenList[WordType.Punctuation], _rand);

                    MetaTermBase? mtb = null;

                    if (randomPunct.Value == "\"")
                    {
                        if (inQuote)
                        {
                            closeQuote = true;
                            ignore = true;
                        }
                        else if (inParen)
                        {
                            ignore = true;
                        }
                        else
                        {
                            inQuote = true;

                            activeQuoteLength = (int)_rand.Next(_quoteLenLower, _quoteLenUpper);
                            mtb = new MetaWordQuote(QuoteType.OpenQuote);
                        }
                    }
                    else if (randomPunct.Value == "(")
                    {
                        if (inParen)
                        {
                            ignore = true;
                        }
                        else if (inQuote)
                        {
                            ignore = true;
                        }
                        else
                        {
                            inParen = true;

                            activeParenLength = (int)_rand.Next(_parenLenLower, _parenLenUpper);
                            mtb = new MetaWordParen(ParenType.OpenParen);
                        }
                    }
                    else if (randomPunct.Value == ")")
                    {
                        if (inParen)
                        {
                            closeParen = true;
                        }

                        ignore = true;
                    }

                    if (!ignore)
                    {
                        bool allowFullStop = true;

                        if (sentenceLength < 2)
                        {
                            if (nextToken.PartOfSpeech == WordType.Noun
                                || nextToken.PartOfSpeech == WordType.ProperNoun
                                || nextToken.PartOfSpeech == WordType.Verb
                                || nextToken.PartOfSpeech == WordType.Adverb)
                            {
                                // allow;
                            }
                            else
                            {
                                allowFullStop = false;
                            }
                        }

                        bool appendPunc = true;

                        if (object.ReferenceEquals(null, mtb))
                        {
                            MetaWordPunctuation mwp = new MetaWordPunctuation(randomPunct.Value);
                            mtb = mwp;

                            if (mwp.IsFullStop())
                            {
                                if (allowFullStop)
                                {
                                    fullStop = true;

                                    sentenceLength = 0;
                                }
                                else
                                {
                                    appendPunc = false;
                                }
                            }

                            if (mwp.IsSoftStop())
                            {
                                chainStop = true;
                            }
                        }

                        if (appendPunc)
                        {
                            if (object.ReferenceEquals(null, mtb))
                            {
                                throw new NullReferenceException();
                            }

                            phrase.Words.Add(mtb);
                        }
                    }
                }

                if (inQuote)
                {
                    if (runningQuoteLength > activeQuoteLength
                        || fullStop
                        || (timeToQuit & safeEnd))
                    {
                        closeQuote = true;
                    }
                }

                if (inParen)
                {
                    if (runningParenLength > activeParenLength
                        || fullStop
                        || (timeToQuit & safeEnd))
                    {
                        closeParen = true;
                    }
                }

                if (inQuote && closeParen)
                {
                    closeQuote = true;
                }

                if (closeQuote)
                {
                    phrase.TidyPunctuationForEndQuoteParen();
                    phrase.Words.Add(new MetaWordQuote(QuoteType.CloseQuote));
                    inQuote = false;
                    runningQuoteLength = 0;
                    activeQuoteLength = 0;
                }

                if (closeParen)
                {
                    phrase.TidyPunctuationForEndQuoteParen();
                    phrase.Words.Add(new MetaWordParen(ParenType.CloseParen));
                    inParen = false;
                    runningParenLength = 0;
                    activeParenLength = 0;
                }

                mtokenKey = mtokenKey.Shift(nextToken);

                // After a stop, don't continue the current chain.
                if (chainStop)
                {
                    mtokenKey = MarkovChainByToken.Keys.ToList()[(int)_rand.Next(MarkovChainByToken.Keys.Count)];
                }

                // This is only generating a single phrase, so a full stop means we're done.
                if (fullStop)
                {
                    break;
                }

                if (inQuote)
                {
                    runningQuoteLength++;
                }

                if (inParen)
                {
                    runningParenLength++;
                }

                previousToken = nextToken;

                if (nextToken.TermType == TermType.TWord)
                {
                    previousWord = nextToken.Value;
                }
            }

            if (phrase.CompleteSentence)
            {
                if (phrase.Words.Last() is MetaWordPunctuation)
                { }
                else
                {
                    endPunctuation = true;
                }

                if (endPunctuation)
                {
                    phrase.Words.Add(new MetaWordPunctuation(PunctuationType.Period));
                }

            }

            phrase.TidyPunctuation();

            return phrase;
        }

        public MetaParagraph GenerateParagraph(int maxSentenceWordCount)
        {
            int averageParagraphWordsHalf = (int)(AverageWordsPerParagraph / 2);

            MetaParagraph paragraph = new MetaParagraph();

            int paragraphWordLength = 0;

            while (true)
            {
                var sentence = GeneratePhrase(true, maxSentenceWordCount);
                paragraph.Sentences.Add(sentence);

                paragraphWordLength += sentence.GetWordCount();

                if (paragraphWordLength > averageParagraphWordsHalf && _rand.Next(ChanceEndParagraph) == 0)
                {
                    break;
                }
            }

            return paragraph;
        }

        public MetaTextBlock GenerateTextBlock(int maxWordCount, int maxSentenceWordCount)
        {
            var results = new MetaTextBlock();

            int totalWordCount = 0;

            while (true)
            {
                var paragraph = GenerateParagraph(maxSentenceWordCount);
                results.Paragraphs.Add(paragraph);

                totalWordCount += paragraph.GetWordCount();

                if (totalWordCount > maxWordCount)
                {
                    break;
                }
            }

            return results;
        }

        public void RemoveTokens(WordType wt, Func<WordToken, bool> filter)
        {
            if (TokenList.ContainsKey(wt))
            {
                var group = TokenList[wt];
                List<WordToken> toRemove = new List<WordToken>();
                foreach (var item in group)
                {
                    if (filter(item))
                    {
                        toRemove.Add(item);
                    }
                }

                foreach (var item in toRemove)
                {
                    group.Remove(item);
                }
            }
        }

        public string GenerateTextBlockString(int wordCount)
        {
            var results = GenerateTextBlock(wordCount, DefaultSentenceWordCount);

            return ResolveValue(results);
        }

        private string ResolveValue(MetaTextBlock mtb)
        {
            var sb = new StringBuilder();

            sb.Append(ResolveValue(mtb.Paragraphs[0]));

            for (int i = 1; i < mtb.Paragraphs.Count; i++)
            {
                var paragraph = mtb.Paragraphs[i];
                var p1 = ResolveValue(paragraph);

                sb.Append("\n\n");
                sb.Append(p1);
            }

            return sb.ToString();
        }

        private string ResolveValue(MetaParagraph mtb)
        {
            var sb = new StringBuilder();

            MetaTermBase last = mtb.Sentences[0].Words.Last();

            sb.Append(ResolveValue(mtb.Sentences[0]));

            for (int i = 1; i < mtb.Sentences.Count; i++)
            {
                var sentence = mtb.Sentences[i];
                var s1 = ResolveValue(sentence);

                if (last.ValueSpaceAfter)
                {
                    sb.Append(" ");
                }

                sb.Append(s1);

                last = sentence.Words.Last();
            }

            return sb.ToString();
        }

        private string ResolveValue(MetaPhrase mtb)
        {
            var sb = new StringBuilder();

            if (mtb.CompleteSentence)
            {
                sb.Append(ResolveValue(mtb.Words[0]).FirstCharToUpper());
            }
            else
            {
                sb.Append(ResolveValue(mtb.Words[0]));
            }

            MetaTermBase last = mtb.Words[0];

            for (int i = 1; i < mtb.Words.Count; i++)
            {
                var word = mtb.Words[i];
                var w1 = ResolveValue(word);

                if (last.ValueSpaceAfter && word.ValueSpaceBefore)
                {
                    sb.Append(" ");
                }

                sb.Append(w1);

                last = word;
            }

            return sb.ToString();
        }

        private string ResolveValue(MetaTermBase mtb)
        {
            if (mtb is MetaWordValue mwvalue)
            {
                if (mwvalue.PartOfSpeech == WordType.Pronoun && mwvalue.Value == "i")
                {
                    return mwvalue.Value.ToUpper();
                }

                return mwvalue.Value;
            }
            else if (mtb is MetaWordPunctuation mwpunc)
            {
                return mwpunc.Value;
            }
            else if (mtb is MetaWordProperNoun mwpnoun)
            {
                return mwpnoun.Value;
            }
            else if (mtb is MetaWordQuote mwquote)
            {
                return mwquote.Value;
            }
            else if (mtb is MetaWordParen mwparen)
            {
                return mwparen.Value;
            }

            throw new NotSupportedException();
        }

        private bool ExpectsFollowing(MetaTermBase mtb)
        {
            if (mtb is MetaWordValue mwvalue)
            {
                var lower = mwvalue.Value.ToLower();

                return _continuationWords.Contains(lower);
            }

            return false;
        }
    }
}
