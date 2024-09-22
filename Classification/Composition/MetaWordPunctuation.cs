using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaWordPunctuation : MetaTermBase
    {
        private const string _emDash = "—";

        private string? _otherPunct;

        public PunctuationType PunctuationType { get; set; } = PunctuationType.DefaultUnknown;

        public MetaWordPunctuation(PunctuationType pt)
        {
            TermType = TermType.TPunctuation;
            PunctuationType = pt;
        }

        public MetaWordPunctuation(string value)
        {
            TermType = TermType.TPunctuation;

            if (value == "--" || value == _emDash)
            {
                PunctuationType = PunctuationType.Emdash;
            }
            else if (value == "?")
            {
                PunctuationType = PunctuationType.Question;
            }
            else if (value == "!")
            {
                PunctuationType = PunctuationType.Exclaim;
            }
            else if (value == ".")
            {
                PunctuationType = PunctuationType.Period;
            }
            else if (value == ",")
            {
                PunctuationType = PunctuationType.Comma;
            }
            else if (value == ":")
            {
                PunctuationType = PunctuationType.Colon;
            }
            else if (value == ";")
            {
                PunctuationType = PunctuationType.Semicolon;
            }
            else
            {
                PunctuationType = PunctuationType.Other;
                _otherPunct = value;
            }
        }

        public override string Value
        {
            get
            {
                switch (PunctuationType)
                {
                    case PunctuationType.Period: return ".";
                    case PunctuationType.Comma: return ",";
                    case PunctuationType.Colon: return ":";
                    case PunctuationType.Semicolon: return ";";
                    case PunctuationType.Emdash: return "--";
                    case PunctuationType.Question: return "?";
                    case PunctuationType.Exclaim: return "!";

                    case PunctuationType.Other: return _otherPunct!;
                }

                throw new NotImplementedException();
            }

            set
            { }
        }

        public override bool ValueSpaceBefore
        {
            get
            {
                switch (PunctuationType)
                {
                    case PunctuationType.Comma:
                    case PunctuationType.Colon:
                    case PunctuationType.Semicolon:
                    case PunctuationType.Emdash:
                    case PunctuationType.Period:
                    case PunctuationType.Exclaim:
                    case PunctuationType.Question:
                        return false;
                }

                return false;
            }

            set
            { }
        }

        public override bool ValueSpaceAfter
        {
            get
            {
                switch (PunctuationType)
                {
                    case PunctuationType.Emdash:
                        return false;

                    case PunctuationType.Period:
                    case PunctuationType.Comma:
                    case PunctuationType.Colon:
                    case PunctuationType.Semicolon:
                    case PunctuationType.Exclaim:
                    case PunctuationType.Question:
                        return true;
                }

                return false;
            }

            set
            { }
        }

        public bool IsFullStop()
        {
            switch (PunctuationType)
            {
                case PunctuationType.Comma:
                case PunctuationType.Colon:
                case PunctuationType.Semicolon:
                case PunctuationType.Emdash:
                    return false;
                
                case PunctuationType.Period:
                case PunctuationType.Exclaim:
                case PunctuationType.Question:
                    return true;
            }

            return false;
        }

        public bool IsSoftStop()
        {
            switch (PunctuationType)
            {
                case PunctuationType.Comma:
                case PunctuationType.Colon:
                case PunctuationType.Semicolon:
                case PunctuationType.Emdash:
                    return true;
                
                case PunctuationType.Period:
                case PunctuationType.Exclaim:
                case PunctuationType.Question:
                    return false;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return PunctuationType.GetHashCode();
        }

        public static bool operator ==(MetaWordPunctuation x, MetaWordPunctuation y)
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
                return x.PunctuationType == y.PunctuationType;
            }
        }

        public static bool operator !=(MetaWordPunctuation x, MetaWordPunctuation y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is MetaWordPunctuation other)
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
                    return this.PunctuationType == other.PunctuationType;
                }
            }

            return false;
        }

        public bool Equals(MetaWordPunctuation x, MetaWordPunctuation y)
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
                return x.PunctuationType == y.PunctuationType;
            }
        }

        public int GetHashCode([DisallowNull] MetaWordPunctuation obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(MetaWordPunctuation other)
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
                return this.PunctuationType == other.PunctuationType;
            }
        }
    }

}
