using System;
using System.Linq;

namespace Simples.SouthAfrican
{
    public readonly struct IdNumber
    {
        private readonly string literal;

        private IdNumber(string literalIdNumber) => literal = literalIdNumber.Length > 13 ? literalIdNumber.Substring(0, 13) : literalIdNumber;

        public DateTime? DateOfBirth => GetDateOfBirth();

        public Gender Gender => HasLength && IsNumberic && int.Parse(literal[5].ToString()) >= 5;

        public bool IsValid => Validate().valid;

        public bool SouthAfrican => HasLength && IsNumberic && int.Parse(literal[10].ToString()) == 0;

        private bool HasLength => literal.Length == 13;

        private bool IsNumberic => ulong.TryParse(literal, out var idv);

        public static implicit operator IdNumber(string literal) => new IdNumber(literal);

        public static implicit operator string(IdNumber idNumber) => idNumber.ToString();

        public override string ToString() => literal;

        private static DateTime? CreateDate(int year, int month, int day)
        {
            try
            {
                return new DateTime(year, month, day);
            }
            catch (Exception) { return null; }
        }

        private DateTime? GetDateOfBirth()
        {
            if (!HasLength) return null;
            if (!IsNumberic) return null;
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

        private (bool valid, string reason) Validate()
        {
            if (!HasLength) return (false, "The length of the ID number is not correct");
            if (!IsNumberic) return (false, "The ID number can only contain numeric digits");
            if (DateOfBirth == null) return (false, "The date of birth is not valid");
            if (!ValidateArray()) return (false, "ID number does not validate");
            return (true, null);
        }

        private bool ValidateArray()
        {
            var numbers = literal.Select(c => int.Parse(c.ToString()));
            Func<int, int, bool> oddPredecate = (n, i) => i % 2 == 0;
            Func<int, int, bool> evenPredicate = (n, i) => i % 2 != 0;
            Func<int, int> evenMap = (n) => ((n *= 2) > 9) ? n - 9 : n;
            Func<int, int, int> reduction = (a, b) => a + b;
            var odds = numbers.Where(oddPredecate).Aggregate(reduction);
            var evens = numbers.Where(evenPredicate).Select(evenMap).Aggregate(reduction);
            var nCheck = odds + evens;
            return (nCheck % 10) == 0;
        }
    }
}
