using Classification.Date;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaWordParen : MetaTermBase
    {
        public ParenType ParenType { get; set; } = ParenType.DefaultUnknown;

        public MetaWordParen(ParenType pt)
        {
            TermType = TermType.TParen;
            ParenType = pt;
        }

        public MetaWordParen()
        {
            TermType = TermType.TParen;
        }

        public override string Value
        {
            get
            {
                switch (ParenType)
                {
                    case ParenType.OpenParen: return "(";
                    case ParenType.CloseParen: return ")";
                }

                throw new NotSupportedException();
            }

            set
            { }
        }

        public override bool ValueSpaceBefore
        {
            get
            {
                switch (ParenType)
                {
                    case ParenType.OpenParen: return true;
                    case ParenType.CloseParen: return false;
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
                switch (ParenType)
                {
                    case ParenType.OpenParen: return false;
                    case ParenType.CloseParen: return true;
                }

                throw new NotSupportedException();
            }

            set
            { }
        }

        public override int GetHashCode()
        {
            return ParenType.GetHashCode();
        }

        public static bool operator ==(MetaWordParen x, MetaWordParen y)
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
                return x.ParenType == y.ParenType;
            }
        }

        public static bool operator !=(MetaWordParen x, MetaWordParen y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is MetaWordParen other)
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
                    return this.ParenType == other.ParenType;
                }
            }

            return false;
        }

        public bool Equals(MetaWordParen x, MetaWordParen y)
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
                return x.ParenType == y.ParenType;
            }
        }

        public int GetHashCode([DisallowNull] MetaWordParen obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(MetaWordParen other)
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
                return this.ParenType == other.ParenType;
            }
        }
    }

}
