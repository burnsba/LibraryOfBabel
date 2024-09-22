using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Date
{
    internal class Token
    {
        private string _original;
        private int _value;
        private bool _hasValue = false;

        public string Original => _original;

        public PartType Part { get; set; }

        public int Value => _value;

        public bool HasValue => _hasValue;

        public bool MaybeDayOfMonth { get; set; }
        public bool MaybeYear { get; set; }
        public bool MaybeDecade { get; set; }
        public bool MaybeOrdinal { get; set; }
        public bool MaybeYearDesignate { get; set; }
        public int Index { get; set; }

        // zero is a flag this isn't used, index starts at 1
        public int YearDesignatePart { get; set; } = 0;
        public bool YearDesignateBc { get; set; } = false;

        public bool Consumed { get; set; } = false;

        public Token()
        {
        }

        public Token(string original)
        {
            _original = original;
        }

        public void SetValue(int value)
        {
            _value = value;
            _hasValue = true;
        }

        public override string ToString()
        {
            return _original;
        }

        public override int GetHashCode()
        {
            return Index.GetHashCode() ^ _original.GetHashCode();
        }

        public static bool operator ==(Token x, Token y)
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
                return x.Index == y.Index && x._original == y._original;
            }
        }

        public static bool operator !=(Token x, Token y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is Token other)
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
                    return this.Index == other.Index && this._original == other._original;
                }
            }

            return false;
        }

        public bool Equals(Token x, Token y)
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
                return x.Index == y.Index && x._original == y._original;
            }
        }

        public int GetHashCode([DisallowNull] Token obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(Token other)
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
                return this.Index == other.Index && this._original == other._original;
            }
        }
    }
}
