using AlgorithmsLibrary.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary
{
    public static class ShannonFanoAlgm
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
        private static Dictionary<char, string> GetShannonFanoCodes(string source)
        {
            var Frequencies = GetFrequencies(source);

            return GetShannonFanoCodes(Frequencies);
        }
        private static DoublyNode<char> BuildTree(DoublyNode<char> parent, int leftBorder, int rightBoder, List<DoublyNode<char>> nodes)
        {
            //если на отрезке не один элемент
            if (leftBorder != rightBoder)
            {
                int l = leftBorder;
                int sumLeftPart = nodes[l++].Weight, sumRightPart = parent.Weight - sumLeftPart;
                // делим относительно веса попалам
                while (sumLeftPart < sumRightPart)
                {
                    sumLeftPart += nodes[l++].Weight;
                    sumRightPart = parent.Weight - sumLeftPart;
                }
                //рекурсивно проходимся по каждой из частей
                var node1 = BuildTree(new DoublyNode<char>(default, sumLeftPart), leftBorder, l - 1, nodes);
                var node2 = BuildTree(new DoublyNode<char>(default, sumRightPart), l, rightBoder, nodes);
                parent = new DoublyNode<char>(node1, node2);
            }
            else //один элемент
            {
                parent = nodes[leftBorder];
            }
            return parent;
        }

        private static Dictionary<char, string> GetShannonFanoCodes(Dictionary<char, int> frequencies)
        {
            //получаем словарь частот и переносим его в список
            var nodes = frequencies.Select(x => new DoublyNode<char>(x.Key, x.Value)).ToList();

            nodes.Sort();
            nodes.Reverse();

            //строим дерево по алгоритму Фано
            //поскольку список узлов с частотами упорядочен, то идя с начала находим, 
            //где список можно поделить попалам (относительно веса), продолжаем, пока не дойдем до листа
            var parent = new DoublyNode<char>(default, nodes.Sum(x => x.Weight));
            var root = BuildTree(parent, 0, nodes.Count - 1, nodes);

            //обходим поулчившееся дерево, приписывая каждому символу свой код
            return root.InOrderTraversal();
        }


        /// <summary>
        /// Encoding. The algorithm compile a dictionary of frequencies from the source string, 
        /// then builds a tree on them and gets a dictionary with ShannonFano codes.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Encoded string.</returns>
        public static IAlgmEncoded<string, Dictionary<char, string>> Encode(string source)
        {
            StringBuilder encoded = new StringBuilder(string.Empty);
            Dictionary<char, string> codes = GetShannonFanoCodes(source);

            foreach (char c in source)
                encoded.Append(codes[c]);

            var encodedString = encoded.ToString();
            return new EncodedMessage<string, Dictionary<char, string>>(encodedString, codes, CalculateCompressionRatio(source, encodedString));
        }

        private static Dictionary<string, char> GetReverseCodes(Dictionary<char, string> codes)
        {
            return codes.ToDictionary(x => x.Value, x => x.Key);
        }
        /// <summary>
        /// Decoding, if a frequency dictionary is available. The algorithm will first generate ShannonFano codes, 
        /// then decode source string.
        /// </summary>
        /// <param name="frequencies">A dictionary where each character has its own frequency.</param>
        /// <param name="encoded">Encoded string.</param>
        /// <returns>Decoded string.</returns>
        public static IAlgmEncoded<string> Decode(Dictionary<char, int> frequencies, string encoded)
        {
            var codesForDecoding = GetShannonFanoCodes(frequencies);

            return Decode(codesForDecoding, encoded);
        }

        /// <summary>
        /// Decoding, if ShannonFano codes are known.
        /// </summary>
        /// <param name="codes">A dictionary where each character corresponds to its code.</param>
        /// <param name="encoded">Encoded string.</param>
        /// <returns>Decoded string.</returns>
        public static IAlgmEncoded<string> Decode(Dictionary<char, string> codes, string encoded)
        {
            StringBuilder decoded = new StringBuilder(string.Empty);
            var codesForDecoding = GetReverseCodes(codes);

            string buffer = string.Empty;
            foreach (char c in encoded)
            {
                buffer += c;
                if (codesForDecoding.ContainsKey(buffer))
                {
                    decoded.Append(codesForDecoding[buffer]);
                    buffer = string.Empty;
                }
            }

            var decodedString = decoded.ToString();
            return new EncodedMessage<string>(decodedString, CalculateCompressionRatio(decodedString, encoded));
        }

        /// <summary>
        /// Calculation of the compression ratio.
        /// </summary>
        /// <param name="sourceString">Source string.</param>
        /// <param name="compressionString">Compression string.</param>
        /// <returns>Compression ratio.</returns>
        private static double CalculateCompressionRatio(string sourceString, string compressionString)
        {
            double countBitsSourceString = 8 * sourceString.Length;
            double countBitsCompressionString = compressionString.Length;

            return Math.Round(countBitsSourceString / countBitsCompressionString, 3);
        }
    }
}
