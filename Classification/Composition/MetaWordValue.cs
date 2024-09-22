using Classification.Analyze;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaWordValue : MetaTermBase
    {
        public WordType PartOfSpeech { get; set; } = WordType.DefaultUnknown;

        internal MetaWordValue(WordToken wt)
        {
            PartOfSpeech = wt.PartOfSpeech;
            TermType = wt.TermType;
            Value = wt.Value;
        }

        public override bool ValueSpaceBefore
        {
            get
            {
                switch (PartOfSpeech)
                {
                    case WordType.Particle:
                        if (Value.StartsWith("'"))
                        {
                            return false;
                        }

                        break;

                }

                return true;
            }

            set
            { }
        }

        public override bool ValueSpaceAfter
        {
            get
            {
                return true;
            }

            set
            { }
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 1;
        }

        public static bool operator ==(MetaWordValue x, MetaWordValue y)
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
                if (object.ReferenceEquals(null, x.Value) && object.ReferenceEquals(null, y.Value))
                {
                    return true;
                }
                else if (object.ReferenceEquals(null, x.Value) && !object.ReferenceEquals(null, y.Value))
                {
                    return false;
                }
                else if (!object.ReferenceEquals(null, x.Value) && object.ReferenceEquals(null, y.Value))
                {
                    return false;
                }

                return x.Value == y.Value;
            }
        }

        public static bool operator !=(MetaWordValue x, MetaWordValue y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is MetaWordValue other)
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
                    if (object.ReferenceEquals(null, this.Value) && object.ReferenceEquals(null, other.Value))
                    {
                        return true;
                    }
                    else if (object.ReferenceEquals(null, this.Value) && !object.ReferenceEquals(null, other.Value))
                    {
                        return false;
                    }
                    else if (!object.ReferenceEquals(null, this.Value) && object.ReferenceEquals(null, other.Value))
                    {
                        return false;
                    }

                    return this.Value == other.Value;
                }
            }

            return false;
        }

        public bool Equals(MetaWordValue x, MetaWordValue y)
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
                if (object.ReferenceEquals(null, x.Value) && object.ReferenceEquals(null, y.Value))
                {
                    return true;
                }
                else if (object.ReferenceEquals(null, x.Value) && !object.ReferenceEquals(null, y.Value))
                {
                    return false;
                }
                else if (!object.ReferenceEquals(null, x.Value) && object.ReferenceEquals(null, y.Value))
                {
                    return false;
                }

                return x.Value == y.Value;
            }
        }

        public int GetHashCode([DisallowNull] MetaWordValue obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(MetaWordValue other)
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
                if (object.ReferenceEquals(null, this.Value) && object.ReferenceEquals(null, other.Value))
                {
                    return true;
                }
                else if (object.ReferenceEquals(null, this.Value) && !object.ReferenceEquals(null, other.Value))
                {
                    return false;
                }
                else if (!object.ReferenceEquals(null, this.Value) && object.ReferenceEquals(null, other.Value))
                {
                    return false;
                }

                return this.Value == other.Value;
            }
        }
    }

}
