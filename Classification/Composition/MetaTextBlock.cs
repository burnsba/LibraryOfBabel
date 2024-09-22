using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaTextBlock
    {
        public List<MetaParagraph> Paragraphs { get; set; } = new List<MetaParagraph>();

        public int GetWordCount() => Paragraphs.Sum(x => x.GetWordCount());
    }
}
