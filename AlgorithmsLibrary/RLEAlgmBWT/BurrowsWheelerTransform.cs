using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLibrary
{
    public static class BurrowsWheelerTransform
    {
        /// <summary>
        /// Encodes the input string using BWT and returns encoded string and 
        /// the index of original string in the sorted rotation matrix
        /// </summary>
        /// <param name="inputString">Input string</param>
        public static (string encoded, int index) Encode(string inputString)
        {
            int inputStringLength = inputString.Length;
            if (inputStringLength == 0)
            {
                return (string.Empty, 0);
            }

            //получаем матрицу всех смещений слов
            var rotations = GetRotations(inputString);
            //сортируем все слова в лексико графическом порядке
            Array.Sort(rotations, StringComparer.Ordinal);
            //берем из матрицы последний стобик
            var lastColumn = rotations
                .Select(x => x[inputStringLength - 1])
                .ToArray();
            var encoded = new string(lastColumn);
            return (encoded, Array.IndexOf(rotations, inputString));
        }

        /// <summary>
        /// Decodes the input string and returns original string
        /// </summary>
        /// <param name="encodedString">Encoded string</param>
        /// <param name="index">Index  of original string in the sorted rotation matrix</param>
        public static string Decode(string encodedString, int index)
        {
            int encodedStringLength = encodedString.Length;
            if (encodedStringLength == 0)
                return string.Empty;

            //массив строк для восстановления всех смещений
            var rotations = new string[encodedStringLength];

            for (var i = 0; i < encodedStringLength; i++)
            {
                //добавляем к существующей матрице слева стобик из символов encodedString 
                for (var j = 0; j < encodedStringLength; j++)
                {
                    rotations[j] = encodedString[j] + rotations[j];
                }

                //сортируем строки матрицы в лексикографическом порядке
                Array.Sort(rotations, StringComparer.Ordinal);
            }

            //возвращаем строку расположенную по необходимому индексу
            return rotations[index];
        }

        private static string[] GetRotations(string s)
        {
            var result = new string[s.Length];

            for (var i = 0; i < s.Length; i++)
            {
                result[i] = s.Substring(i) + s.Substring(0, i);
            }

            return result;
        }
    }
}
