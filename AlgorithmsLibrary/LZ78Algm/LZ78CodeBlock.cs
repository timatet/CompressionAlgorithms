using System;

namespace AlgorithmsLibrary
{
    public class LZ78CodeBlock : IEquatable<LZ78CodeBlock>
    {
        public int Position { get; private set; }
        public char Char { get; private set; }

        public LZ78CodeBlock(int Position, char Char)
        {
            this.Position = Position;
            this.Char = Char;
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", Position, Char);
        }

        public bool Equals(LZ78CodeBlock other)
        {
            return Position == other.Position && Char == other.Char;
        }
    }
}
