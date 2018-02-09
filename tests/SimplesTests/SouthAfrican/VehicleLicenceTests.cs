using Simples.SouthAfrican;
using System;
using Xunit;

namespace SimplesTests.SouthAfrican
{
    public class VehicleLicenceTests
    {
        const string licenceDisk = "%MVL1CC97%0160%4025T0BL%1%4025012X3BR4%JCT416GP%DDN201W%Sedan (closed top) / Sedan (toe-kap)%TOYOTA%CONQUEST%White / Wit%AHT54EE9009953744%2E3157067%2018-03-31%";

        [Fact]
        public void Parse_Invalid_Exception()
        {
            Assert.Throws<FormatException>(() => VehicleLicence.Parse(""));
        }

        [Fact]
        public void Parse_HasRegistration()
        {
            var licence = VehicleLicence.Parse(licenceDisk);
            Assert.Equal("JCT416GP", licence.RegistrationNumber);
        }

        [Fact]
        public void Parse_HasType()
        {
            var licence = VehicleLicence.Parse(licenceDisk);
            Assert.Equal("Sedan (closed top)", licence.Type);
        }

        [Fact]
        public void Parse_Make()
        {
            var licence = VehicleLicence.Parse(licenceDisk);
            Assert.Equal("TOYOTA", licence.Make);
        }

        [Fact]
        public void Parse_Model()
        {
            var licence = VehicleLicence.Parse(licenceDisk);
            Assert.Equal("CONQUEST", licence.Model);
        }

        [Fact]
        public void Parse_Colour()
        {
            var licence = VehicleLicence.Parse(licenceDisk);
            Assert.Equal("White", licence.Colour);
        }

        [Fact]
        public void Parse_BadDate_Exeption()
        {
            var badDateString = licenceDisk.Replace("2018-03-31", "2018-02-31");
            Assert.Throws<FormatException>(() => VehicleLicence.Parse(badDateString));
        }

        [Fact]
        public void Parse_ExpiryDate()
        {
            var licence = VehicleLicence.Parse(licenceDisk);
            Assert.Equal(new DateTime(2018, 03, 31), licence.ExpiryDate);
        }

        [Fact]
        public void Parse_VehicleIdentificationNumber()
        {
            var licence = VehicleLicence.Parse(licenceDisk);
            Assert.Equal("AHT54EE9009953744", licence.VehicleIdentificationNumber);
        }
    }
}
