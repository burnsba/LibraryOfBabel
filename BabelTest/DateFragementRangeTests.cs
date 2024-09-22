using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classification.Date;

namespace BabelTest
{
    public class DateFragementRangeTests
    {
        [InlineData("january 14 - 16", 14, 0, null, null, null, 16, 0, null, null, null)]
        [InlineData("january 14 to 16", 14, 0, null, null, null, 16, 0, null, null, null)]
        [InlineData("14th january to 16th of january", 14, 0, null, null, null, 16, 0, null, null, null)]
        [InlineData("14th january - 16th of january", 14, 0, null, null, null, 16, 0, null, null, null)]
        [InlineData("the 14th - 16th of january", 14, 0, null, null, null, 16, 0, null, null, null)]
        [InlineData("the 14th to 16th of january", 14, 0, null, null, null, 16, 0, null, null, null)]
        [InlineData("the 14th through 16th of january", 14, 0, null, null, null, 16, 0, null, null, null)]
        [InlineData("jan - march", null, 0, null, null, null, null, 2, null, null, null)]
        [InlineData("january to march", null, 0, null, null, null, null, 2, null, null, null)]
        [InlineData("march - july 1950", null, 2, 1950, null, null, null, 6, 1950, null, null)]
        [InlineData("jan through feb 1950", null, 0, 1950, null, null, null, 1, 1950, null, null)]
        [InlineData("the 1950s-1960s", null, null, null, 1950, null, null, null, null, 1960, null)]
        [InlineData("the 1950s-60s", null, null, null, 1950, null, null, null, null, 1960, null)]
        [InlineData("the 1950s - 1960s", null, null, null, 1950, null, null, null, null, 1960, null)]
        [InlineData("the 1950s - 60s", null, null, null, 1950, null, null, null, null, 1960, null)]
        [InlineData("the 14th to 20th centuries", null, null, null, null, 14, null, null, null, null, 20)]
        [Theory()]
        public void ValidMonthTests(
            string text,
            int? leftDay,
            int? leftMonth,
            int? leftYear,
            int? leftDecade,
            int? leftCentury,
            int? rightDay,
            int? rightMonth,
            int? rightYear,
            int? rightDecade,
            int? rightCentury)
        {
            DateFragementRange dfr;

            dfr = DateFragementRange.Parse(text);

            Assert.NotNull(dfr.Start);
            Assert.NotNull(dfr.End);

            if (leftDay.HasValue)
            {
                Assert.Equal(leftDay.Value, dfr.Start.DayOfMonth);
            }
            else
            {
                Assert.False(dfr.Start.HasDay);
            }

            if (leftMonth.HasValue)
            {
                Assert.Equal(leftMonth.Value, dfr.Start.Month);
            }
            else
            {
                Assert.False(dfr.Start.HasMonth);
            }

            if (leftYear.HasValue)
            {
                Assert.Equal(leftYear.Value, dfr.Start.Year);
            }
            else
            {
                Assert.False(dfr.Start.HasYear);
            }

            if (leftDecade.HasValue)
            {
                Assert.Equal(leftDecade.Value, dfr.Start.Decade);
            }
            else
            {
                Assert.False(dfr.Start.HasDecade);
            }

            if (leftCentury.HasValue)
            {
                Assert.Equal(leftCentury.Value, dfr.Start.Century);
            }
            else
            {
                Assert.False(dfr.Start.HasCentury);
            }

            if (rightDay.HasValue)
            {
                Assert.Equal(rightDay.Value, dfr.End.DayOfMonth);
            }
            else
            {
                Assert.False(dfr.End.HasDay);
            }

            if (rightMonth.HasValue)
            {
                Assert.Equal(rightMonth.Value, dfr.End.Month);
            }
            else
            {
                Assert.False(dfr.End.HasMonth);
            }

            if (rightYear.HasValue)
            {
                Assert.Equal(rightYear.Value, dfr.End.Year);
            }
            else
            {
                Assert.False(dfr.End.HasYear);
            }

            if (rightDecade.HasValue)
            {
                Assert.Equal(rightDecade.Value, dfr.End.Decade);
            }
            else
            {
                Assert.False(dfr.End.HasDecade);
            }

            if (rightCentury.HasValue)
            {
                Assert.Equal(rightCentury.Value, dfr.End.Century);
            }
            else
            {
                Assert.False(dfr.End.HasCentury);
            }
        }
    }
}
