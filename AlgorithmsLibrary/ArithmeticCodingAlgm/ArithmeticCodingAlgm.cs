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
            
            StringBuilder TheImmutablePart = new StringBuilder();

            foreach (char c in source)
            {
                Symbol item = codes.Find(x => x.Data == c);
                h = HighRange; l = LowRange;
                HighRange = l + (h - l) * item.HighRange;
                LowRange = l + (h - l) * item.LowRange;
                DiscardTheImmutablePart(ref TheImmutablePart, ref HighRange, ref LowRange);
            }

            // ищем оптимальное представление числа
            StringBuilder answer = new StringBuilder();
            answer.Append('0');
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
                    if (k == 28) break;
                }
                answer.Clear();
                answer.Append(TheImmutablePart);
                for (int i = 0; i < k; i++)
                    answer.Append(number[i].ToString());
            }

            double compressionRatio = CalculateCompressionRatio(source, answer.ToString());
            return new EncodedMessage<string, IAlgmEncoded<int, Dictionary<char, int>>>(answer.ToString(), new EncodedMessage<int, Dictionary<char, int>>(source.Length, GetFrequencies(source), compressionRatio), compressionRatio);
        }

        private static void DiscardTheImmutablePart(ref StringBuilder theImmutablePart, ref decimal highRange, ref decimal lowRange)
        {
            string lr = lowRange.ToString();
            string hr = highRange.ToString();
            int i=0;
            while (i<lr.Length && i<hr.Length && lr[i]==hr[i])
            {
                if (!((i == 0 && lr[i] == '0') || lr[i] == ','))
                {
                    theImmutablePart.Append(lr[i]);
                    highRange *= 10; highRange -= (int)highRange % 10;
                    lowRange *= 10; lowRange -= (int)lowRange % 10;
                }
                i++;
            }
        }

        private static void DiscardTheImmutablePart(ref decimal highRange, ref decimal lowRange, ref decimal code, ref int index, string encoded, 
            ref bool thePreviousDigitIsZero, ref int CntOfZero)
        {
            string lr = lowRange.ToString();
            string hr = highRange.ToString();
            string c = code.ToString();
            int i = 0, encodedLength = encoded.Length;
            while (i < lr.Length && i < hr.Length &&  i < c.Length && lr[i] == hr[i] && lr[i]==c[i])
            {
                if (!((i == 0 && lr[i] == '0') || lr[i] == ','))
                {
                    highRange *= 10; highRange -= (int)highRange % 10;
                    lowRange *= 10; lowRange -= (int)lowRange % 10;
                    code = Convert.ToDecimal("0," + (index<encodedLength?encoded.Substring(index, Math.Min(28, encodedLength - index)):"0" ));
                    index++;    
                }
                i++;
            }
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

            bool thePreviousDigitIsZero = false;
            int CntOfZero = 0;
            int IndexInEncodedString = 1;
            decimal code = Convert.ToDecimal("0," + encoded.Substring(0, Math.Min(28, encoded.Length)));
            //int IndexInEncodedString = 28;
            //if (encoded.Length < 28)
            //    IndexInEncodedString = encoded.Length - 1;
            //decimal code = Convert.ToDecimal("0," + encoded.Substring(0, IndexInEncodedString));
            decimal HighRange = 1, LowRange = 0, h, l;
            for (int i = 0; i < CountOfAllSymbols; i++)
            {
                h = HighRange; l = LowRange;
                Symbol item = codes.Find(x => l + (h - l) * x.HighRange > code && l + (h - l) * x.LowRange <= code);
                decoded.Append(item.Data);
                HighRange = l + (h - l) * item.HighRange;
                LowRange = l + (h - l) * item.LowRange;
                DiscardTheImmutablePart(ref HighRange, ref LowRange, ref code, ref IndexInEncodedString, encoded, ref thePreviousDigitIsZero, ref CntOfZero);
            }

            var decodedString = decoded.ToString();
            return new EncodedMessage<string>(decodedString, CalculateCompressionRatio(decodedString, encoded));
        }
        private static double CalculateCompressionRatio(string sourceString, string compressionString)
        {
            var t = compressionString.Length;
            if (t > 100000)
                return 0.0;

            //var log = Math.Ceiling(System.Numerics.BigInteger.Log(System.Numerics.BigInteger.Parse(compressionString), 2));
            //var bite = Math.Ceiling(log / 8);
            //var k1 = Math.Round(sourceString.Length / bite, 3);

            byte[] BigIArray = System.Numerics.BigInteger.Parse(compressionString).ToByteArray();
            string BinaryCode = string.Concat(BigIArray.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')).Reverse());
            var r = BinaryCode.Length;
            var k = Math.Round(Convert.ToDouble(((decimal)sourceString.Length * 8 / (decimal)BinaryCode.ToString().TrimStart('0').Length).ToString()), 3);
            return k;
            //return Math.Round((double)((sourceString.Length * 8) / Convert.ToString(compressionString.Length, 2).Length), 3);
        }
    }
}
