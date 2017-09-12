using System;

namespace Simples
{
    public class Degree
    {
        decimal value;

        Degree(decimal from) => value = from;
        Degree(double from) : this((decimal)from) { }

        public int Degrees => (int)Math.Floor(Math.Abs(value));
        public int Minutes => (int)Math.Floor((value - Degrees) * 60);
        public decimal Seconds => (value - Degrees - (decimal)Minutes / 60) * 3600;

        public override string ToString() => $"{Degrees}\u00B0 {Minutes}\u2032 {Seconds:0.00}\u2033";

        public static implicit operator Degree(double from) => new Degree(from);
        public static implicit operator Degree(decimal from) => new Degree(from);
        public static implicit operator decimal(Degree from) => from.value;
        public static implicit operator double(Degree from) => (double)from.value;
        public static implicit operator string(Degree from) => from.ToString();
    }
}
