using AlgorithmsLibrary.CommonClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgorithmsLibrary
{
    public static class HuffmanAlgm
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
        private static Dictionary<char, string> GetHuffmanCodes(string source)
        {
            var Frequencies = GetFrequencies(source);

            return GetHuffmanCodes(Frequencies);
        }
        private static Dictionary<char, string> GetHuffmanCodes(Dictionary<char, int> frequencies)
        {
            //получаем словарь частот и переносим его в список
            var nodes = frequencies.Select(x => new DoublyNode<char>(x.Key, x.Value)).ToList();

            nodes.Sort();

            //строим дерево хаффмана
            //поскольку список узлов с частотами упорядочен, то выбираем два первых (наименьших) узла
            //объединяем их (вес нового узла = сумме вероятностей) и добавляем обратно в список
            //удаляя два выбранных сначала узла
            while (nodes.Count > 1)
            {
                DoublyNode<char> parent = new DoublyNode<char>(nodes[0], nodes[1]);
                nodes.RemoveRange(0, 2);
                var index = nodes.BinarySearch(parent);
                if (index < 0)
                    index = ~index;
                nodes.Insert(index, parent);
            }

            //обходим поулчившееся дерево, приписывая каждому символу свой код
            return nodes.FirstOrDefault().InOrderTraversal();
        }

        /// <summary>
        /// Encoding. The algorithm compile a dictionary of frequencies from the source string, 
        /// then builds a tree on them and gets a dictionary with Huffman codes.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Encoded string.</returns>
        public static IAlgmEncoded<string, Dictionary<char, string>> Encode(string source)
        {
            StringBuilder encoded = new StringBuilder(string.Empty);
            Dictionary<char, string> codes = GetHuffmanCodes(source);

            foreach (char c in source)
                encoded.Append(codes[c]);

            return new EncodedMessage<string, Dictionary<char, string>>(encoded.ToString(), codes);
        }

        private static Dictionary<string, char> GetReverseCodes(Dictionary<char, string> codes)
        {
            return codes.ToDictionary(x => x.Value, x => x.Key);
        }

        /// <summary>
        /// Decoding, if a frequency dictionary is available. The algorithm will first generate Huffman codes, 
        /// then decode source string.
        /// </summary>
        /// <param name="frequencies">A dictionary where each character has its own frequency.</param>
        /// <param name="encoded">Encoded string.</param>
        /// <returns>Decoded string.</returns>
        public static IAlgmEncoded<string> Decode(Dictionary<char, int> frequencies, string encoded)
        {
            var codesForDecoding = GetHuffmanCodes(frequencies);

            return Decode(codesForDecoding, encoded);
        }

        /// <summary>
        /// Decoding, if Huffman codes are known.
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

            return new EncodedMessage<string>(decoded.ToString());
        }

        /// <summary>
        /// Calculation of the compression ratio.
        /// </summary>
        /// <param name="sourceString">Source string.</param>
        /// <param name="compressionString">Compression string.</param>
        /// <returns>Compression ratio.</returns>
        public static double CalculateCompressionRatio(string sourceString, string compressionString)
        {
            double countBitsSourceString = 8 * sourceString.Length;
            double countBitsCompressionString = compressionString.Length;

            return countBitsSourceString / countBitsCompressionString;
        }
    }
}
