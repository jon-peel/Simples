using System;

namespace Simples
{
    public class Gender : IEquatable<Gender>
    {
        readonly bool male;
        Gender(bool isMale) => male = isMale;
        public static implicit operator Gender(bool male) => new Gender(male);
        public static implicit operator Gender(string literal) => new Gender(literal.ToUpper().Equals("Male"));
        public static implicit operator bool(Gender gender) => gender.male;
        public static implicit operator string(Gender gender) => gender.ToString();

        public override string ToString() => male ? "Male" : "Female";
        public bool Equals(Gender other) => male == other.male;
    }
}
