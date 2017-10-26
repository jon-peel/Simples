namespace Simples
{
    public class Coordinate
    {
        Degree longitude;
        Degree latitude;

        Coordinate(Degree longitude, Degree latitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public override string ToString()
        => $"{longitude} {(longitude.Negative ? "S" : "N")}, {latitude} {(latitude.Negative ? "W" : "E")}";

        public static implicit operator Coordinate((Degree longitude, Degree latitude) temp) => new Coordinate(temp.longitude, temp.latitude);
        public static implicit operator string(Coordinate coordinate) => coordinate.ToString();
    }
}
