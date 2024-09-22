using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classification.Date
{
    public class DateFragementRange
    {
        private const string _invalidFormatException = "String was not recognized as a valid date fragment range";

        private static readonly List<string> _rangeSeperator = new List<string>()
        {
            "to",
            "upto",
            "through",
        };

        public bool HasStartDate => !object.ReferenceEquals(null, Start);
        public bool HasEndDate => !object.ReferenceEquals(null, End);

        public DateFragment? Start { get; set; }
        public DateFragment? End { get; set; }

        private DateFragementRange()
        { }

        public DateFragementRange(DateFragment start, DateFragment end)
        {
            Start = start;
            End = end;
        }

        public static DateFragementRange Parse(string dateString)
        {
            if (string.IsNullOrEmpty(dateString))
            {
                throw new FormatException(_invalidFormatException);
            }

            var splits = dateString.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var lowerSplits = splits.Select(x => x.ToLower()).ToList();

            if (!splits.Any())
            {
                throw new FormatException(_invalidFormatException);
            }

            var sepSplit = new List<int>();
            var dashSplit = new List<int>();

            // Track singular tokens that can be split into two dates, like "1950s-60s".
            var maybeSubSplit = new List<string>();
            int splitIndex = 0;
            bool foundSplit = false;
            string leftText = string.Empty;
            string rightText = string.Empty;

            int index = 0;
            foreach (var s in lowerSplits)
            {
                if (_rangeSeperator.Contains(s))
                {
                    sepSplit.Add(index);
                }

                if (s == "-")
                {
                    dashSplit.Add(index);
                }
                else if (s.Contains("-"))
                {
                    maybeSubSplit.Add(s);
                }

                index++;
            }

            foreach (var i in sepSplit)
            {
                if (i > 0 && i < splits.Length - 1)
                {
                    splitIndex = i;
                    foundSplit = true;
                    break;
                }
            }

            if (foundSplit == false && dashSplit.Count > 0)
            {
                /***
                 * If there is an odd number of dashes,
                 * split in the middle.
                 */

                if (dashSplit.Count == 1)
                {
                    splitIndex = dashSplit[0];
                    foundSplit = true;
                }
                else if ((dashSplit.Count & 1) == 1)
                {
                    // integer truncate
                    splitIndex = dashSplit[dashSplit.Count / 2];
                    foundSplit = true;
                }
            }

            if (foundSplit)
            {
                leftText = String.Join(" ", splits.Take(splitIndex));
                rightText = String.Join(" ", splits.Skip(splitIndex + 1));
            }
            else if (maybeSubSplit.Count == 1)
            {
                var dashToken = maybeSubSplit[0];

                var subSplits = dashToken.Split('-');

                if (subSplits.Length == 2)
                {
                    leftText = subSplits[0];
                    rightText = subSplits[1];
                }

                if (!string.IsNullOrEmpty(leftText) && !string.IsNullOrEmpty(rightText))
                {
                    foundSplit = true;
                }
            }

            if (foundSplit == false)
            {
                throw new FormatException(_invalidFormatException);
            }

            var result = new DateFragementRange();
            DateFragment? leftParse = null;
            DateFragment? rightParse = null;
            bool hasValue = false;

            leftParse = DateFragment.InternalParse(leftText, false);
            rightParse = DateFragment.InternalParse(rightText, false);

            // This is supposed to be a date range, so it doesn't make sense
            // to succeed if only one value is found.
            if (leftParse != null && rightParse != null)
            {
                result.Start = leftParse!;
                result.End = rightParse!;
                hasValue = true;
            }

            if (!hasValue)
            {
                throw new FormatException(_invalidFormatException);
            }

            // if this is text like "14th to 20th centuries" then the right
            // is parsed correctly, but the left is set as a day.
            if (result.Start!.IsDayOnly && result.End!.HasCentury)
            {
                int val = result.Start.DayOfMonth!.Value;
                result.Start.UnsetDayOfMonth();
                result.Start.SetCentury(val);
            }

            // If this is a decade space with missing century prefix like "1950s-60s",
            // fix the right hand side decade.
            if (result.Start.IsDecadeOnly
                && result.End!.IsDecadeOnly
                && result.Start.Decade > 100
                && result.End.Decade < 100)
            {
                int leftDec = result.Start.Decade.Value / 100;
                leftDec *= 100;
                result.End.SetDecade(result.End.Decade.Value + leftDec);
            }

            // check for missing data that needs to be copied to the other side.
            if (result.Start!.HasDay && result.End!.HasDay)
            {
                if (result.Start.HasMonth && !result.End.HasMonth)
                {
                    result.End.SetMonth(result.Start.Month!.Value);
                }
                else if (result.End.HasMonth && !result.Start.HasMonth)
                {
                    result.Start.SetMonth(result.End.Month!.Value);
                }
            }

            if (result.Start!.HasMonth && result.End!.HasMonth)
            {
                if (result.Start.HasYear && !result.End.HasYear)
                {
                    result.End.SetYear(result.Start.Year!.Value);
                }
                else if (result.End.HasYear && !result.Start.HasYear)
                {
                    result.Start.SetYear(result.End.Year!.Value);
                }
            }

            return result;
        }

        public override string ToString()
        {
            if (HasStartDate && HasEndDate)
            {
                return $"{Start} to {End}";
            }
            else if (HasStartDate)
            {
                return Start!.ToString();
            }
            else if (HasEndDate)
            {
                return End!.ToString();
            }
            else
            {
                return "empty range";
            }
        }

        public override int GetHashCode()
        {
            if (HasStartDate && HasEndDate)
            {
                return Start!.GetHashCode() ^ End!.GetHashCode();
            }
            else if (HasStartDate)
            {
                return Start!.GetHashCode();
            }
            else if (HasEndDate)
            {
                return End!.GetHashCode();
            }
            else
            {
                return 7;
            }
        }

        public static bool operator ==(DateFragementRange x, DateFragementRange y)
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
                if (x.HasStartDate != y.HasStartDate
                    || x.HasEndDate != y.HasEndDate)
                {
                    return false;
                }

                return x.Start == y.Start && x.End == y.End;
            }
        }

        public static bool operator !=(DateFragementRange x, DateFragementRange y) => !(x == y);

        public override bool Equals(object? obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (obj is DateFragementRange other)
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
                    if (this.HasStartDate != other.HasStartDate
                    || this.HasEndDate != other.HasEndDate)
                    {
                        return false;
                    }

                    return this.Start == other.Start && this.End == other.End;
                }
            }

            return false;
        }

        public bool Equals(DateFragementRange x, DateFragementRange y)
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
                if (x.HasStartDate != y.HasStartDate
                    || x.HasEndDate != y.HasEndDate)
                {
                    return false;
                }

                return x.Start == y.Start && x.End == y.End;
            }
        }

        public int GetHashCode([DisallowNull] DateFragementRange obj)
        {
            return obj.GetHashCode();
        }

        public bool Equals(DateFragementRange other)
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
                if (this.HasStartDate != other.HasStartDate
                    || this.HasEndDate != other.HasEndDate)
                {
                    return false;
                }

                return this.Start == other.Start && this.End == other.End;
            }
        }
    }
}
