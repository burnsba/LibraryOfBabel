using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Composition
{
    public class MetaWordDate : MetaTermBase
    {
        public int DateType => throw new NotImplementedException();

        public MetaWordDate()
        {
            TermType = TermType.TDate;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool operator ==(MetaWordDate x, MetaWordDate y)
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
                return x.DateType == y.DateType;
            }
        }

        public static bool operator !=(MetaWordDate x, MetaWordDate y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is MetaWordDate other)
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
                    return this.DateType == other.DateType;
                }
            }

            return false;
        }

        public bool Equals(MetaWordDate x, MetaWordDate y)
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
                return x.DateType == y.DateType;
            }
        }

        public int GetHashCode([DisallowNull] MetaWordDate obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(MetaWordDate other)
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
                return this.DateType == other.DateType;
            }
        }
    }

}
