using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary
{
    public class Symbol : IComparable<Symbol>
    {
        public char Data { get; set; }
        private decimal lowRange;
        public decimal LowRange {
            get { return lowRange; } set { lowRange = value; HighRange = value + Frequency; } }
        public decimal HighRange { get; private set; }
        public decimal Frequency { get; set; }
        public Symbol(char data, decimal frequency)
        {
            Data = data;
            Frequency = frequency;
        }
        public Symbol(char data, decimal frequency, decimal lowRange)
        {
            Data = data;
            Frequency = frequency;
            LowRange = lowRange;
        }
        public int CompareTo(Symbol other)
        {
            return Frequency.CompareTo(other.Frequency);
        }
        
    }
    public static class ArithmeticCodingAlgm
    {
        private static Dictionary<char, int> GetFrequencies(string source)
        {
            Dictionary<char, int> frequencies = new Dictionary<char, int>();

            foreach (char c in source)
            {
                if (frequencies.ContainsKey(c))
                    frequencies[c]++;
                else
                    frequencies.Add(c, 1);
            }
            return frequencies;
        }
        private static List<Symbol> GetSymbolsRanges(string source)
        {
            var frequencies = GetFrequencies(source);

            return GetSymbolsRanges(frequencies, source.Length);
        }
        private static List<Symbol> GetSymbolsRanges(Dictionary<char, int> frequencies, int CountOfAllSymbols)
        {

            decimal frequencyOfOneSymbol = (decimal)1 / CountOfAllSymbols;
            var nodes = frequencies.Select(x => new Symbol(x.Key, x.Value * frequencyOfOneSymbol)).ToList();

            nodes.Sort();
            nodes.Reverse();

            decimal current = 0;
            foreach (var item in nodes)
            {
                item.LowRange = current;
                current = item.HighRange;
            }
            return nodes;
        }

        public static string Encode(string source)
        {
            List<Symbol> codes = GetSymbolsRanges(source);
            decimal HighRange = 1, LowRange = 0, h, l;
            foreach (char c in source)
            {
                Symbol item = codes.Find(x => x.Data == c);
                h = HighRange; l = LowRange;
                HighRange = l + (h - l) * item.HighRange;
                LowRange = l + (h - l) * item.LowRange;
            }
            decimal dec = 10;
            decimal result = (int)(HighRange * dec) / dec;
            while (!(result >= LowRange && result < HighRange))
            {
                dec *= 10;
                result = (int)(HighRange * dec) / dec;
            }
            return ((int)(result*dec)).ToString();
        }
        public static string Decode(Dictionary<char, int> frequencies, string encoded, int CountOfAllSymbols)
        {
            List<Symbol> codes = GetSymbolsRanges(frequencies, CountOfAllSymbols);
            StringBuilder decoded = new StringBuilder(string.Empty);

            decimal code = int.Parse(encoded) / (decimal)Math.Pow(10, encoded.Length);
            decimal HighRange = 1, LowRange = 0, h, l;
            for (int i=0; i< CountOfAllSymbols; i++)
            {
                h = HighRange; l = LowRange;
                Symbol item = codes.Find(x => l + (h - l) *x.HighRange>code && l + (h - l) *x.LowRange<=code);
                decoded.Append(item.Data);
                HighRange = l + (h - l) * item.HighRange;
                LowRange = l + (h - l) * item.LowRange;
            }

            return decoded.ToString();
        }
    }
}
