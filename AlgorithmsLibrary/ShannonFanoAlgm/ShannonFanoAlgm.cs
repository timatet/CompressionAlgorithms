﻿using AlgorithmsLibrary.CommonClasses;
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
                while (sumLeftPart < sumRightPart/* && l != r*/)
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
        //private static Dictionary<char, string> InOrderTraversal(DoublyNode<char> root)
        //{
        //    Dictionary<char, string> codes = new Dictionary<char, string>();

        //    if (root != null)
        //    {
        //        Stack<DoublyNode<char>> stack = new Stack<DoublyNode<char>>();
        //        var current = root;
        //        bool goLeftNext = true;

        //        stack.Push(current);

        //        string buffer = string.Empty;
        //        while (stack.Count > 0)
        //        {
        //            if (goLeftNext)
        //            {
        //                while (current.Previous != null)
        //                {
        //                    stack.Push(current);
        //                    current = current.Previous;
        //                    buffer += "0";
        //                }
        //            }

        //            //проверка является ли вершина обхода листом
        //            //вершина является листом если ей присвоено значение != default
        //            if (current.Data != default)
        //            {
        //                codes.Add(current.Data, buffer);
        //            }

        //            if (current.Next != null)
        //            {
        //                current = current.Next;
        //                buffer += "1";
        //                goLeftNext = true;
        //            }
        //            else
        //            {
        //                current = stack.Pop();
        //                buffer = buffer.Remove(buffer.Length - 1);
        //                goLeftNext = false;
        //            }
        //        }
        //    }

        //    return codes;
        //}
        private static Dictionary<char, string> InOrderTraversal(DoublyNode<char> root)
        {
            Dictionary<char, string> codes = new Dictionary<char, string>();

            if (root != null)
            {
                Stack<DoublyNode<char>> stack = new Stack<DoublyNode<char>>();
                var parent = root;
                var current = root;
                stack.Push(current);
                bool goLeftNext = true, goRightNext = true;

                string buffer = string.Empty;

                while (root.Previous != null || root.Next != null)
                {
                    while (current.Previous != null)
                    {
                        current = current.Previous;
                        stack.Push(current);
                        buffer += "0";
                    }

                    //проверка является ли вершина обхода листом
                    //вершина является листом если ей присвоено значение != default
                    if (current.Data != default)
                    {
                        codes.Add(current.Data, buffer);
                        stack.Pop();
                        parent = stack.Peek();
                        if (current == parent.Previous) parent.Previous = null;
                        else parent.Next = null;
                        current = parent;
                        goLeftNext = false;
                        buffer = buffer.Remove(buffer.Length - 1);
                    }
                    if (current.Next != null)
                    {
                        current = current.Next;
                        stack.Push(current);
                        buffer += "1";
                        goLeftNext = true;
                    }
                    else
                    {
                        stack.Pop();
                        parent = stack.Peek();
                        if (current == parent.Previous) parent.Previous = null;
                        else parent.Next = null;
                        current = parent;
                        buffer = buffer.Remove(buffer.Length - 1);
                        goLeftNext = false;
                    }
                }
            }
            return codes;
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
            var root = BuildTree(parent, 0, nodes.Count-1, nodes);

            //обходим поулчившееся дерево, приписывая каждому символу свой код
            return InOrderTraversal(root);
        }


        /// <summary>
        /// Encoding. The algorithm compile a dictionary of frequencies from the source string, 
        /// then builds a tree on them and gets a dictionary with ShannonFano codes.
        /// </summary>
        /// <param name="source">Source string.</param>
        /// <returns>Encoded string.</returns>
        public static string Encode(string source)
        {
            StringBuilder encoded = new StringBuilder(string.Empty);
            Dictionary<char, string> codes = GetShannonFanoCodes(source);

            foreach (char c in source)
                encoded.Append(codes[c]);

            return encoded.ToString();
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
        public static string Decode(Dictionary<char, int> frequencies, string encoded)
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
        public static string Decode(Dictionary<char, string> codes, string encoded)
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

            return decoded.ToString();
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
