using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaParagraph
    {
        public List<MetaPhrase> Sentences { get; set; } = new List<MetaPhrase>();

        public List<MetaPhrase> Footnotes { get; set; } = new List<MetaPhrase>();

        public int GetWordCount() => Sentences.Sum(x => x.GetWordCount());
    }
}
