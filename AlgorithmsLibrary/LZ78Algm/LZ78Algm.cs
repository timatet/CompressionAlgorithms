using AlgorithmsLibrary.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AlgorithmsLibrary
{
    /// <summary>
    /// Здесь идея этого алгоритма.
    /// </summary>
    public static class LZ78Algm 
    {
        static bool ExtendedAlgm = false;
        public static IAlgmEncoded<List<LZ78CodeBlock>> Encode(string source, bool extended)
        {
            ExtendedAlgm = extended;
            return Encode(source);
        }
        public static IAlgmEncoded<List<LZ78CodeBlock>> Encode(string source)
        {
            string Buffer = ""; //строка для формирования ключа для словаря
            Dictionary<string, int> Dictionary = new Dictionary<string, int> { { "", 0 } };
            List<LZ78CodeBlock> EncodedString = new List<LZ78CodeBlock>(); // ответ
            StringBuilder extended = new StringBuilder(string.Empty);

            for (int i = 0; i < source.Length; i++)
            {
                if (Dictionary.ContainsKey(Buffer + source[i]))
                {   // можем ли мы увеличить префикс
                    Buffer += source[i];
                }
                else
                {
                    var codeblock = new LZ78CodeBlock(Dictionary[Buffer], source[i]);
                    if (ExtendedAlgm)
                    {
                        char c = Buffer.Length == 0 ? source[i] : Buffer[0];
                        extended.Append(c + " " + codeblock + "\n");
                    }
                    EncodedString.Add(codeblock);  // добавляем пару в ответ
                    Dictionary.Add(Buffer + source[i], Dictionary.Count); // добавляем слово в словарь
                    Buffer = string.Empty;
                }
            }
            // если буффер не пуст - этот код уже был, нужно его добавить в конец словаря
            if (!Buffer.Equals(string.Empty))
            {
                if (Dictionary.ContainsKey(Buffer))
                {
                    var codeblock = new LZ78CodeBlock(Dictionary[Buffer], '$');
                    if (ExtendedAlgm)
                    {
                        extended.Append('$' + " " + codeblock + "\n");
                    }
                    EncodedString.Add(codeblock);
                }
                else
                {
                    var last_ch = Buffer.Last(); // берем последний символ буффера, как "новый" символ
                    Buffer = Buffer.Remove(Buffer.Length - 1); // удаляем последний символ из буфера

                    var codeblock = new LZ78CodeBlock(Dictionary[Buffer], last_ch);
                    if (ExtendedAlgm)
                    {
                        extended.Append(Buffer[0] + " " + codeblock + "\n");
                    }
                    EncodedString.Add(codeblock); // добавляем пару в ответ 
                }
            }

            if (ExtendedAlgm)
            {
                foreach (var item in Dictionary)
                {
                    extended.Append(item.Key + " " + item.Value + "\n");
                }
            }

            return new EncodedMessage<List<LZ78CodeBlock>>(EncodedString, CalculateCompressionRatio(source, EncodedString), extended.ToString());
        }

        private static List<LZ78CodeBlock> ParseEncodedString(string encodedString)
        {
            List<LZ78CodeBlock> encodedStringParsed = new List<LZ78CodeBlock>();
            // вид кодового блока:
            //({0},{2})...({0},{2})
            //парсит всю строку на блоки
            //globalCode - проверяет всю строку, подходит ли она для декодирования
            Regex globalCode = new Regex(@"(?=^)(([(](\d+)([,])(.|\n|\r|\t)[)])|(\s)|(\n)|(\r))+(?=$)");
            Regex regex = new Regex(@"([(](\d+)([,])(.|\n|\r|\t)[)])"); //регулярка кодового блока
            Regex intRegex = new Regex(@"\d+"); //регулярка цыфры
            if (!globalCode.IsMatch(encodedString))
            {
                throw new CodingException();
            }

            MatchCollection matches = regex.Matches(encodedString);
            foreach (Match match in matches)
            {
                string codeBlock = match.Value;
                MatchCollection matchesBlock = intRegex.Matches(codeBlock);
                encodedStringParsed.Add(new LZ78CodeBlock(int.Parse(matchesBlock[0].Value), codeBlock[codeBlock.Length - 2]));
            }

            return encodedStringParsed;
        }

        private static double CalculateCompressionRatio(string sourceString, List<LZ78CodeBlock> compressionString)
        {
            //Считаем что в стандартной кодировке один символ = 8бит
            double countBitsSourceString = 8 * sourceString.Length;

            double countBitsCompressionString = 0;
            foreach (LZ78CodeBlock compression in compressionString)
            {
                int countBitsOffset = Convert.ToString(compression.Position, 2).Length;
                int countBitsChar = 8;

                countBitsCompressionString += countBitsOffset + countBitsOffset + countBitsChar;
            }

            return Math.Round(countBitsSourceString / countBitsCompressionString, 3);
        }

        public static IAlgmEncoded<string> Decode(string encodedString)
        {
            List<LZ78CodeBlock> encodedStringParsed = ParseEncodedString(encodedString);

            StringBuilder resultDecoding = new StringBuilder(string.Empty);

            List<string> dict = new List<string> { string.Empty }; // словарь, слово с номером 0 — пустая строка
            foreach (LZ78CodeBlock code in encodedStringParsed)
            {
                var word = dict[code.Position] + code.Char; // составляем слово из уже известного из словаря и новой буквы
                resultDecoding.Append(word); // приписываем к ответу
                dict.Add(word); // добавляем в словарь
            }

            string decodedString = resultDecoding.ToString();
            return new EncodedMessage<string>(decodedString, CalculateCompressionRatio(decodedString, encodedStringParsed));
        }
    }
}
