using Simples;
using Xunit;

namespace SimplesTests
{
    public class CoordinateTests
    {
        [Theory]
        [InlineData(-26.2041, 28.0473, "26\u00B0 12\u2032 14.76\u2033 S, 28\u00B0 2\u2032 50.28\u2033 E")]
        public void Coordinate_Angle_ToString(decimal longitude, decimal latitude, string expected)
        {
            Coordinate coordinate = (longitude, latitude);
            var result = coordinate.ToString();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(26.2041, 28.0473)]
        [InlineData(-26.2041, -28.0473)]
        public void Coordinate_Long_Lat(decimal longitude, decimal latitude)
        {
            Coordinate coordinate = (longitude, latitude);
            Assert.Equal<decimal>(longitude, coordinate.Longitude);
            Assert.Equal<decimal>(latitude, coordinate.Latitude);
        }
    }
}