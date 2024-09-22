using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaTermBase
    {
        public TermType TermType { get; set; } = TermType.TWord;

        public virtual string Value { get; set; }

        public virtual bool ValueSpaceBefore { get; set; } = true;
        public virtual bool ValueSpaceAfter { get; set; } = true;

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
