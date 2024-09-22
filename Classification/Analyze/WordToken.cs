using Catalyst;
using Classification.Composition;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Analyze
{
    public class WordToken
    {
        public string Value { get; set; }
        public TermType TermType { get; set; } = TermType.DefaultUnknown;
        public WordType PartOfSpeech { get; set; } = WordType.DefaultUnknown;

        public WordToken()
        {
        }

        public WordToken(string value, WordType wt, TermType tt)
        {
            Value = value;
            TermType = tt;
            PartOfSpeech = wt;
        }

        public override string ToString()
        {
            return Value;
        }

        public static bool operator ==(WordToken x, WordToken y)
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
                return x.Value == y.Value;
            }
        }

        public static bool operator !=(WordToken x, WordToken y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is WordToken other)
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
                    return this.Value == other.Value;
                }
            }

            return false;
        }

        public bool Equals(WordToken x, WordToken y)
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
                return x.Value == y.Value;
            }
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 7;
        }

        public int GetHashCode([DisallowNull] WordToken obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(WordToken other)
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
                return this.Value == other.Value;
            }
        }
    }
}
