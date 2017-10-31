using System;
using System.Linq;

namespace Simples
{
    public class CharString : IComparable<CharString>, IComparable<string>
    {
        readonly string inner;

        public override string ToString() => inner;
        public int CompareTo(CharString other) => CompareTo(other.inner);
        public int CompareTo(string other) => string.Compare(inner, other, StringComparison.Ordinal);

        CharString(string from) => inner = from.ToUpper();
        CharString(char[] from) => inner = new CharString(new string(from));

        public static implicit operator CharString(string from) => new CharString(from);
        public static implicit operator CharString(char[] from) => new CharString(from);
        public static implicit operator string(CharString from) => from.ToString();

        public static CharString operator ++(CharString @this)
        {
            if (string.IsNullOrEmpty(@this)) return "A";
            var chars = @this.inner.ToCharArray();
            var i = chars.Length - 1;
            if (++chars[i] <= 'Z') return chars;
            CharString fore = chars.Take(i).ToArray();
            return ++fore + "A";
        }
    }
}
