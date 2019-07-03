namespace Simples
{
    public readonly struct Degree
    {
        readonly decimal value;

        Degree(decimal from)
        {
            value = from.Abs();
            Negative = from < 0;
        }

        Degree(double from) : this((decimal)from)
        {
        }

        public bool Negative { get; }
        public int Degrees => value.Floor();
        public int Minutes => ((value - Degrees) * 60).Floor();
        public decimal Seconds => (value - Degrees - (decimal)Minutes / 60) * 3600;

        public override string ToString() => $"{Degrees}\u00B0 {Minutes}\u2032 {Seconds:0.00}\u2033";

        public static implicit operator Degree(double from) => new Degree(from);

        public static implicit operator Degree(decimal from) => new Degree(from);

        public static implicit operator decimal(Degree from) => from.Negative ? 0 - from.value : from.value;

        public static implicit operator double(Degree from) => from.Negative ? 0D - (double)from.value : (double)from.value;

        public static implicit operator string(Degree from) => from.ToString();
    }
}
