using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaPhrase
    {
        public List<MetaTermBase> Words { get; set; } = new List<MetaTermBase>();

        public bool CompleteSentence { get; set; } = true;

        public MetaPhrase() { }

        public MetaPhrase(List<MetaTermBase> phrase)
        {
            foreach (var x in phrase)
            {
                Words.Add(x);
            }
        }

        internal void TidyPunctuationForEndQuoteParen()
        {
            if (Words.Count < 3)
            {
                return;
            }

            var len = Words.Count;

            var last1 = Words[len - 1];

            if (last1 is MetaWordPunctuation)
            {
                var newWordList = Words.Take(len - 1).ToList();

                Words = newWordList;
            }
        }

        internal void TidyPunctuation()
        {
            if (Words.Count < 4)
            {
                return;
            }

            var len = Words.Count;

            var last1 = Words[len - 1];
            var last2 = Words[len - 2];
            var last3 = Words[len - 3];

            // For phrases that end like:
            //     still (lured him on and we will?).
            // Move the last punctuation in the paren to the end of the sentence.
            if (last1 is MetaWordPunctuation
                && last2 is MetaWordParen
                && last3 is MetaWordPunctuation)
            {
                var mwp1 = last1 as MetaWordPunctuation;
                var mwp3 = last3 as MetaWordPunctuation;

                if (mwp1!.IsFullStop() && mwp3!.IsFullStop())
                {
                    var newWordList = Words.Take(len - 3).ToList();
                    newWordList.Add(last2);
                    newWordList.Add(last3);

                    Words = newWordList;
                }
            }

            // For phrases that end like:
            //     all "right placed her mad way blocked:".
            // Drop the last punctuation at the end of the quote.
            // Same if inside paren.
            if (last1 is MetaWordPunctuation
                && (
                    last2 is MetaWordQuote
                    || last2 is MetaWordParen)
                && last3 is MetaWordPunctuation)
            {
                var mwp1 = last1 as MetaWordPunctuation;

                if (mwp1!.IsFullStop())
                {
                    var newWordList = Words.Take(len - 3).ToList();
                    newWordList.Add(last2);
                    newWordList.Add(last1);

                    Words = newWordList;
                }
            }
        }

        public int GetWordCount() => Words.Count;
    }
}
