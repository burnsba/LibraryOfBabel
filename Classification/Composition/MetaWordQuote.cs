using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaWordQuote : MetaTermBase
    {
        public QuoteType QuoteType { get; set; } = QuoteType.DefaultUnknown;

        public MetaWordQuote(QuoteType qt)
        {
            TermType = TermType.TQuote;
            QuoteType = qt;

            Value = "\"";
        }

        public MetaWordQuote()
        {
            TermType = TermType.TQuote;

            Value = "\"";
        }

        public override bool ValueSpaceBefore
        {
            get
            {
                switch (QuoteType)
                {
                    case QuoteType.OpenQuote: return true;
                    case QuoteType.CloseQuote: return false;
                }

                throw new NotSupportedException();
            }

            set
            { }
        }

        public override bool ValueSpaceAfter
        {
            get
            {
                switch (QuoteType)
                {
                    case QuoteType.OpenQuote: return false;
                    case QuoteType.CloseQuote: return true;
                }

                throw new NotSupportedException();
            }

            set
            { }
        }

        public override int GetHashCode()
        {
            return QuoteType.GetHashCode();
        }

        public static bool operator ==(MetaWordQuote x, MetaWordQuote y)
        {
            if (object.ReferenceEquals(null, x) && object.ReferenceEquals(null, y))
            {
                return true;
            }
            else if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            else if (object.ReferenceEquals(null, x) && !object.ReferenceEquals(null, y))
            {
                return false;
            }
            else if (!object.ReferenceEquals(null, x) && object.ReferenceEquals(null, y))
            {
                return false;
            }
            else
            {
                return x.QuoteType == y.QuoteType;
            }
        }

        public static bool operator !=(MetaWordQuote x, MetaWordQuote y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is MetaWordQuote other)
            {
                if (object.ReferenceEquals(null, this) && object.ReferenceEquals(null, other))
                {
                    return true;
                }
                else if (object.ReferenceEquals(other, this))
                {
                    return true;
                }
                else if (object.ReferenceEquals(null, this) && !object.ReferenceEquals(null, other))
                {
                    return false;
                }
                else if (!object.ReferenceEquals(null, this) && object.ReferenceEquals(null, other))
                {
                    return false;
                }
                else
                {
                    return this.QuoteType == other.QuoteType;
                }
            }

            return false;
        }

        public bool Equals(MetaWordQuote x, MetaWordQuote y)
        {
            if (object.ReferenceEquals(null, x) && object.ReferenceEquals(null, y))
            {
                return true;
            }
            else if (object.ReferenceEquals(x, y))
            {
                return true;
            }
            else if (object.ReferenceEquals(null, x) && !object.ReferenceEquals(null, y))
            {
                return false;
            }
            else if (!object.ReferenceEquals(null, x) && object.ReferenceEquals(null, y))
            {
                return false;
            }
            else
            {
                return x.QuoteType == y.QuoteType;
            }
        }

        public int GetHashCode([DisallowNull] MetaWordQuote obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(MetaWordQuote other)
        {
            if (object.ReferenceEquals(null, this) && object.ReferenceEquals(null, other))
            {
                return true;
            }
            else if (object.ReferenceEquals(other, this))
            {
                return true;
            }
            else if (object.ReferenceEquals(null, this) && !object.ReferenceEquals(null, other))
            {
                return false;
            }
            else if (!object.ReferenceEquals(null, this) && object.ReferenceEquals(null, other))
            {
                return false;
            }
            else
            {
                return this.QuoteType == other.QuoteType;
            }
        }
    }

}
