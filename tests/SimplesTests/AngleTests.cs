using Simples;
using Xunit;

namespace SimplesTests
{
    public class AngleTests
    {
        const decimal DefaultDecimalValue = 12.111M;
        const double DefaultFloatValue = 12.111;

        [Fact]
        public void Degree_FromDecimal()
        {
            Degree deg = DefaultDecimalValue;
            Assert.NotNull(deg);
        }

        [Fact]
        public void Degree_FromFloat()
        {
            Degree deg = DefaultFloatValue;
            Assert.NotNull(deg);
        }

        [Fact]
        public void Float_FromDegree()
        {
            Degree deg = DefaultFloatValue;
            double val = deg;
            Assert.Equal(DefaultFloatValue, val);
        }

        [Fact]
        public void Decimal_FromDegree()
        {
            Degree deg = DefaultFloatValue;
            decimal val = deg;
            Assert.Equal(DefaultDecimalValue, val);
        }

        [Theory]
        [InlineData(012.111000, 012, 06, 39.6000)]
        [InlineData(045.123877, 045, 07, 25.9572)]
        [InlineData(099.876544, 099, 52, 35.5584)]
        [InlineData(100.123832, 100, 07, 25.7952)]
        [InlineData(180.445522, 180, 26, 43.8792)]
        public void Degrees_Minute_Second(decimal value, int expectedDegrees, int expectedMinutes, decimal expectedSeconds)
        {
            Degree deg = value;
            Assert.False(deg.Negative);
            Assert.Equal(expectedDegrees, deg.Degrees);
            Assert.Equal(expectedMinutes, deg.Minutes);
            Assert.Equal(expectedSeconds, deg.Seconds, 4);
        }

        [Theory]
        [InlineData(-012.111000, 012, 06, 39.6000)]
        [InlineData(-045.123877, 045, 07, 25.9572)]
        [InlineData(-099.876544, 099, 52, 35.5584)]
        [InlineData(-100.123832, 100, 07, 25.7952)]
        [InlineData(-180.445522, 180, 26, 43.8792)]
        public void Degrees_Negative_Minute_Second(decimal value, int expectedDegrees, int expectedMinutes, decimal expectedSeconds)
        {
            Degree deg = value;
            Assert.True(deg.Negative);
            Assert.Equal(expectedDegrees, deg.Degrees);
            Assert.Equal(expectedMinutes, deg.Minutes);
            Assert.Equal(expectedSeconds, deg.Seconds, 4);
        }

        [Theory]
        [InlineData(012.111000, "12\u00B0 6\u2032 39.60\u2033")]
        [InlineData(045.123877, "45\u00B0 7\u2032 25.96\u2033")]
        [InlineData(099.876544, "99\u00B0 52\u2032 35.56\u2033")]
        [InlineData(100.123832, "100\u00B0 7\u2032 25.80\u2033")]
        [InlineData(180.445522, "180\u00B0 26\u2032 43.88\u2033")]
        public void Degrees_String(decimal value, string expectedString)
        {
            Degree deg = value;
            Assert.Equal(expectedString, deg.ToString());
            Assert.Equal(expectedString, deg);
        }
    }
}
