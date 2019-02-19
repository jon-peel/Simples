using Xunit;
using Simples;

namespace SimplesTests
{
    public class ByteArrayTests
    {
        [Fact]
        public void ToHexString_SingleZero()
        {
            var data = new byte[] { 0 };
            var hex = data.ToHexString();
            Assert.Equal("00", hex);
        }

        [Fact]
        public void ToHexString_Double255()
        {
            var data = new byte[] { byte.MaxValue, byte.MaxValue };
            var hex = data.ToHexString();
            Assert.Equal("ffff", hex);
        }

        [Fact]
        public void ToBase64_SingleZero()
        {
            var data = new byte[] { 0 };
            var hex = data.ToBase64();
            Assert.Equal("AA==", hex);
        }

        [Fact]
        public void ToBase64_Double255()
        {
            var data = new byte[] { byte.MaxValue, byte.MaxValue };
            var hex = data.ToBase64();
            Assert.Equal("//8=", hex);
        }
    }
}
