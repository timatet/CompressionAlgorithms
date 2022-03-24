using AlgorithmsLibrary.StringBuilderAddons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AlgorithmsLibrary
{
    public static class LZ77Algm
    {
        /// <summary>
        /// String compression using the LZ77 algorithm
        /// </summary>
        /// <param name="inputString">Source string</param>
        /// <returns>Compressed (encoded) string</returns>
        public static List<CodeBlock> Encode(string inputString)
        {
            List<CodeBlock> result = new List<CodeBlock>();
            StringBuilder searchBuffer = new StringBuilder();
            StringBuilder establishingBuffer = new StringBuilder(inputString);

            int currentLengthSubString = 0;
            //Кодирование идет до тех пор пока учреждающий буфер (establishingBuffer) не окажется пуст
            while (establishingBuffer.Length > 0)
            {
                int positionInBuffer = -1;
                //Ищем в searchBuffer максимальную подстроку, имеющую вхождение в establishingBuffer
                while (true)
                {
                    int previousPositionInBuffer = positionInBuffer;

                    if (establishingBuffer.Length < currentLengthSubString + 1)
                        positionInBuffer = -1;
                    else
                        positionInBuffer = searchBuffer.IndexOf(establishingBuffer.Substring(0, currentLengthSubString + 1));

                    if (positionInBuffer < 0)
                    {
                        //Если подстроки не найдено возвращаем текущее значение начала этой строки
                        //по умолчанию -1
                        positionInBuffer = previousPositionInBuffer;
                        break;
                    }
                    else
                    {
                        currentLengthSubString++;
                    }
                }

                //Если строки(символа) не было найдено, то заносим в буффер новый элемент
                if (positionInBuffer < 0)
                {
                    char nextChar = establishingBuffer[0];

                    searchBuffer.Append(nextChar);
                    establishingBuffer.Remove(0, 1);

                    result.Add(new CodeBlock(0, 0, nextChar)); //указываем на него метку
                }
                else
                {
                    int offset = searchBuffer.Length - positionInBuffer;
                    int length = currentLengthSubString;

                    //Если длина найденной подстроки равна количеству элементов буффера
                    //то последний символ это символ конца строки
                    if (currentLengthSubString == establishingBuffer.Length)
                        establishingBuffer.Append('$');
                    char nextChar = establishingBuffer[currentLengthSubString];

                    string subInEstablish = establishingBuffer.Substring(0, currentLengthSubString + 1);
                    establishingBuffer.Remove(0, length + 1);
                    searchBuffer.Append(subInEstablish);

                    result.Add(new CodeBlock(offset, length, nextChar));
                }

                currentLengthSubString = 0;
            }

            return result;
        }

        /// <summary>
        /// Calculation of the compression ratio
        /// </summary>
        /// <param name="sourceString">Source string</param>
        /// <param name="compressionString">Compressed (encoded) string</param>
        /// <returns>Compression ratio</returns>
        public static double CalculateCompressionRatio(string sourceString, List<CodeBlock> compressionString)
        {
            //Считаем что в стандартной кодировке один символ = 8бит
            double countBitsSourceString = 8 * sourceString.Length;

            double countBitsCompressionString = 0;
            foreach (CodeBlock compression in compressionString)
            {
                int countBitsOffset = Convert.ToString(compression.Offset, 2).Length;
                int countBitsLength = Convert.ToString(compression.Length, 2).Length;
                int countBitsChar = 8;

                countBitsCompressionString += countBitsOffset + countBitsOffset + countBitsChar;
            }

            return countBitsSourceString / countBitsCompressionString;
        }

        /// <summary>
        /// Decoding strings compressed by the LZ77 algorithm
        /// </summary>
        /// <param name="encodedString">Compressed (encoded) string</param>
        /// <returns>Decoded string</returns>
        public static string Decode(List<CodeBlock> encodedString)
        {
            StringBuilder resultDecoding = new StringBuilder(string.Empty);

            foreach (CodeBlock codeBlock in encodedString) {
                if (codeBlock.Length > 0) {                         
                    int start = resultDecoding.Length - codeBlock.Offset;
                    for (int i = 0; i <= codeBlock.Length - 1; i++)       
                        resultDecoding.Append(resultDecoding[start + i]);
                }
                resultDecoding.Append(codeBlock.NextChar);
            }

            if (resultDecoding.Length > 0 && resultDecoding[resultDecoding.Length - 1].Equals('$'))
                resultDecoding.Remove(resultDecoding.Length - 1, 1);

            return resultDecoding.ToString();
        }
    }

}
