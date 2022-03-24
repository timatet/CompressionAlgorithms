﻿using System.Collections.Generic;
using System.Text;
using System;

namespace AlgorithmsLibrary
{
    public static class RLEAlgm
    {
        public static List<RLECodeBlock> Encode(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                throw new ArgumentNullException("string for encoding is null or empty");
            }

            List<RLECodeBlock> result = new List<RLECodeBlock>();
            var encoded = BurrowsWheelerTransform.Encode(inputString);

            result.Add(new RLECodeBlock(default, encoded.index));
            int countRepeatsSymbols = 0;
            char currentSymbol = encoded.encoded[0];
            encoded.encoded += '$';

            foreach (var symbol in encoded.encoded)
            {
                if (currentSymbol == symbol)
                {
                    countRepeatsSymbols++;
                    continue;
                }
                
                result.Add(new RLECodeBlock(currentSymbol, countRepeatsSymbols));
                countRepeatsSymbols = 1;
                currentSymbol = symbol;
            }

            return result;
        }

        public static string Decode(List<RLECodeBlock> encodedString)
        {
            if (encodedString.Count < 2)
            {
                throw new ArgumentNullException("string for decoding is null or empty");
            }

            StringBuilder decompressedString = new StringBuilder(string.Empty);

            for (int i = 1; i < encodedString.Count; i++)
            {
                for (int j = 0; j < encodedString[i].Repeats; j++)
                    decompressedString.Append(encodedString[i].Symbol);
            }

            var result = BurrowsWheelerTransform.Decode(decompressedString.ToString(), encodedString[0].Repeats);
            return result;
        }

        public static double CalculateCompressionRatio(string sourceString, List<RLECodeBlock> compressionString)
        {
            double countBitsSourceString = 8 * sourceString.Length;

            double countBitsCompressionString = 0;
            foreach (RLECodeBlock compression in compressionString)
            {
                int countBitsChar = 8;
                int countBitsRepeats = Convert.ToString(compression.Repeats, 2).Length;

                countBitsCompressionString += countBitsChar + countBitsRepeats;
            }

            countBitsCompressionString -= 8; //в процессе подсчета был посчитан лишний байт, так как один кодовый блок
            //используется только для хранения индекса, где была расположена первоначальная строчка 
            return countBitsSourceString / countBitsCompressionString;
        }
    }
}
