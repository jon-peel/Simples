using System;
using System.Globalization;

namespace Simples.SouthAfrican
{
    public readonly struct VehicleLicence
    {
        public string RegistrationNumber { get; }
        public Tweetalig Type { get; }
        public string Make { get; }
        public string Model { get; }
        public Tweetalig Colour { get; }
        public string VehicleIdentificationNumber { get; }
        VehicleLicence(string registrationNumber, string type, string make, string model, string colour, DateTime expiryDate, string vehicleIdentificationNumber)
        {
            RegistrationNumber = registrationNumber;
            Type = Tweetalig.Parse(type);
            Make = make;
            Model = model;
            Colour = Tweetalig.Parse(colour);
            ExpiryDate = expiryDate;
            VehicleIdentificationNumber = vehicleIdentificationNumber;
        }
        public static VehicleLicence Parse(string licenceDisk)
        {
            if (!string.IsNullOrEmpty(licenceDisk))
            {
                var fields = licenceDisk.Split('%');
                if (fields.Length >= 15 && DateTime.TryParseExact(fields[14], "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var expiryDate))
                {
                    return new VehicleLicence(fields[6], fields[8], fields[9], fields[10], fields[11], expiryDate, fields[12]);
                }
            }
            throw new FormatException("Input string is not in the correct format");
        }

        public DateTime ExpiryDate { get; }
        /*
        0: 
        1: MVL1CC97
        2: 0160
        3: 4025T0BL
        4: 1
        5: 4025012X3BR4
        6: JCT416GP
        7: DDN201W
        8: Sedan (closed top) / Sedan (toe-kap)
        9: TOYOTA
       10: CONQUEST
       11: White / Wit
       12: AHT54EE9009953744
       13: 2E3157067
       14: 2018-03-31
       */
    }
}
