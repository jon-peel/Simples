using System;
using System.Linq;

namespace Simples.SouthAfrican
{
    public class IdNumber
    {
        readonly string literal;

        IdNumber(string literalIdNumber) => literal = literalIdNumber.Length > 13 ? literalIdNumber.Substring(0, 13) : literalIdNumber;
        public static implicit operator IdNumber(string literal) => new IdNumber(literal);
        public static implicit operator string(IdNumber idNumber) => idNumber.literal;

        public DateTime? DateOfBirth => GetDateOfBirth();
        public Gender Gender => HasLength && IsNumberic && int.Parse(literal[5].ToString()) >= 5;
        public bool SouthAfrican => HasLength && IsNumberic && int.Parse(literal[10].ToString()) == 0;
        public bool IsValid => Validate().valid;

        bool HasLength => literal.Length == 13;
        bool IsNumberic => ulong.TryParse(literal, out var idv);

        (bool valid, string reason) Validate()
        {
            if (!HasLength) return (false, "The length of the ID number is not correct");
            if (!IsNumberic) return (false, "The ID number can only contain numeric digits");
            if (DateOfBirth == null) return (false, "The date of birth is not valid");
            if (!ValidateArray()) return (false, "ID number does not validate");
            return (true, null);
        }

        DateTime? GetDateOfBirth()
        {
            if (!HasLength) return null;
            if (!IsNumberic) return null;
            var yr = literal.Substring(0, 2);
            var month = int.Parse(literal.Substring(2, 2));
            if (month < 1 || month > 12) return null;
            var day = int.Parse(literal.Substring(4, 2));
            if (day < 1 || day > 31) return null;
            var year = "19" + yr;
            try
            {
                var date = new DateTime(int.Parse(year), month, day);
                return date;
            }
            catch (Exception)
            {
                return null;
            }
        }

        bool ValidateArray()
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
