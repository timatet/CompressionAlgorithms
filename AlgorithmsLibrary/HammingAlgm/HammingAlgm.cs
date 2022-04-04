using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            for (int p = 1; ; p++)
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
        /// <param name="l">Длина всего сообщения.</param>
        /// <returns>Список позиций подконтрольных битов в пределах длины сообщения.</returns>
        private static List<int> GetPositionsForContolBitCalculation(int p, int l)
        {
            // [(4k-2)*(p/2), (4k-2)*(p/2)+p-1] - числа в этих отрезках нам нужны
            // [(2k-1)*p, (2k-1)*p+p-1] = [(2k-1)*p, 2kp-1]
            // необходимо определить рамки для k:
            // (2k-1)*p > 0 и 2kp-1 < l
            // k>1/2 => k>=1 и k < (l+1)/(2p)
            var array = new List<int>();
            int maxK = (l + 1) / (2 * p) + 1;
            for (int k = 1; k <= maxK; k++)
            {
                int _2k1 = 2 * k - 1;
                for (int t = _2k1 * p; t < (_2k1 + 1) * p && t <= l; t++)
                {
                    array.Add(t - 1);
                }
            }

            return array;
        }

        private static string GetBinary(int m)
        {
            int[] bin = new int[8];
            int binLen = bin.Length;
            for (int i = 0; i < binLen; i++)
            {
                int degOfTwo = 1 << (binLen - i - 1);
                if (m >= degOfTwo)
                {
                    m -= degOfTwo;
                    bin[i] = 1;
                    if (m == 0)
                        return string.Join(null, bin);
                }
            }

            return string.Join(null, bin);
        }

        public static IAlgmEncoded<string> EncodeASCII(string source)
        {
            string sourceASCII = string.Join(null, Encoding.ASCII.GetBytes(source).Select(x => GetBinary(x)));

            return Encode(sourceASCII);
        }

        private static byte[] GetByteArray(string bin)
        {
            byte[] result = new byte[bin.Length / 8];
            for (int i = 0; i < bin.Length / 8; i++)
            {
                result[i] = Convert.ToByte(bin.Substring(i * 8, 8), 2);
            }
            return result;
        }

        public static IAlgmEncoded<string> DecodeASCII(string source)
        {
            var restored = Decode(source).GetAnswer();

            return new EncodedMessage<string>(Encoding.ASCII.GetString(GetByteArray(restored)), 0.0);
        }

        /// <summary>
        /// Encoding of the string. Arranging of information bits and calculation of control bits.
        /// </summary>
        /// <param name="source">Source message.</param>
        /// <returns>Encoded message.</returns>
        public static IAlgmEncoded<string> Encode(string source)
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

            return new EncodedMessage<string>(string.Join(null, DataArray), 0.0);
        }

        /// <summary>
        /// Decoding a message with a single error.
        /// </summary>
        /// <param name="encodedWithOneError">Transmitted message.</param>
        /// <returns>Restored message indicating the bit where the error was made.</returns>
        public static IAlgmEncoded<string> Decode(string encodedWithOneError)
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
            for (int i = 0; i < cntOfContolBits + 1; i++)
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

            return new EncodedMessage<string>(result.ToString(), 0.0);
        }
    }
}
