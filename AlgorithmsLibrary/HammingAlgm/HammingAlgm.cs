using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;

namespace AlgorithmsLibrary
{
    public static class HammingAlgm
    {
        /// <summary>
        /// Вычисление количества контрольных бит, необходимых для кодирования сообщения.
        /// </summary>
        /// <param name="m">Длина кодируемого сообщения.</param>
        /// <returns>Число контрольных бит.</returns>
        private static int GetCountOfControlBits(int m)
        {
            for (int p = 1;; p++)
            {
                if ((1 << p) >= (m + p + 1))
                {
                    return p;
                }
            }
        }

        /// <summary>
        /// Вычисление позиций в сообщении, биты на которых контролируются битом с позиции p.
        /// </summary>
        /// <param name="p">Позиция контрольного бита.</param>
        /// <param name="dataLen">Длина всего сообщения.</param>
        /// <returns>Список позиций подконтрольных битов в пределах длины сообщения.</returns>
        private static List<int> GetPositionsForContolBitCalculation(int p, int dataLen)
        { //наверное эти значения можно вычислять как то попроще...
            var array = new List<int>();
            bool skip = false; //необходимо взять все позиции начиная с p с интервалом p
            for (int i = p - 1, mod = 0; i < dataLen; i++)
            {
                if (skip)
                {
                    if (++mod == p)
                    {
                        skip = false;
                        mod = 0;
                    }
                    continue;
                }

                array.Add(i);
                if (++mod == p)
                {
                    skip = true;
                    mod = 0;
                }
            }

            return array;
        }

        /// <summary>
        /// Encoding of the string. Arranging of information bits and calculation of control bits.
        /// </summary>
        /// <param name="source">Source message.</param>
        /// <returns>Encoded message.</returns>
        public static string Encode(string source)
        {
            int cntOfContolBits = GetCountOfControlBits(source.Length);
            int dataLen = source.Length + cntOfContolBits;

            int[] DataArray = new int[dataLen];
            for (int i = dataLen - 1, j = source.Length - 1; i > 1; i--)
            {
                if ((i + 1) > 0 && (((i + 1) & i) == 0))
                { //если бит является степенью двойки, то он является контрольным
                    //сюда мы ничего не записываем
                    continue;
                }
                //в свободные ячейки расставляем биты исходной строки
                DataArray[i] = source[j--] - '0';
                if (DataArray[i] != 0 && DataArray[i] != 1)
                    throw new ArgumentException("the number must consist of 0 and 1");
            }

            for (int i = 0; i < cntOfContolBits; i++)
            { //осталось вычислить значения контрольных битов
                int contolBit = (1 << i) - 1;
                var positions = GetPositionsForContolBitCalculation(1 << i, dataLen);
                DataArray[contolBit] = positions.Select(p => DataArray[p]).Sum() % 2;
            }

            return string.Join(null, DataArray);
        }

        /// <summary>
        /// Decoding a message with a single error.
        /// </summary>
        /// <param name="encodedWithOneError">Transmitted message.</param>
        /// <returns>Restored message indicating the bit where the error was made.</returns>
        public static DecodedMessage Decode(string encodedWithOneError)
        {
            //задача пересчитать контрольные биты. Найти те, которые отличаются
            //сумма позиций этих битов и есть номер бита в котором была ошибка
            int dataLen = encodedWithOneError.Length;
            int cntOfContolBits = GetCountOfControlBits(dataLen) - 1;
            var DataArray = new int[dataLen];
            for (int i = 0; i < dataLen; i++)
            {
                DataArray[i] = encodedWithOneError[i] - '0';
                if (DataArray[i] != 0 && DataArray[i] != 1)
                    throw new ArgumentException("the number must consist of 0 and 1");
            }

            int brakePositions = 0;
            for (int i = 0; i < cntOfContolBits; i++)
            { //осталось вычислить значения контрольных битов
                var positions = GetPositionsForContolBitCalculation(1 << i, dataLen);

                if (positions.Select(p => DataArray[p]).Sum() % 2 == 1)
                    brakePositions += 1 << i;
            }

            StringBuilder encoded = new StringBuilder(encodedWithOneError);
            if (brakePositions != 0)
            {
                //Исправляем ошибку
                encoded[brakePositions - 1] = encoded[brakePositions - 1] == '0' ? '1' : '0';
            }

            //восстанавливаем сообщение
            StringBuilder result = new StringBuilder(string.Empty);
            for (int i = 0; i < dataLen; i++)
            {
                if ((i + 1) > 0 && (((i + 1) & i) == 0))
                {
                    continue;
                }
                result.Append(encoded[i]);
            }

            return new DecodedMessage(result.ToString(), encodedWithOneError, brakePositions);
        }
    }
}
