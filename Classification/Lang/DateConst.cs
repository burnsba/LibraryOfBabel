using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Lang
{
    public class DateConst
    {
        public static readonly List<string> MonthName = new List<string>()
        {
            "january",
            "february",
            "march",
            "april",
            "may",
            "june",
            "july",
            "august",
            "september",
            "october",
            "november",
            "december",
        };

        public static readonly List<string> MonthNameAbv3 = new List<string>()
        {
            "jan",
            "feb",
            "mar",
            "apr",
            "may",
            "jun",
            "jul",
            "aug",
            "sep",
            "oct",
            "nov",
            "dec",
        };

        public static readonly List<string> YearDesignate = new List<string>()
        {
            "anno Domini",
            "AD",
            "A.D.",
            "common era",
            "current era",
            "CE",
            "C.E.",
        };

        public static readonly List<string[]> YearDesignateLowerSplits =
            YearDesignate
            .Select(x => x.ToLower())
            .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();

        public static readonly List<string> YearDesignateBc = new List<string>()
        {
            "BC",
            "B.C.",
            "before Christ",
            "before common era",
            "BCE",
            "B.C.E.",
        };

        public static readonly List<string[]> YearDesignateBcLowerSplits =
            YearDesignateBc
            .Select(x => x.ToLower())
            .Select(x => x.Split(' ', StringSplitOptions.RemoveEmptyEntries)).ToList();

        // 1st - 19th.
        public static readonly List<List<string>> PossessiveDayOfMonth = new List<List<string>>()
        {
            new List<string>()
            {
                "1st", "1'st", "first", "fst",
            },

            new List<string>()
            {
                "2nd", "2'nd", "second",
            },

            new List<string>()
            {
                "3rd", "3'rd", "third",
            },

            new List<string>()
            {
                "4th", "4'th", "fourth",
            },

            new List<string>()
            {
                "5th", "5'th", "fifth",
            },

            new List<string>()
            {
                "6th", "6'th", "sixth",
            },

            new List<string>()
            {
                "7th", "7'th", "seventh",
            },

            new List<string>()
            {
                "8th", "8'th", "eighth",
            },

            new List<string>()
            {
                "9th", "9'th", "ninth",
            },

            new List<string>()
            {
                "10th", "10'th", "tenth",
            },

            new List<string>()
            {
                "11th", "11'th", "eleventh",
            },

            new List<string>()
            {
                "12th", "12'th", "twelfth", "twelvth",
            },

            new List<string>()
            {
                "13th", "13'th", "thirteenth",
            },

            new List<string>()
            {
                "14th", "14'th", "fourteenth",
            },

            new List<string>()
            {
                "15th", "15'th", "fifteenth",
            },

            new List<string>()
            {
                "16th", "16'th", "sixteenth",
            },

            new List<string>()
            {
                "17th", "17'th", "seventeenth",
            },

            new List<string>()
            {
                "18th", "18'th", "eighteenth",
            },

            new List<string>()
            {
                "19th", "19'th", "ninteenth", "nineteenth",
            },
        };

        public static string ToOrdinal(int i)
        {
            if (i <= 0)
            {
                throw new NotSupportedException();
            }

            if (i < 20)
            {
                return PossessiveDayOfMonth[i - 1][0];
            }

            var prefix = i / 10;
            var index = i % 10;

            if (index == 0)
            {
                return $"{prefix}0th";
            }

            return $"{prefix}{PossessiveDayOfMonth[index - 1][0]}";
        }
    }
}
