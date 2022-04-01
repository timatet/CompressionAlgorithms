using System;

namespace AlgorithmsLibrary
{
    /// <summary>
    /// Метка. Кодовый блок, состоящий из компонетов: смещение, длина, следующий символ.
    /// </summary>
    public class CodeBlock : IEquatable<CodeBlock>
    {
        public int Offset { get; private set; }
        public int Length { get; private set; }
        public char NextChar { get; private set; }

        public CodeBlock(int offset, int length, char nextChar)
        {
            this.Offset = offset;
            this.Length = length;
            this.NextChar = nextChar;
        }

        public override string ToString()
        {
            return string.Format("({0},{1},{2})", Offset, Length, NextChar);
        }

        public bool Equals(CodeBlock other)
        {
            return Offset == other.Offset && Length == other.Length && NextChar == other.NextChar;
        }
    }
}
