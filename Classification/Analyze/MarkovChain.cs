using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Analyze
{
    public class MarkovChain<T> : IEqualityComparer<MarkovChain<T>>, IEquatable<MarkovChain<T>>
    {
        private int _length = 0;
        public ReadOnlyCollection<T> Chain { get; private set; }
        public int Length => _length;

        public MarkovChain(IEnumerable<T> chain)
        {
            if (object.ReferenceEquals(null, chain))
            {
                throw new NullReferenceException();
            }

            if (chain.Count() < 1)
            {
                throw new ArgumentException("Markov chain requires entities.");
            }

            if (chain.Any(x => object.ReferenceEquals(null, x)))
            {
                throw new NullReferenceException("Markov chain contains null value");
            }

            Chain = chain.ToList().AsReadOnly();
            _length = Chain.Count;
        }

        public MarkovChain<T> Shift(T val)
        {
            var currentChain = Chain.ToList();
            currentChain.RemoveAt(0);
            currentChain.Add(val);

            return new MarkovChain<T>(currentChain);
        }

        public override string ToString()
        {
            return string.Join(", ", Chain.AsEnumerable());
        }

        public static bool operator ==(MarkovChain<T> x, MarkovChain<T> y)
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
                if (x.Length != y.Length)
                {
                    return false;
                }

                for (int i = 0; i < x.Length; i++)
                {
                    if (!x.Chain[i]!.Equals(y.Chain[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public static bool operator !=(MarkovChain<T> x, MarkovChain<T> y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is MarkovChain<T> other)
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
                    if (this.Length != other.Length)
                    {
                        return false;
                    }

                    for (int i = 0; i < this.Length; i++)
                    {
                        if (!this.Chain[i]!.Equals(other.Chain[i]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            return false;
        }

        public bool Equals(MarkovChain<T> x, MarkovChain<T> y)
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
                if (x.Length != y.Length)
                {
                    return false;
                }

                for (int i = 0; i < x.Length; i++)
                {
                    if (!x.Chain[i]!.Equals(y.Chain[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        public override int GetHashCode()
        {
            int hash = Length.GetHashCode();
            foreach (var x in Chain)
            {
                hash ^= x.GetHashCode();
            }

            return hash;
        }

        public int GetHashCode([DisallowNull] MarkovChain<T> obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(MarkovChain<T> other)
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
                if (this.Length != other.Length)
                {
                    return false;
                }

                for (int i = 0; i < this.Length; i++)
                {
                    if (!this.Chain[i]!.Equals(other.Chain[i]))
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }

}
