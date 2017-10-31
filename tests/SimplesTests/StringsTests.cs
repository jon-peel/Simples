using Simples;
using Xunit;

namespace SimplesTests
{
    public class CharStringTests
    {
        [Theory]
        [InlineData("A", "B")]
        [InlineData("C", "D")]
        [InlineData("E", "F")]
        [InlineData("G", "H")]
        [InlineData("I", "J")]
        [InlineData("K", "L")]
        [InlineData("M", "N")]
        [InlineData("O", "P")]
        [InlineData("Q", "R")]
        public void Inc_Single(string start, string expected) => Check(start, expected);

        [Theory]
        [InlineData("AA", "AB")]
        [InlineData("GG", "GH")]
        public void Inc_Multi(string start, string expected) => Check(start, expected);

        [Theory]
        [InlineData("AZ", "BA")]
        [InlineData("GZ", "HA")]
        public void Inc_Roll(string start, string expected) => Check(start, expected);

        [Theory]
        [InlineData("AZZ", "BAA")]
        [InlineData("GZZ", "HAA")]
        public void Inc_MultiRoll(string start, string expected) => Check(start, expected);

        [Theory]
        [InlineData("ZZ", "AAA")]
        [InlineData("ZZZ", "AAAA")]
        public void Inc_FullRoll(string start, string expected) => Check(start, expected);

        void Check(CharString start, CharString expected) => Assert.Equal(expected, ++start);
    }
}
