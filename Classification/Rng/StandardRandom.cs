using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Rng
{
    /// <summary>
    /// This is s simple XOR Shift RNG.
    /// Note that results do not follow a normal distribution.
    /// Implemented here to ensure results are deterministic across platforms ...
    /// </summary>
    public class StandardRandom
    {
        private UInt32 _seed = 0;

        public StandardRandom()
        {
            _seed = (UInt32)DateTime.UtcNow.Ticks;
        }

        public StandardRandom(UInt32 seed)
        {
            if (seed == 0)
            {
                throw new ArgumentException("Seed must be non-zero");
            }

            _seed = seed;
        }

        public StandardRandom(int seed)
        {
            if (seed == 0)
            {
                throw new ArgumentException("Seed must be non-zero");
            }

            _seed = (UInt32)seed;
        }

        public UInt32 Next()
        {
            UInt32 x = _seed;

            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;

            _seed = x;

            return x;
        }

        public UInt32 Next(int max)
        {
            UInt32 x = _seed;

            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;

            _seed = x;

            return x % (uint)max;
        }

        public UInt32 Next(int lower, int upper)
        {
            if (lower == upper)
            {
                throw new ArgumentException($"{nameof(lower)} cannot equal {nameof(upper)}");
            }

            if (upper < lower)
            {
                throw new ArgumentException($"{nameof(upper)} must be larger than {nameof(lower)}");
            }

            UInt32 x = _seed;

            x ^= x << 13;
            x ^= x >> 17;
            x ^= x << 5;

            _seed = x;

            int range = (upper - lower);

            return (uint)lower + (uint)(x % (uint)range);
        }

        public Single NextFloat()
        {
            UInt32 i = Next();
            Single result = (Single)i / (Single)(UInt32.MaxValue);
            return result;
        }
    }
}
