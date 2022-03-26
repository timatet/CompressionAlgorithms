using System.Collections.Generic;

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
                if ((1 << p) >= m + p + 1)
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
        /// Кодирование строки. Расстановка информационных битов и вычисление контрольных битов.
        /// </summary>
        /// <param name="source">Исходное сообщение.</param>
        /// <returns>Закодированное сообщение.</returns>
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
            }

            for (int i = 0; i < cntOfContolBits; i++)
            { //осталось вычислить значения контрольных битов
                int contolBit = (1 << i) - 1;
                var positions = GetPositionsForContolBitCalculation(1 << i, dataLen);
                foreach (var p in positions)
                {
                    DataArray[contolBit] += DataArray[p];
                }
                DataArray[contolBit] %= 2;
            }

            return string.Join(null, DataArray);
        }

        /// <summary>
        /// Декодирование сообщения с одной допущенной (возможно) ошибкой.
        /// </summary>
        /// <param name="encodedWithOneError">Передаваемое сообщение.</param>
        /// <returns>Восстановленное сообщение с указанием бита, где была допущена ошибка.</returns>
        public static DecodedMessage Decode(string encodedWithOneError)
        {
            return default;
        }
    }
}
