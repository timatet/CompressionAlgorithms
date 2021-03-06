using System;

namespace AlgorithmsLibrary
{
    public class RLECodeBlock : IEquatable<RLECodeBlock>
    {
        public char Symbol { get; private set; }
        public int Repeats { get; private set; }

        public RLECodeBlock(char symbol, int countrepeats)
        {
            this.Symbol = symbol;
            this.Repeats = countrepeats;
        }

        public override string ToString()
        {
            if (Symbol == default)
                return string.Format("{0}", Repeats);
            else
                return "{" + Symbol + "," + Repeats + "}";
        }

        public bool Equals(RLECodeBlock other)
        {
            return Symbol == other.Symbol && Repeats == other.Repeats;
        }
    }
}
