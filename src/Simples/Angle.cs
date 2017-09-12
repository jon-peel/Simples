using System;

namespace Simples
{
    public class Angle
    {
        readonly decimal value;

        Angle(decimal from)
        {
            value = Math.Abs(from);
            Negative = from < 0;
        }

        Angle(double from) : this((decimal)from) { }

        public bool Negative { get; }
        public int Degrees => (int)Math.Floor(value);
        public int Minutes => (int)Math.Floor((value - Degrees) * 60);
        public decimal Seconds => (value - Degrees - (decimal)Minutes / 60) * 3600;

        public override string ToString() => $"{Degrees}\u00B0 {Minutes}\u2032 {Seconds:0.00}\u2033";

        public static implicit operator Angle(double from) => new Angle(from);
        public static implicit operator Angle(decimal from) => new Angle(from);
        public static implicit operator decimal(Angle from) => from.value;
        public static implicit operator double(Angle from) => (double)from.value;
        public static implicit operator string(Angle from) => from.ToString();
    }
}
