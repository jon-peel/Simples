using Simples.SouthAfrican;
using System.Collections.Generic;
using Xunit;

namespace SimplesTests.SouthAfrican
{
    public class TweetaligTests
    {
        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Parse_HasEnglish(string full, string eng, string afr)
        {
            var result = Tweetalig.Parse(full);
            Assert.Equal(eng, result);
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Parse_HasAfrikaans(string full, string eng, string afr)
        {
            var result = Tweetalig.Parse(full);
            Assert.Equal(afr, result.Afrikaans);
        }

        [Theory]
        [MemberData(nameof(GetTestData))]
        public void Parse_HasBoth(string full, string eng, string afr)
        {
            var result = Tweetalig.Parse(full);
            Assert.Equal(full, result.GetBoth());
        }

        public static IEnumerable<object[]> GetTestData()
        {
            yield return new[] { "Yes / Ja", "Yes", "Ja" };
            yield return new[] { "Sedan (closed top) / Sedan (toe-kap)", "Sedan (closed top)", "Sedan (toe-kap)" };
            yield return new[] { "White / Wit", "White", "Wit" };
        }
    }
}
