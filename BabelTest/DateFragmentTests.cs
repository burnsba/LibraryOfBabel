using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classification.Date;

namespace BabelTest
{
    public class DateFragmentTests
    {
        [InlineData("the first", 1)]
        [InlineData("the 1st", 1)]
        [InlineData("the 1'st", 1)]
        [InlineData("the 17th", 17)]
        [InlineData("the 20th", 20)]
        [InlineData("the 20'th", 20)]
        [InlineData("the twentieth", 20)]
        [InlineData("the 21st", 21)]
        [InlineData("the 21'st", 21)]
        [InlineData("the twenty first", 21)]
        [InlineData("the twenty-first", 21)]
        [Theory()]
        public void ValidDayTests(string text, int day)
        {
            DateFragment df;

            df = DateFragment.Parse(text);
            Assert.True(df.HasDay);
            Assert.Equal(day, df.DayOfMonth);
            Assert.False(df.HasMonth);
            Assert.False(df.HasYear);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("january", 0)]
        [InlineData("february", 1)]
        [InlineData("march", 2)]
        [InlineData("april", 3)]
        [InlineData("may", 4)]
        [InlineData("june", 5)]
        [InlineData("july", 6)]
        [InlineData("august", 7)]
        [InlineData("september", 8)]
        [InlineData("october", 9)]
        [InlineData("november", 10)]
        [InlineData("december", 11)]
        [Theory()]
        public void ValidMonthTests(string text, int month)
        {
            DateFragment df;
            int expectedMonth = month;

            df = DateFragment.Parse(text);
            Assert.False(df.HasDay);
            Assert.True(df.HasMonth);
            Assert.Equal(expectedMonth, df.Month);
            Assert.False(df.HasYear);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("decembler")]
        [Theory()]
        public void InvalidMonthTests(string text)
        {
            Assert.Throws<FormatException>(() => DateFragment.Parse(text));
        }

        [InlineData("march 1", 1, 2)]
        [InlineData("march first", 1, 2)]
        [InlineData("march 2", 2, 2)]
        [InlineData("march second", 2, 2)]
        [InlineData("march 20", 20, 2)]
        [InlineData("march twentieth", 20, 2)]
        [InlineData("march 21", 21, 2)]
        [InlineData("march twenty first", 21, 2)]
        [InlineData("march twenty-first", 21, 2)]
        [InlineData("march 30", 30, 2)]
        [Theory()]
        public void ValidMonthDayTests(string text, int day, int month)
        {
            DateFragment df;
            int expectedDay = day;
            int expectedMonth = month;

            df = DateFragment.Parse(text);
            Assert.True(df.HasDay);
            Assert.Equal(expectedDay, df.DayOfMonth);
            Assert.True(df.HasMonth);
            Assert.Equal(expectedMonth, df.Month);
            Assert.False(df.HasYear);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("April 1994", 3, 1994)]
        [InlineData("April, 1994", 3, 1994)]
        [InlineData("April, 1994,", 3, 1994)]
        [InlineData("on April 1994", 3, 1994)]
        [InlineData("on April, 1994", 3, 1994)]
        [InlineData("on April, 1994,", 3, 1994)]
        [Theory()]
        public void ValidMonthYearTests(string text, int month, int year)
        {
            DateFragment df;
            int expectedMonth = month;
            int expectedYear = year;

            df = DateFragment.Parse(text);
            Assert.False(df.HasDay);
            Assert.True(df.HasMonth);
            Assert.Equal(expectedMonth, df.Month);
            Assert.True(df.HasYear);
            Assert.Equal(expectedYear, df.Year);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("April 16 1994", 16, 3, 1994)]
        [InlineData("16 April 1994", 16, 3, 1994)]
        [InlineData("April 16 1994,", 16, 3, 1994)]
        [InlineData("16 April 1994,", 16, 3, 1994)]
        [InlineData("April 16, 1994", 16, 3, 1994)]
        [InlineData("16 April, 1994", 16, 3, 1994)]
        [InlineData("April 16, 1994,", 16, 3, 1994)]
        [InlineData("16 April, 1994,", 16, 3, 1994)]
        [InlineData("April, 16 1994", 16, 3, 1994)]
        [InlineData("16, April 1994", 16, 3, 1994)]
        [InlineData("April, 16 1994,", 16, 3, 1994)]
        [InlineData("16, April 1994,", 16, 3, 1994)]
        [InlineData("April, 16, 1994", 16, 3, 1994)]
        [InlineData("16, April, 1994", 16, 3, 1994)]
        [InlineData("April, 16, 1994,", 16, 3, 1994)]
        [InlineData("16, April, 1994,", 16, 3, 1994)]
        [InlineData("the April 16 1994", 16, 3, 1994)]
        [InlineData("the 16 April 1994", 16, 3, 1994)]
        [InlineData("the April 16 1994,", 16, 3, 1994)]
        [InlineData("the 16 April 1994,", 16, 3, 1994)]
        [InlineData("the April 16, 1994", 16, 3, 1994)]
        [InlineData("the 16 April, 1994", 16, 3, 1994)]
        [InlineData("the April 16, 1994,", 16, 3, 1994)]
        [InlineData("the 16 April, 1994,", 16, 3, 1994)]
        [InlineData("the April, 16 1994", 16, 3, 1994)]
        [InlineData("the 16, April 1994", 16, 3, 1994)]
        [InlineData("the April, 16 1994,", 16, 3, 1994)]
        [InlineData("the 16, April 1994,", 16, 3, 1994)]
        [InlineData("the April, 16, 1994", 16, 3, 1994)]
        [InlineData("the 16, April, 1994", 16, 3, 1994)]
        [InlineData("the April, 16, 1994,", 16, 3, 1994)]
        [InlineData("the 16, April, 1994,", 16, 3, 1994)]
        [Theory()]
        public void ValidDayMonthYearTests(string text, int day, int month, int year)
        {
            DateFragment df;
            int expectedDay = day;
            int expectedMonth = month;
            int expectedYear = year;

            df = DateFragment.Parse(text);
            Assert.True(df.HasDay);
            Assert.Equal(expectedDay, df.DayOfMonth);
            Assert.True(df.HasMonth);
            Assert.Equal(expectedMonth, df.Month);
            Assert.True(df.HasYear);
            Assert.Equal(expectedYear, df.Year);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("on 3rd of April 1994,", 3, 3, 1994)]
        [InlineData("on April 3rd 1994,", 3, 3, 1994)]
        [InlineData("on April 3rd, 1994,", 3, 3, 1994)]
        [InlineData("on 3'rd of April 1994,", 3, 3, 1994)]
        [InlineData("on third of April 1994,", 3, 3, 1994)]
        [InlineData("on 20th of April 1994,", 20, 3, 1994)]
        [InlineData("on April 20th 1994,", 20, 3, 1994)]
        [InlineData("on 20'th of April 1994,", 20, 3, 1994)]
        [InlineData("on April 20'th, 1994,", 20, 3, 1994)]
        [InlineData("on twentieth of April 1994,", 20, 3, 1994)]
        [InlineData("on April twentieth, 1994,", 20, 3, 1994)]
        [InlineData("on 21st of April 1994,", 21, 3, 1994)]
        [InlineData("on April 21st 1994,", 21, 3, 1994)]
        [InlineData("on 21'st of April 1994,", 21, 3, 1994)]
        [InlineData("on April 21'st, 1994,", 21, 3, 1994)]
        [InlineData("on twenty-first of April 1994,", 21, 3, 1994)]
        [InlineData("on April twenty-first, 1994,", 21, 3, 1994)]
        [InlineData("on twenty first of April 1994,", 21, 3, 1994)]
        [InlineData("on April twenty first, 1994,", 21, 3, 1994)]

        [InlineData("twenty first day in the month of April in the year of our lord 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on the twenty first day in the month of April in the year of our lord 1994,", 21, 3, 1994)]

        [InlineData("on 30th of May 1994,", 30, 4, 1994)]
        [InlineData("on May 30th 1994,", 30, 4, 1994)]
        [InlineData("on 30'th of May 1994,", 30, 4, 1994)]
        [InlineData("on May 30'th, 1994,", 30, 4, 1994)]
        [InlineData("on thirtieth of May 1994,", 30, 4, 1994)]
        [InlineData("on May thirtieth, 1994,", 30, 4, 1994)]
        [InlineData("on 31st of May 1994,", 31, 4, 1994)]
        [InlineData("on May 31st 1994,", 31, 4, 1994)]
        [InlineData("on 31'st of May 1994,", 31, 4, 1994)]
        [InlineData("on May 31'st, 1994,", 31, 4, 1994)]
        [InlineData("on thirty-first of May 1994,", 31, 4, 1994)]
        [InlineData("on May thirty-first, 1994,", 31, 4, 1994)]
        [InlineData("on thirty first of May 1994,", 31, 4, 1994)]
        [InlineData("on May thirty first, 1994,", 31, 4, 1994)]

        [InlineData("first in third group, on 3rd of April 1994,", 3, 3, 1994)]
        [InlineData("first in third group, on April 3rd 1994,", 3, 3, 1994)]
        [InlineData("first in third group, on April 3rd, 1994,", 3, 3, 1994)]
        [InlineData("first in third group, on 3'rd of April 1994,", 3, 3, 1994)]
        [InlineData("first in third group, on third of April 1994,", 3, 3, 1994)]
        [InlineData("first in third group, on 20th of April 1994,", 20, 3, 1994)]
        [InlineData("first in third group, on April 20th 1994,", 20, 3, 1994)]
        [InlineData("first in third group, on 20'th of April 1994,", 20, 3, 1994)]
        [InlineData("first in third group, on April 20'th, 1994,", 20, 3, 1994)]
        [InlineData("first in third group, on twentieth of April 1994,", 20, 3, 1994)]
        [InlineData("first in third group, on April twentieth, 1994,", 20, 3, 1994)]
        [InlineData("first in third group, on 21st of April 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on April 21st 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on 21'st of April 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on April 21'st, 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on twenty-first of April 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on April twenty-first, 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on twenty first of April 1994,", 21, 3, 1994)]
        [InlineData("first in third group, on April twenty first, 1994,", 21, 3, 1994)]
        [Theory()]
        public void ValidOrdinalDayMonthYearTests(string text, int day, int month, int year)
        {
            DateFragment df;
            int expectedDay = day;
            int expectedMonth = month;
            int expectedYear = year;

            df = DateFragment.Parse(text);
            Assert.True(df.HasDay);
            Assert.Equal(expectedDay, df.DayOfMonth);
            Assert.True(df.HasMonth);
            Assert.Equal(expectedMonth, df.Month);
            Assert.True(df.HasYear);
            Assert.Equal(expectedYear, df.Year);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }


        [InlineData("45 ad,", 45)]
        [InlineData("AD 45,", 45)]
        [InlineData("45 A.D.", 45)]
        [InlineData("45 anno Domini", 45)]
        [InlineData("45 common era", 45)]
        [InlineData("45 current era", 45)]
        [InlineData("45 CE", 45)]
        [InlineData("CE 45", 45)]
        [InlineData("45 C.E.", 45)]

        [InlineData("45 BCE", -45)]
        [InlineData("45 b.c.e.", -45)]
        [InlineData("45 B.C.E.", -45)]
        [InlineData("45 BC,", -45)]
        [InlineData("45 B.C.,", -45)]
        [InlineData("45 b.c.", -45)]
        [InlineData("45 before Christ", -45)]
        [InlineData("45 before common era", -45)]

        [InlineData("4500 BCE", -4500)]
        [Theory()]
        public void ValidYearDesignationTests(string text, int year)
        {
            DateFragment df;
            int expectedYear = year;

            df = DateFragment.Parse(text);
            Assert.False(df.HasDay);
            Assert.False(df.HasMonth);
            Assert.True(df.HasYear);
            Assert.Equal(expectedYear, df.Year);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("January 45 ad,", 0, 45)]
        [InlineData("January AD 45,", 0, 45)]
        [InlineData("January 45 A.D.", 0, 45)]
        [InlineData("January 45 anno Domini", 0, 45)]
        [InlineData("January 45 common era", 0, 45)]
        [InlineData("January 45 current era", 0, 45)]
        [InlineData("January 45 CE", 0, 45)]
        [InlineData("January CE 45", 0, 45)]
        [InlineData("January 45 C.E.", 0, 45)]

        [InlineData("January 45 BCE", 0, -45)]
        [InlineData("January 45 b.c.e.", 0, -45)]
        [InlineData("January 45 B.C.E.", 0, -45)]
        [InlineData("January 45 BC,", 0, -45)]
        [InlineData("January 45 B.C.,", 0, -45)]
        [InlineData("January 45 b.c.", 0, -45)]
        [InlineData("January 45 before Christ", 0, -45)]
        [InlineData("January 45 before common era", 0, -45)]

        [InlineData("January 17 ad,", 0, 17)]
        [InlineData("January AD 17,", 0, 17)]
        [InlineData("January 17 A.D.", 0, 17)]
        [InlineData("January 17 anno Domini", 0, 17)]
        [InlineData("January 17 common era", 0, 17)]
        [InlineData("January 17 current era", 0, 17)]
        [InlineData("January 17 CE", 0, 17)]
        [InlineData("January CE 17", 0, 17)]
        [InlineData("January 17 C.E.", 0, 17)]

        [InlineData("January 17 BCE", 0, -17)]
        [InlineData("January 17 b.c.e.", 0, -17)]
        [InlineData("January 17 B.C.E.", 0, -17)]
        [InlineData("January 17 BC,", 0, -17)]
        [InlineData("January 17 B.C.,", 0, -17)]
        [InlineData("January 17 b.c.", 0, -17)]
        [InlineData("January 17 before Christ", 0, -17)]
        [InlineData("January 17 before common era", 0, -17)]
        [Theory()]
        public void ValidMonthYearDesignationTests(string text, int month, int year)
        {
            DateFragment df;
            int expectedYear = year;
            int expectedMonth = month;

            df = DateFragment.Parse(text);
            Assert.False(df.HasDay);
            Assert.True(df.HasMonth);
            Assert.Equal(expectedMonth, df.Month);
            Assert.True(df.HasYear);
            Assert.Equal(expectedYear, df.Year);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("January 10, 45 ad,", 10, 0, 45)]
        [InlineData("January 10, AD 45,", 10, 0, 45)]
        [InlineData("January 10, 45 A.D.", 10, 0, 45)]
        [InlineData("January 10, 45 anno Domini", 10, 0, 45)]
        [InlineData("January 10, 45 common era", 10, 0, 45)]
        [InlineData("January 10, 45 current era", 10, 0, 45)]
        [InlineData("January 10, 45 CE", 10, 0, 45)]
        [InlineData("January 10, CE 45", 10, 0, 45)]
        [InlineData("January 10, 45 C.E.", 10, 0, 45)]

        [InlineData("January 10, 45 BCE", 10, 0, -45)]
        [InlineData("January 10, 45 b.c.e.", 10, 0, -45)]
        [InlineData("January 10, 45 B.C.E.", 10, 0, -45)]
        [InlineData("January 10, 45 BC,", 10, 0, -45)]
        [InlineData("January 10, 45 B.C.,", 10, 0, -45)]
        [InlineData("January 10, 45 b.c.", 10, 0, -45)]
        [InlineData("January 10, 45 before Christ", 10, 0, -45)]
        [InlineData("January 10, 45 before common era", 10, 0, -45)]

        [InlineData("January 10, 17 ad,", 10, 0, 17)]
        [InlineData("January 10, AD 17,", 10, 0, 17)]
        [InlineData("January 10, 17 A.D.", 10, 0, 17)]
        [InlineData("January 10, 17 anno Domini", 10, 0, 17)]
        [InlineData("January 10, 17 common era", 10, 0, 17)]
        [InlineData("January 10, 17 current era", 10, 0, 17)]
        [InlineData("January 10, 17 CE", 10, 0, 17)]
        [InlineData("January 10, CE 17", 10, 0, 17)]
        [InlineData("January 10, 17 C.E.", 10, 0, 17)]

        [InlineData("January 10, 17 BCE", 10, 0, -17)]
        [InlineData("January 10, 17 b.c.e.", 10, 0, -17)]
        [InlineData("January 10, 17 B.C.E.", 10, 0, -17)]
        [InlineData("January 10, 17 BC,", 10, 0, -17)]
        [InlineData("January 10, 17 B.C.,", 10, 0, -17)]
        [InlineData("January 10, 17 b.c.", 10, 0, -17)]
        [InlineData("January 10, 17 before Christ", 10, 0, -17)]
        [InlineData("January 10, 17 before common era", 10, 0, -17)]
        [Theory()]
        public void ValidDayMonthYearDesignationTests(string text, int day, int month, int year)
        {
            DateFragment df;
            int expectedYear = year;
            int expectedMonth = month;
            int expectedDay = day;

            df = DateFragment.Parse(text);
            Assert.True(df.HasDay);
            Assert.Equal(expectedDay, df.DayOfMonth);
            Assert.True(df.HasMonth);
            Assert.Equal(expectedMonth, df.Month);
            Assert.True(df.HasYear);
            Assert.Equal(expectedYear, df.Year);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }

        [InlineData("twenty first century", 21)]
        [InlineData("twenty-first century", 21)]
        [InlineData("21st century", 21)]
        [InlineData("21'st century", 21)]
        [InlineData("twentieth century", 20)]
        [InlineData("20th century", 20)]
        [InlineData("20'th century", 20)]
        [InlineData("nineteenth century", 19)]
        [InlineData("19th century", 19)]
        [InlineData("19'th century", 19)]
        [InlineData("fifteenth century", 15)]
        [InlineData("15th century", 15)]
        [InlineData("15'th century", 15)]
        [InlineData("fifteenth century BC", -15)]
        [InlineData("15th century BC", -15)]
        [InlineData("15'th century BC", -15)]
        [Theory()]
        public void ValidCenturyTests(string text, int century)
        {
            DateFragment df;

            df = DateFragment.Parse(text);
            Assert.False(df.HasDay);
            Assert.False(df.HasMonth);
            Assert.False(df.HasYear);
            Assert.False(df.HasDecade);
            Assert.True(df.HasCentury);
            Assert.Equal(century, df.Century);
        }

        [InlineData("the 1950s", 1950)]
        [InlineData("the 1950's", 1950)]
        [InlineData("the 50s", 50)]
        [InlineData("the 50's", 50)]
        [Theory()]
        public void ValidDecadeTests(string text, int decade)
        {
            DateFragment df;

            df = DateFragment.Parse(text);
            Assert.False(df.HasDay);
            Assert.False(df.HasMonth);
            Assert.False(df.HasYear);
            Assert.True(df.HasDecade);
            Assert.Equal(decade, df.Decade);
            Assert.False(df.HasCentury);
        }

        [InlineData("1/9/2024", 9, 0, 2024)]
        [InlineData("1/29/2024", 29, 0, 2024)]
        [InlineData("2024-05-07", 7, 4, 2024)]
        [Theory()]
        public void RegularDateStringTests(string text, int day, int month, int year)
        {
            DateFragment df;

            df = DateFragment.Parse(text);
            Assert.True(df.HasDay);
            Assert.Equal(day, df.DayOfMonth);
            Assert.True(df.HasMonth);
            Assert.Equal(month, df.Month);
            Assert.True(df.HasYear);
            Assert.Equal(year, df.Year);
            Assert.False(df.HasDecade);
            Assert.False(df.HasCentury);
        }
    }
}
