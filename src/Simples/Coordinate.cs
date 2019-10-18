namespace Simples
{
    public readonly struct Coordinate
    {
        public Degree Longitude { get; }
        public Degree Latitude { get; }

        public Coordinate(decimal longitude, decimal latitude)
        {
            Latitude = new Degree(latitude);
            Longitude = new Degree(longitude);
        }

        public Coordinate(Degree longitude, Degree latitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        => $"{Longitude} {(Longitude.Negative ? "S" : "N")}, {Latitude} {(Latitude.Negative ? "W" : "E")}";

        public static implicit operator Coordinate((Degree longitude, Degree latitude) temp) => new Coordinate(temp.longitude, temp.latitude);
        public static implicit operator string(Coordinate coordinate) => coordinate.ToString();
    }
}
