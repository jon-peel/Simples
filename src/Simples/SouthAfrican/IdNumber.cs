using System;
using System.Linq;

namespace Simples.SouthAfrican
{
    public readonly struct IdNumber
    {
        readonly string literal;

        IdNumber(string literalIdNumber) => literal = literalIdNumber.Length > 13 ? literalIdNumber.Substring(0, 13) : literalIdNumber;

        public DateTime? DateOfBirth => GetDateOfBirth();

        public Gender Gender => HasLength && IsNumeric && int.Parse(literal[6].ToString()) >= 5;

        public bool IsValid => Validate().valid;

        public bool SouthAfrican => HasLength && IsNumeric && int.Parse(literal[10].ToString()) == 0;

        bool HasLength => literal.Length == 13;

        bool IsNumeric => ulong.TryParse(literal, out var idv);

        public static implicit operator IdNumber(string literal) => new IdNumber(literal);

        public static implicit operator string(IdNumber idNumber) => idNumber.ToString();

        public override string ToString() => literal;

        static DateTime? CreateDate(int year, int month, int day)
        {
            try
            {
                var dateInSa = new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.FromHours(2));
                var localDate = dateInSa.LocalDateTime;
                return localDate;
            }
            catch (Exception) { return null; }
        }

        DateTime? GetDateOfBirth()
        {
            if (!HasLength) return null;
            if (!IsNumeric) return null;
            var yr = literal.Substring(0, 2);
            var month = int.Parse(literal.Substring(2, 2));
            if (month < 1 || month > 12) return null;
            var day = int.Parse(literal.Substring(4, 2));
            if (day < 1 || day > 31) return null;
            var date = CreateDate(2000 + int.Parse(yr), month, day);
            if (date == null || date > DateTime.Now)
                date = CreateDate(1900 + int.Parse(yr), month, day);
            return date;
        }

        (bool valid, string reason) Validate()
        {
            if (!HasLength) return (false, "The length of the ID number is not correct");
            if (!IsNumeric) return (false, "The ID number can only contain numeric digits");
            if (DateOfBirth == null) return (false, "The date of birth is not valid");
            if (!ValidateArray()) return (false, "ID number does not validate");
            return (true, null);
        }

        bool ValidateArray()
        {
            var numbers = literal.Select(c => int.Parse(c.ToString())).ToArray();
            var odds = numbers.Where(OddPredicate).Aggregate(Reduction);
            var evens = numbers.Where(EvenPredicate).Select(EvenMap).Aggregate(Reduction);
            var nCheck = odds + evens;
            return (nCheck % 10) == 0;

            bool OddPredicate(int n, int i) => i % 2 == 0;
            bool EvenPredicate(int n, int i) => i % 2 != 0;
            int EvenMap(int n) => ((n *= 2) > 9) ? n - 9 : n;
            int Reduction(int a, int b) => a + b;
        }
    }
}
