using System.Linq;

namespace Simples.SouthAfrican
{
    public readonly struct Tweetalig
    {
        public string English { get; }
        public string Afrikaans { get; }
        public string GetBoth() => $"{English} / {Afrikaans}";
        Tweetalig(string eng, string afr)
        {
            English = eng;
            Afrikaans = afr;
        }

        public static Tweetalig Parse(string full)
        {
            var dual = full?.Split('/', '\\', '|').Select(v => v.Trim()).ToArray() ?? new string[0];
            var eng = dual.Any() ? dual[0] : null;
            var afr = dual.Length > 1 ? dual[1] : eng;
            return new Tweetalig(eng, afr);
        }

        public static implicit operator string(Tweetalig @this) => @this.English;
        public override string ToString() => this;
    }
}
