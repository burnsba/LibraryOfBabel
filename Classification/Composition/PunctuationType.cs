using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public enum PunctuationType
    {
        DefaultUnknown,

        Period,
        Question,
        Exclaim,
        Comma,
        Colon,
        Semicolon,
        Emdash,

        Footnote,

        Other,
    }
}
