using Simples;
using Simples.SouthAfrican;
using System;
using Xunit;

namespace SimplesTests.SouthAfrican
{
    public class IdNumberTests
    {
        const string myId = "8401295047080";

        [Fact]
        public void IdNumber_NotMoreThan13()
        {
            IdNumber id = "12345678901234567890";
            string literal = id;
            Assert.Equal(13, literal.Length);
        }

        [Fact]
        public void DateOfBirth_Valid()
        {
            IdNumber id = myId;
            var expectedDate = new DateTimeOffset(1984, 01, 29, 0, 0, 0, TimeSpan.FromHours(2));
            var dateOfBirth = id.DateOfBirth;
            Assert.Equal(expectedDate.LocalDateTime, dateOfBirth);
        }

        [Fact]
        public void Gender_IsMale()
        {
            IdNumber id = myId;
            Assert.Equal((Gender)true, id.Gender);
        }

        [Fact]
        public void SouthAfrican_True()
        {
            IdNumber id = myId;
            Assert.True(id.SouthAfrican);
        }

        [Fact]
        public void DateOfBirth_InValid()
        {
            IdNumber id = "0000000000000";
            var dateOfBirth = id.DateOfBirth;
            Assert.Null(dateOfBirth);
        }

        [Fact]
        public void Validate_IsValid()
        {
            IdNumber id = myId;
            Assert.True(id.IsValid);
        }

        [Fact]
        public void Validate_ToShort()
        {
            IdNumber id = "000000000000";
            Assert.False(id.IsValid);
        }

        [Fact]
        public void Validate_NoDate()
        {
            IdNumber id = "0000000000000";
            Assert.False(id.IsValid);
        }

        [Fact]
        public void Validate_ContainsLetters()
        {
            IdNumber id = "8401295047A7A";
            Assert.False(id.IsValid);
        }

        [Theory]
        [InlineData("4006300395087", "1940-06-29T22:00:00.000Z", false, true)]
        [InlineData("4709020184083", "1947-09-01T22:00:00.000Z", false, true)]
        [InlineData("4812295009181", "1948-12-28T22:00:00.000Z", true, false)]
        [InlineData("5201045738084", "1952-01-03T22:00:00.000Z", true, true)]
        [InlineData("5309280385089", "1953-09-27T22:00:00.000Z", false, true)]
        [InlineData("5411270786084", "1954-11-26T22:00:00.000Z", false, true)]
        [InlineData("5503035365087", "1955-03-02T22:00:00.000Z", true, true)]
        [InlineData("6108215079083", "1961-08-20T22:00:00.000Z", true, true)]
        [InlineData("6212252193086", "1962-12-24T22:00:00.000Z", false, true)]
        [InlineData("6410170770080", "1964-10-16T22:00:00.000Z", false, true)]
        [InlineData("6911240308082", "1969-11-23T22:00:00.000Z", false, true)]
        [InlineData("7005025451081", "1970-05-01T22:00:00.000Z", true, true)]
        [InlineData("7009110689084", "1970-09-10T22:00:00.000Z", false, true)]
        [InlineData("7103060835084", "1971-03-05T22:00:00.000Z", false, true)]
        [InlineData("7202156107083", "1972-02-14T22:00:00.000Z", true, true)]
        [InlineData("7207016182184", "1972-06-30T22:00:00.000Z", true, false)]
        [InlineData("7408270297086", "1974-08-26T22:00:00.000Z", false, true)]
        [InlineData("7907175148088", "1979-07-16T22:00:00.000Z", true, true)]
        [InlineData("8101270160086", "1981-01-26T22:00:00.000Z", false, true)]
        [InlineData("8205100657086", "1982-05-09T22:00:00.000Z", false, true)]
        [InlineData("8302180857080", "1983-02-17T22:00:00.000Z", false, true)]
        [InlineData("8401295047080", "1984-01-28T22:00:00.000Z", true, true)]
        [InlineData("8412065666082", "1984-12-05T22:00:00.000Z", true, true)]
        [InlineData("8511180765081", "1985-11-17T22:00:00.000Z", false, true)]
        [InlineData("9408300571086", "1994-08-29T22:00:00.000Z", false, true)]
        [InlineData("9805140397087", "1998-05-13T22:00:00.000Z", false, true)]
        [InlineData("1010270108085", "2010-10-26T22:00:00.000Z", false, true)]
        public void Validate_AllValid(string idNumber, string dateOfBirth, bool gender, bool southAfrican)
        {
            IdNumber id = idNumber;
            Assert.True(id.IsValid);
            Assert.Equal(DateTime.Parse(dateOfBirth), id.DateOfBirth);
            Assert.Equal((Gender)gender, id.Gender);
            Assert.Equal(southAfrican, id.SouthAfrican);
        }
    }
}