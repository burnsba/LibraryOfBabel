using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classification.Rng;

namespace BabelTest
{
    public class RandTests
    {
        [Fact]
        public void RandTest1()
        {
            var rand = new StandardRandom(1);

            UInt32 n = rand.Next();

            Assert.Equal((UInt32)270369, n);
        }

        [Fact]
        public void RandTestAll1s()
        {
            var rand = new StandardRandom((UInt32)0xffffffff);

            UInt32 n = rand.Next();

            Assert.Equal((UInt32)253983, n);
        }

        [Fact]
        public void RandTestAllLoop()
        {
            var rand = new StandardRandom((int)0x1234);

            UInt32 n = 0;
            
            for (int i = 0; i < 1000; i++)
            {
                n = rand.Next();
            }

            Assert.Equal((UInt32)568839046, n);
        }

        [Fact]
        public void RandTestFloat1()
        {
            var rand = new StandardRandom(1);

            Single f = rand.NextFloat();

            Assert.Equal((Single)6.29501883E-05f, f);
        }

        [Fact]
        public void RandTestFloatAll1s()
        {
            var rand = new StandardRandom((UInt32)0xffffffff);

            Single f = rand.NextFloat();

            Assert.Equal((Single)5.91350254E-05f, f);
        }

        [Fact]
        public void RandTestFloatAllLoop()
        {
            var rand = new StandardRandom((int)0x1234);

            Single f = 0.0f;

            for (int i = 0; i < 1000; i++)
            {
                f = rand.NextFloat();
            }

            Assert.Equal((Single)0.13244316f, f);
        }
    }
}
