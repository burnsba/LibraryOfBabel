using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classification.Date
{
    public class DateFragment
    {
        private const string _invalidFormatException = "String was not recognized as a valid date fragment";
        private const int _numberNearYearDesignateMaxDistance = 3;

        private int _dayOfMonth;
        private int _month; // 0 - 11
        private int _year;
        private int _decade; // bit of a misnomer since this will be 1950 or similar
        private int _century;

        private bool _hasDay;
        private bool _hasMonth;
        private bool _hasYear;
        private bool _hasDecade;
        private bool _hasCentury;

        public static readonly int[] DaysInMonthInLeapYears = new int[] { 31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        public static readonly int[] DaysInMonthInRegularYears = new int[] { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };

        private static readonly List<string> CenturyMarker = new List<string>()
        {
            "century",
            "centuries",
        };

        

        private static readonly Regex _intCommaRegex = new Regex("^[,0-9]+$");

        public static DateFragment Zero { get; } = new DateFragment();

        public bool HasDay => _hasDay;
        public bool HasMonth => _hasMonth;
        public bool HasYear => _hasYear;
        public bool HasDecade => _hasDecade;
        public bool HasCentury => _hasCentury;

        public bool IsCompleteYmd => HasDay && HasMonth && HasYear;

        public bool AnyValue => HasDay || HasMonth || HasYear || HasDecade || HasCentury;

        public bool IsDayOnly => HasDay && !HasMonth && !HasYear && !HasDecade && !HasCentury;
        public bool IsMonthOnly => !HasDay && HasMonth && !HasYear && !HasDecade && !HasCentury;
        public bool IsYearOnly => !HasDay && !HasMonth && HasYear && !HasDecade && !HasCentury;
        public bool IsDecadeOnly => !HasDay && !HasMonth && !HasYear && HasDecade && !HasCentury;
        public bool IsCenturyOnly => !HasDay && !HasMonth && !HasYear && !HasDecade && HasCentury;

        public int? DayOfMonth => _hasDay ? _dayOfMonth : null;

        /// <summary>
        /// Index starts at 0. January = 0, etc.
        /// </summary>
        public int? Month => HasMonth ? _month : null;

        /// <summary>
        /// General decade range, like "1950s".
        /// </summary>
        public int? Decade => HasDecade ? _decade : null;
        public int? Year => HasYear ? _year : null;
        public int? Century => HasCentury ? _century : null;

        internal DateFragment()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day">Day, from 1 to 31.</param>
        /// <param name="month">Jan = 0, Dec = 11.</param>
        /// <param name="year">Year. Negative values for BCE.</param>
        public DateFragment(int? day, int? month, int? year)
        {
            if (day.HasValue)
            {
                SetDayOfMonth(day.Value);
            }

            if (month.HasValue)
            {
                SetMonth(month.Value);
            }

            if (year.HasValue)
            {
                SetYear(year.Value);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="day">Day, from 1 to 31.</param>
        /// <param name="month">Jan = 0, Dec = 11.</param>
        /// <param name="year">Year. Negative values for BCE.</param>
        /// <param name="decade">Stored like a year, so 1950 = 1950s.</param>
        /// <param name="century">20th century = 20. Negative values for BCE.</param>
        public DateFragment(int? day, int? month, int? year, int? decade, int? century)
            : this(day, month, year)
        {
            if (decade.HasValue)
            {
                SetDecade(decade.Value);
            }

            if (century.HasValue)
            {
                SetCentury(century.Value);
            }
        }

        internal void SetDayOfMonth(int value)
        {
            if (value < 32)
            {
                _dayOfMonth = value;
                _hasDay = true;
            }
        }

        internal void UnsetDayOfMonth()
        {
            _dayOfMonth = 0;
            _hasDay = false;
        }

        internal void SetMonth(int value)
        {
            if (value >= 0 && value < 12)
            {
                _month = value;
                _hasMonth = true;
            }
        }

        internal void SetDecade(int value)
        {
            _decade = value;
            _hasDecade = true;
        }

        internal void SetYear(int value)
        {
            _year = value;
            _hasYear = true;
        }

        internal void SetYearBc(int value)
        {
            _year = 0 - value;
            _hasYear = true;
        }

        internal void SetCentury(int value)
        {
            _century = value;
            _hasCentury = true;
        }

        internal void SetCenturyBc(int value)
        {
            _century = 0 - value;
            _hasCentury = true;
        }

        internal static DateFragment? InternalParse(string dateString, bool allowThrow)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                if (allowThrow)
                {
                    throw new FormatException(_invalidFormatException);
                }

                return null;
            }

            var tokens = Tokenize(dateString);

            var useful = tokens.Where(x => x.HasValue || x.YearDesignatePart > 0 || x.Part == PartType.Century).ToList();

            if (!useful.Any())
            {
                if (string.IsNullOrEmpty(dateString))
                {
                    if (allowThrow)
                    {
                        throw new FormatException(_invalidFormatException);
                    }

                    return null;
                }
            }

            var result = new DateFragment();

            /**** check for immediate return cases. */
            if (useful.Count == 1)
            {
                var t1 = useful[0];

                // 1994
                if (t1.MaybeYear)
                {
                    result.SetYear(t1.Value);

                    return result;
                }
            }
            else if (useful.Count == 2)
            {
                var t1 = useful[0];
                var t2 = useful[1];

                // april 1994
                if (t1.Part == PartType.Month && t2.MaybeYear)
                {
                    result.SetMonth(t1.Value);
                    result.SetYear(t2.Value);

                    return result;
                }
            }
            else if (useful.Count == 3)
            {
                var t1 = useful[0];
                var t2 = useful[1];
                var t3 = useful[2];

                if (t1.Part == PartType.Month && t2.MaybeDayOfMonth && t3.MaybeYear)
                {
                    // april 16, 1994
                    result.SetMonth(t1.Value);
                    result.SetDayOfMonth(t2.Value);
                    result.SetYear(t3.Value);

                    return result;
                }
                else if (t1.MaybeDayOfMonth && t2.Part == PartType.Month && t3.MaybeYear)
                {
                    // 16 april 1994
                    result.SetDayOfMonth(t1.Value);
                    result.SetMonth(t2.Value);
                    result.SetYear(t3.Value);

                    return result;
                }
            }

            /****
             * Done with immediate return check. Be careful to not double spend a token (check `Consumed`).
             * */

            // First check for a year designate (BC/AD).
            // This will consume somewhat ambiguous text, taking a nearby number for the year
            // instead of day of the month. For example, the year 17 in "January 17 BCE".

            // is this a "year BC / AD"?
            // Also handles nearby "century" token ("15th century BC")
            var yearDesignate = tokens.FirstOrDefault(x => x.MaybeYearDesignate);
            if (!object.ReferenceEquals(null, yearDesignate))
            {
                // Find a year near "BC" (or "AD").
                // Strictly this should be within one token, but also need space for words like "century".
                var closestNumber = CollectionUtility.SpiralFindNext(
                    tokens,
                    yearDesignate.Index,
                    x => !x.Consumed && x.HasValue,
                    _numberNearYearDesignateMaxDistance);

                // Check for nearby "century" markers too
                var centuryToken = CollectionUtility.SpiralFindNext(
                    tokens,
                    yearDesignate.Index,
                    x => !x.Consumed && x.Part == PartType.Century,
                    _numberNearYearDesignateMaxDistance);

                if (object.ReferenceEquals(null, closestNumber))
                {
                    // Couldn't find a year. Maybe this is a phrase like "common era 450".
                    // Find the last term in the "year designate", then look for a nearby year again.
                    var lastDesignateTerm = yearDesignate;
                    while (true)
                    {
                        int index = lastDesignateTerm.Index + 1;
                        if (index < tokens.Count)
                        {
                            if (tokens[index].YearDesignatePart > 0)
                            {
                                lastDesignateTerm = tokens[index];
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    if (lastDesignateTerm != yearDesignate)
                    {
                        closestNumber = CollectionUtility.SpiralFindNext(
                            tokens,
                            lastDesignateTerm.Index,
                            x => x.HasValue,
                            _numberNearYearDesignateMaxDistance);

                        if (object.ReferenceEquals(null, centuryToken))
                        {
                            centuryToken = CollectionUtility.SpiralFindNext(
                                tokens,
                                lastDesignateTerm.Index,
                                x => !x.Consumed && x.Part == PartType.Century,
                                _numberNearYearDesignateMaxDistance);
                        }
                    }
                }

                if (!object.ReferenceEquals(null, closestNumber))
                {
                    if (object.ReferenceEquals(null, centuryToken))
                    {
                        if (yearDesignate.YearDesignateBc)
                        {
                            result.SetYearBc(closestNumber.Value);
                        }
                        else
                        {
                            result.SetYear(closestNumber.Value);
                        }
                    }
                    else
                    {
                        if (yearDesignate.YearDesignateBc)
                        {
                            result.SetCenturyBc(closestNumber.Value);
                        }
                        else
                        {
                            result.SetCentury(closestNumber.Value);
                        }

                        centuryToken.Consumed = true;
                    }

                    closestNumber.Consumed = true;
                }
            }

            // Now check for regular century markers: "15th century" etc
            if (!result.HasCentury)
            {
                var centuryToken = useful.FirstOrDefault(x => !x.Consumed && x.Part == PartType.Century);
                if (!object.ReferenceEquals(null, centuryToken))
                {
                    // is there a century nearby?
                    // Don't double spend a number here.
                    var c1 = CollectionUtility.SpiralFindNext(tokens, centuryToken.Index, x => !x.Consumed && (x.HasValue || x.MaybeOrdinal));
                    Token? c2 = null;

                    // day could be a compound ordinal across tokens like "twenty first"
                    if (!object.ReferenceEquals(null, c1) && c1.MaybeOrdinal)
                    {
                        c2 = CollectionUtility.SpiralFindNext(tokens, c1.Index, x => x != c1 && !x.Consumed && (x.HasValue || x.MaybeOrdinal), 1);
                    }

                    int century = 0;
                    if (c1 != null && (c1.HasValue || c1.MaybeOrdinal) && c2 != null && (c2.HasValue || c2.MaybeOrdinal))
                    {
                        c1.Consumed = true;
                        c2.Consumed = true;
                        century = c1.Value + c2.Value;
                    }
                    else if (c1 != null && (c1.HasValue || c1.MaybeOrdinal))
                    {
                        c1.Consumed = true;
                        century = c1.Value;
                    }
                    else if (c2 != null && (c2.HasValue || c2.MaybeOrdinal))
                    {
                        c2.Consumed = true;
                        century = c2.Value;
                    }

                    if (century > 0)
                    {
                        result.SetCentury(century);
                        centuryToken.Consumed = true;
                    }
                }
            }

            var decadeToken = useful.FirstOrDefault(x => !x.Consumed && x.MaybeDecade);
            if (!object.ReferenceEquals(null, decadeToken))
            {
                result.SetDecade(decadeToken.Value);
                decadeToken.Consumed = true;
            }

            // is there a month marker?
            var monthToken = useful.FirstOrDefault(x => x.Part == PartType.Month);
            if (!object.ReferenceEquals(null, monthToken))
            {
                monthToken.Consumed = true;
                result.SetMonth(monthToken.Value);

                // If the only thing known is the month, we're done.
                if (useful.Count == 1)
                {
                    return result;
                }

                // is there a day nearby?
                // Don't double spend a number here.
                var d1 = CollectionUtility.SpiralFindNext(tokens, monthToken.Index, x => !x.Consumed && (x.MaybeDayOfMonth || x.MaybeOrdinal));
                Token? d2 = null;

                // day could be a compound ordinal across tokens like "twenty first"
                if (!object.ReferenceEquals(null, d1) && d1.MaybeOrdinal)
                {
                    d2 = CollectionUtility.SpiralFindNext(tokens, d1.Index, x => x != d1 && !x.Consumed && (x.MaybeDayOfMonth || x.MaybeOrdinal), 1);
                }

                int day = 0;
                if (d1 != null && (d1.MaybeDayOfMonth || d1.MaybeOrdinal) && d2 != null && (d2.MaybeDayOfMonth || d2.MaybeOrdinal))
                {
                    d1.Consumed = true;
                    d2.Consumed = true;
                    day = d1.Value + d2.Value;
                }
                else if (d1 != null && (d1.MaybeDayOfMonth || d1.MaybeOrdinal))
                {
                    d1.Consumed = true;
                    day = d1.Value;
                }
                else if (d2 != null && (d2.MaybeDayOfMonth || d2.MaybeOrdinal))
                {
                    d2.Consumed = true;
                    day = d2.Value;
                }

                if (day > 0)
                {
                    result.SetDayOfMonth(day);
                }
            }

            if (!result.HasYear)
            {
                if (!object.ReferenceEquals(null, monthToken))
                {
                    // is there a nearby year?
                    var yearToken = CollectionUtility.SpiralFindNext(tokens, monthToken.Index, x => x.MaybeYear && !x.Consumed);

                    if (yearToken != null)
                    {
                        result.SetYear(yearToken.Value);
                        yearToken.Consumed = true;
                    }
                }
            }

            // Maybe this is a stand alone day like "the 14th".
            if (!result.AnyValue)
            {
                // Don't double spend a number here.
                var d1 = useful.FirstOrDefault(x => !x.Consumed && x.HasValue && (x.MaybeDayOfMonth || x.MaybeOrdinal));
                Token? d2 = null;

                // day could be a compound ordinal across tokens like "twenty first"
                if (!object.ReferenceEquals(null, d1) && d1.MaybeOrdinal)
                {
                    d2 = CollectionUtility.SpiralFindNext(tokens, d1.Index, x => x != d1 && !x.Consumed && (x.MaybeDayOfMonth || x.MaybeOrdinal), 1);
                }

                int day = 0;
                if (d1 != null && (d1.MaybeDayOfMonth || d1.MaybeOrdinal) && d2 != null && (d2.MaybeDayOfMonth || d2.MaybeOrdinal))
                {
                    d1.Consumed = true;
                    d2.Consumed = true;
                    day = d1.Value + d2.Value;
                }
                else if (d1 != null && (d1.MaybeDayOfMonth || d1.MaybeOrdinal))
                {
                    d1.Consumed = true;
                    day = d1.Value;
                }
                else if (d2 != null && (d2.MaybeDayOfMonth || d2.MaybeOrdinal))
                {
                    d2.Consumed = true;
                    day = d2.Value;
                }

                if (day > 0)
                {
                    result.SetDayOfMonth(day);
                }
            }

            if (result.AnyValue)
            {
                return result;
            }

            /// Hmm, couldn't resolve this to a natural language date. See if this parses as a regular date.
            if (dateString.Count(x => x == '/') == 2 || dateString.Count(x => x == '-') == 2)
            {
                DateTime dateTime;
                if (DateTime.TryParse(dateString, out dateTime))
                {
                    result.SetDayOfMonth(dateTime.Day);
                    result.SetMonth(dateTime.Month - 1);
                    result.SetYear(dateTime.Year);
                    return result;
                }
            }

            // Well, couldn't parse anything.

            if (allowThrow)
            {
                throw new FormatException(_invalidFormatException);
            }

            return null;
        }

        public static bool TryParse(string dateString, out DateFragment fragment)
        {
            fragment = DateFragment.Zero;

            var result = InternalParse(dateString, false);

            if (object.ReferenceEquals(null, result))
            {
                return false;
            }

            fragment = result;
            return true;
        }

        public static DateFragment Parse(string dateString)
        {
            var result = InternalParse(dateString, true);

            return result!;
        }

        private static List<Token> Tokenize(string source)
        {
            var tokens = source
                .Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select((x, i) => new Token(x) { Index = i })
                .ToList();

            var commaTrimLowerValues = new List<string>();
            foreach (var token in tokens)
            {
                var lower = token.Original.ToLower();

                string trimComma = lower;

                if (lower.EndsWith(","))
                {
                    trimComma = lower.Substring(0, lower.Length - 1);
                }

                commaTrimLowerValues.Add(trimComma);
            }

            for (int index = 0; index < tokens.Count; index++)
            {
                var t = tokens[index];
                var lower = t.Original.ToLower();

                string trimComma = commaTrimLowerValues[index];

                var monthIndex = Lang.DateConst.MonthName.IndexOf(lower);
                if (monthIndex < 0)
                {
                    monthIndex = Lang.DateConst.MonthNameAbv3.IndexOf(lower);
                }

                if (monthIndex < 0)
                {
                    monthIndex = Lang.DateConst.MonthName.IndexOf(trimComma);
                    if (monthIndex < 0)
                    {
                        monthIndex = Lang.DateConst.MonthNameAbv3.IndexOf(trimComma);
                    }
                }

                if (monthIndex >= 0)
                {
                    t.Part = PartType.Month;
                    t.SetValue(monthIndex);
                    continue;
                }

                foreach (var cm in CenturyMarker)
                {
                    if (trimComma == cm)
                    {
                        t.Part = PartType.Century;
                        goto continue_token;
                    }
                }

                // Check for decade text like "1950s" or "50's".
                // Note the zero in the string, to force significant digits.
                if (t.Original.EndsWith("0s") || t.Original.EndsWith("0's"))
                {
                    string ntext;

                    // Need to check the long one first
                    if (t.Original.EndsWith("0's"))
                    {
                        ntext = t.Original.Substring(0, t.Original.Length - 2);
                    }
                    else
                    {
                        ntext = t.Original.Substring(0, t.Original.Length - 1);
                    }

                    int i;
                    if (int.TryParse(ntext, out i))
                    {
                        t.SetValue(i);

                        t.MaybeDecade = true;

                        goto continue_token;
                    }
                }

                var m = _intCommaRegex.Match(t.Original);
                if (m.Success)
                {
                    var ntext = t.Original.Replace(",", string.Empty);
                    int i;
                    if (int.TryParse(ntext, out i))
                    {
                        t.SetValue(i);

                        if (i > 0 && i < 32)
                        {
                            t.MaybeDayOfMonth = true;
                        }
                        else if (i > 1900)
                        {
                            t.MaybeYear = true;
                        }

                        goto continue_token;
                    }
                }

                // check for ordinal.
                {
                    int groupIndex = 0;

                    if (trimComma == "20th" || trimComma == "20'th" || trimComma == "twentieth" || trimComma == "twenty")
                    {
                        t.Part = PartType.Day;
                        t.SetValue(20);
                        t.MaybeOrdinal = true;
                        t.MaybeDayOfMonth = true;
                        continue;
                    }
                    else if (trimComma == "30th" || trimComma == "30'th" || trimComma == "thirtieth" || trimComma == "thirty")
                    {
                        t.Part = PartType.Day;
                        t.SetValue(30);
                        t.MaybeOrdinal = true;
                        t.MaybeDayOfMonth = true;
                        continue;
                    }

                    foreach (var groupList in Lang.DateConst.PossessiveDayOfMonth)
                    {
                        foreach (var item in groupList)
                        {
                            if (groupIndex < 10)
                            {
                                var twentyCheck1 = "2" + item;
                                var twentyCheck2 = "twenty-" + item;
                                var thirtyCheck1 = "3" + item;
                                var thirtyCheck2 = "thirty-" + item;

                                if (trimComma == twentyCheck1 || trimComma == twentyCheck2)
                                {
                                    t.Part = PartType.Day;
                                    t.SetValue(20 + groupIndex + 1);
                                    t.MaybeOrdinal = true;
                                    goto continue_token;
                                }
                                else if (trimComma == thirtyCheck1 || trimComma == thirtyCheck2)
                                {
                                    t.Part = PartType.Day;
                                    t.SetValue(30 + groupIndex + 1);
                                    t.MaybeOrdinal = true;
                                    goto continue_token;
                                }
                            }

                            if (trimComma == item)
                            {
                                t.Part = PartType.Day;
                                t.SetValue(groupIndex + 1);
                                t.MaybeOrdinal = true;
                                t.MaybeDayOfMonth = true;
                                goto continue_token;
                            }
                        }

                        groupIndex++;
                    }

                }

                // check for year designate (BC, AD)
                if (!t.HasValue && t.YearDesignatePart == 0)
                {
                    var bigGroup = new List<(List<string[]>, bool)>()
                    {
                        /***
                         * Use a boolean flag to indicate if AD (false) or BC (true).
                         */
                        (Lang.DateConst.YearDesignateLowerSplits, false),
                        (Lang.DateConst.YearDesignateBcLowerSplits, true),
                    };

                    foreach (var grouping in bigGroup)
                    {
                        foreach (var line in grouping.Item1)
                        {
                            bool maybe = true;
                            int matchCount = 0;
                            int checkTokenIndex;
                            int offset = 0;
                            string checkTokenValue;

                            foreach (var s in line)
                            {
                                checkTokenIndex = index + offset;

                                checkTokenValue = commaTrimLowerValues[checkTokenIndex];

                                if (checkTokenValue == s)
                                {
                                    matchCount++;
                                }
                                else
                                {
                                    maybe = false;
                                    break;
                                }

                                offset++;
                            }

                            if (maybe && matchCount == line.Length)
                            {
                                for (int j = 0; j < matchCount; j++)
                                {
                                    tokens[index + j].MaybeYearDesignate = true;
                                    tokens[index + j].YearDesignatePart = j + 1;
                                    tokens[index + j].YearDesignateBc = grouping.Item2;
                                }

                                goto finish_year_designate;
                            }
                        }
                    }

                finish_year_designate:
                    ;
                }

            continue_token:
                continue;
            }

            return tokens;
        }

        public override string ToString()
        {
            var terms = new List<string>();

            if (HasMonth)
            {
                var m = Lang.DateConst.MonthName[_month];
                m = string.Join(string.Empty, m[0].ToString().ToUpperInvariant(), m.Substring(1));
                terms.Add(m);
            }

            if (HasDay)
            {
                terms.Add(Lang.DateConst.ToOrdinal(_dayOfMonth));
            }

            if (HasYear)
            {
                if (_year >= 0)
                {
                    terms.Add(_year.ToString());
                }
                else
                {
                    var pos = 0 - _year;
                    terms.Add(pos.ToString());
                    terms.Add("BC");
                }
            }

            if (HasDecade)
            {
                terms.Add($"{_decade}s");
            }

            if (HasCentury)
            {
                if (_century >= 0)
                {
                    terms.Add(Lang.DateConst.ToOrdinal(_century));
                    terms.Add("century");
                }
                else
                {
                    var pos = 0 - _century;
                    terms.Add(Lang.DateConst.ToOrdinal(pos));
                    terms.Add("century BC");
                }
            }

            return string.Join(" ", terms);
        }

        public override int GetHashCode()
        {
            int hash = 0;

            if (HasDay)
            {
                hash |= 1;
            }

            if (HasMonth)
            {
                hash |= 2;
            }

            if (HasYear)
            {
                hash |= 4;
            }

            if (HasDecade)
            {
                hash |= 8;
            }

            if (HasCentury)
            {
                hash |= 16;
            }

            hash = ~hash
                ^ _dayOfMonth
                ^ _month
                ^ _year
                ^ _decade
                ^ _century;

            return hash;
        }

        public static bool operator ==(DateFragment x, DateFragment y)
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
                if (x.HasDay != y.HasDay
                    || x.HasMonth != y.HasMonth
                    || x.HasYear != y.HasYear
                    || x.HasDecade != y.HasDecade
                    || x.HasCentury != y.HasCentury)
                {
                    return false;
                }

                if ((x.HasDay && x.DayOfMonth != y.DayOfMonth)
                    || (x.HasMonth && x.Month != y.Month)
                    || (x.HasYear && x.Year != y.Year)
                    || (x.HasDecade && x.Decade != y.Decade)
                    || (x.HasDecade && x.Century != y.Century)
                    )
                {
                    return false;
                }

                return true;
            }
        }

        public static bool operator !=(DateFragment x, DateFragment y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is DateFragment other)
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
                    if (this.HasDay != other.HasDay
                    || this.HasMonth != other.HasMonth
                    || this.HasYear != other.HasYear
                    || this.HasDecade != other.HasDecade
                    || this.HasCentury != other.HasCentury)
                    {
                        return false;
                    }

                    if ((this.HasDay && this.DayOfMonth != other.DayOfMonth)
                        || (this.HasMonth && this.Month != other.Month)
                        || (this.HasYear && this.Year != other.Year)
                        || (this.HasDecade && this.Decade != other.Decade)
                        || (this.HasDecade && this.Century != other.Century)
                        )
                    {
                        return false;
                    }

                    return true;
                }
            }

            return false;
        }

        public bool Equals(DateFragment x, DateFragment y)
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
                if (x.HasDay != y.HasDay
                    || x.HasMonth != y.HasMonth
                    || x.HasYear != y.HasYear
                    || x.HasDecade != y.HasDecade
                    || x.HasCentury != y.HasCentury)
                {
                    return false;
                }

                if ((x.HasDay && x.DayOfMonth != y.DayOfMonth)
                    || (x.HasMonth && x.Month != y.Month)
                    || (x.HasYear && x.Year != y.Year)
                    || (x.HasDecade && x.Decade != y.Decade)
                    || (x.HasDecade && x.Century != y.Century)
                    )
                {
                    return false;
                }

                return true;
            }
        }

        public int GetHashCode([DisallowNull] DateFragment obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(DateFragment other)
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
                if (this.HasDay != other.HasDay
                    || this.HasMonth != other.HasMonth
                    || this.HasYear != other.HasYear
                    || this.HasDecade != other.HasDecade
                    || this.HasCentury != other.HasCentury)
                {
                    return false;
                }

                if ((this.HasDay && this.DayOfMonth != other.DayOfMonth)
                    || (this.HasMonth && this.Month != other.Month)
                    || (this.HasYear && this.Year != other.Year)
                    || (this.HasDecade && this.Decade != other.Decade)
                    || (this.HasDecade && this.Century != other.Century)
                    )
                {
                    return false;
                }

                return true;
            }
        }
    }
}
