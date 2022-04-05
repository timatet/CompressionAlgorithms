using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace AlgorithmsLibrary
{
    public static class RLEAlgm
    {
        public static IAlgmEncoded<List<RLECodeBlock>> Encode(string inputString)
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

            return new EncodedMessage<List<RLECodeBlock>>(result, CalculateCompressionRatio(inputString, result));
        }

        private static List<RLECodeBlock> ParseEncodedString(string encodedString)
        {
            List<RLECodeBlock> encodedStringParsed = new List<RLECodeBlock>();
            // вид кодового блока {0}{1}
            // {0} - 1 символ
            // {1} - число
            // причем в любой закодированной строке первым стоит так же число - это номер строки в матрице
            Regex intRegex = new Regex(@"\d+"); //регулярка числа
            Regex intRegexEndString = new Regex(@"\d+(?=$)"); //регулярка числа
            //прроверка всей входной строки на строгое соответствие
            //1(-,1)...( ,1)
            Regex globalRLE = new Regex(@"(?=^)(\d+)([{](.|\n|\r|\t)[,](\d+)+[}])+(?=$)");
            //проверка на соответсвие одному блоку (-,1)
            Regex blockRLE = new Regex(@"(?<={)(.|\n|\r|\t)[,](\d+)+(?=})");

            //если входная строка не подходит под паттерн то выбрасывается ошибка
            if (!globalRLE.IsMatch(encodedString))
            {
                throw new ArgumentException();
            }

            //получаем число как номер строки в матрице
            int num = int.Parse(intRegex.Match(encodedString).Value);
            encodedStringParsed.Add(new RLECodeBlock(default, num));

            //иначе разделяю строку на RLE блоки
            MatchCollection matches = blockRLE.Matches(encodedString);
            foreach (Match match in matches)
            {
                string codeBlock = match.Value;
                char cb = codeBlock[0]; // берем символ
                //и получаем цифру
                int intg = int.Parse(intRegexEndString.Match(codeBlock).Value);

                encodedStringParsed.Add(new RLECodeBlock(cb, intg));
            }

            return encodedStringParsed;
        }

        public static IAlgmEncoded<string> Decode(string encodedString)
        {
            var encodedStringParsed = ParseEncodedString(encodedString);

            //0 блок хранит номер строки в матрице BWT 
            //1 блок хранит хотя бы один символ
            if (encodedStringParsed.Count < 2)
            {
                throw new ArgumentNullException("string for decoding is null or empty");
            }

            StringBuilder decompressedString = new StringBuilder(string.Empty);

            for (int i = 1; i < encodedStringParsed.Count; i++)
            {
                for (int j = 0; j < encodedStringParsed[i].Repeats; j++)
                    decompressedString.Append(encodedStringParsed[i].Symbol);
            }

            var result = BurrowsWheelerTransform.Decode(decompressedString.ToString(), encodedStringParsed[0].Repeats);
            return new EncodedMessage<string>(result, CalculateCompressionRatio(result, encodedStringParsed));
        }

        private static double CalculateCompressionRatio(string sourceString, List<RLECodeBlock> compressionString)
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
            return Math.Round(countBitsSourceString / countBitsCompressionString, 3);
        }
    }
}
