using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary
{
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

        public static IAlgmEncoded<string, IAlgmEncoded<int, Dictionary<char, int>>> Encode(string source)
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
            string answer = "0";
            if (LowRange != 0)
            {
                int[] number = new int[28];
                string str = LowRange.ToString();
                int cnt = 0;
                for (int i = 2; i < str.Length; i++)
                    number[cnt++] = int.Parse(str[i].ToString());
                int k = 1;
                decimal result = Convert.ToDecimal(convertToString(number, k));
                while (!(result >= LowRange && result < HighRange))
                {
                    if (result < LowRange)
                        number[k - 1]++;
                    if (result >= HighRange)
                    {
                        number[k - 1] = int.Parse(str[k + 1].ToString());
                        k++;
                    }
                    result = Convert.ToDecimal(convertToString(number, k));
                }
                answer = "";
                for (int i = 0; i < k; i++)
                    answer += number[i].ToString();
            }

            double compressionRatio = CalculateCompressionRatio(source, answer);
            return new EncodedMessage<string, IAlgmEncoded<int, Dictionary<char, int>>>(answer, new EncodedMessage<int, Dictionary<char, int>>(source.Length, GetFrequencies(source), compressionRatio), compressionRatio);
        }
        private static string convertToString(int[] number, int index)
        {
            string str = "0,";
            for (int i = 0; i < index; i++)
                str += number[i].ToString();
            return str;
        }
        public static IAlgmEncoded<string> Decode(Dictionary<char, int> frequencies, string encoded, int CountOfAllSymbols)
        {
            List<Symbol> codes = GetSymbolsRanges(frequencies, CountOfAllSymbols);
            StringBuilder decoded = new StringBuilder(string.Empty);

            //decimal code = int.Parse(encoded) / (decimal)Math.Pow(10, encoded.Length);
            decimal code = Convert.ToDecimal("0,"+encoded);
            decimal HighRange = 1, LowRange = 0, h, l;
            for (int i = 0; i < CountOfAllSymbols; i++)
            {
                h = HighRange; l = LowRange;
                Symbol item = codes.Find(x => l + (h - l) * x.HighRange > code && l + (h - l) * x.LowRange <= code);
                decoded.Append(item.Data);
                HighRange = l + (h - l) * item.HighRange;
                LowRange = l + (h - l) * item.LowRange;
            }

            var decodedString = decoded.ToString();
            return new EncodedMessage<string>(decodedString, CalculateCompressionRatio(decodedString, encoded));
        }
        private static double CalculateCompressionRatio(string sourceString, string compressionString)
        {
            return Math.Round((double)((sourceString.Length * 8)/Convert.ToString(compressionString.Length, 2).Length), 3);
        }
    }
}
