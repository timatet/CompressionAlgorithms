using System;
using System.Collections.Generic;

namespace AlgorithmsLibrary
{
    public static class LinearCodesType52
    {
        /// <summary>
        /// The method defines the generating matrix
        /// </summary>
        /// <param name="n">Count output chars (Columns)</param>
        /// <param name="k">Count input chars (Rows)</param>
        /// <returns>Generating matrix G</returns>
        public static int[,] GetGeneratingMatrix(int n, int k)
        {
            int r = n - k; // number of verification characters

            int[,] IKMatrix = new int[k, k];
            for (int i = 0; i < k; i++)
            {
                IKMatrix[i, i] = 1;
            }

            int[,] QMatrix = new int[k, n - k];

            if (n == 5 && k == 2)
            {
                QMatrix = new int[2, 3]
                {
                    {1, 1, 1 },
                    {0, 1, 1 }
                };
            }

            return UnionMatrixes(IKMatrix, QMatrix);
        }

        public static int[,] GetCheckMatrix(decimal[,] generatingMatrix)
        {
            int k = generatingMatrix.GetLength(0); // строки
            int n = generatingMatrix.GetLength(1); // столбцы


        }

        private static decimal[] GetLinearCombination(int[] koef, decimal[,] generatingMatrix)
        {
            int k = generatingMatrix.GetLength(0);
            int n = generatingMatrix.GetLength(1);

            decimal[] result = new decimal[n];

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[j] += koef[i] * generatingMatrix[i, j];
                }
            }

            for (int i = 0; i < n; i++)
            {
                result[i] %= 2;
            }
            
            return result;
        }

        private static int[] GetBinary(int m, int l)
        {
            int[] bin = new int[l];
            int binLen = bin.Length;
            for (int i = 0; i < binLen; i++)
            {
                int degOfTwo = 1 << (binLen - i - 1);
                if (m >= degOfTwo)
                {
                    m -= degOfTwo;
                    bin[i] = 1;
                    if (m == 0)
                        return bin;
                }
            }

            return bin;
        }

        public static decimal GetRedundancyRatio(int n, int k)
        {
            decimal r = n - k;
            return r / n;
        }

        public static decimal[,] GetMatrixCodeWords(decimal[,] generatingMatrix)
        {
            int k = generatingMatrix.GetLength(0); // строки
            int n = generatingMatrix.GetLength(1); // столбцы

            int codeCount = 2 << (k - 1); // количество кодовых слов
            // как линейных комбинаций строк генерирующей матрицы

            decimal[,] codeWords = new decimal[codeCount, n];

            for (int i = 0; i < codeCount; i++)
            {
                var koefI = GetBinary(i, k);
                var linComb = GetLinearCombination(koefI, generatingMatrix);
                for (int j = 0; j < n; j++)
                {
                    codeWords[i, j] = linComb[j];
                }
            }

            return codeWords;
        }

        /// <summary>
        /// The method reduces the matrix to a stepwise form.
        /// </summary>
        /// <param name="matrix">Input matrix or vectors set</param>
        /// <returns>The transformed matrix</returns>
        public static decimal[,] GetTriangleMatrix(decimal[,] matrix)
        {
            int k = matrix.GetLength(0); // строки
            int n = matrix.GetLength(1); // столбцы

            for (int i = 0; i < n; i++)
            { 
                for (int j = i + 1; j < k; j++)
                {
                    decimal koef = matrix[j, i] / matrix[i, i];
                    for (int t = i; t < n; t++)
                    {
                        matrix[j, t] -= matrix[i, t] * koef;
                    }
                }
            }

            return matrix;
        }

        /// <summary>
        /// Checks the list of vectors for linear independence.
        /// All vectors must be of the same length.
        /// The choice of the length of the vectors is entrusted to the first test subject.
        /// </summary>
        /// <param name="vectors">Set of vectors</param>
        /// <returns>Verification result</returns>
        private static bool IsVectorsLinearlyIndependent(decimal[,] vectors)
        {
            var TriangleMatrix = GetTriangleMatrix(vectors);

            int k = TriangleMatrix.GetLength(0); // строки
            int n = TriangleMatrix.GetLength(1); // столбцы

            bool IsVectorsLinearlyIndependent = false;
            for (int i = 0; i < n; i++)
            {
                if (TriangleMatrix[k - 1, i] != 0)
                {
                    IsVectorsLinearlyIndependent = true;
                    break;
                }
            }

            return IsVectorsLinearlyIndependent;
        }

        public static T[,] UnionMatrixes<T>(T[,] firstMatrix, T[,] secondMatrix)
        {
            int k1 = firstMatrix.GetLength(0); // строки
            int k2 = secondMatrix.GetLength(0);

            if (k1 != k2)
            {
                return new T[k1, k2];
            }

            int n1 = firstMatrix.GetLength(1);
            int n2 = secondMatrix.GetLength(1);
            T[,] unionMatrix = new T[k1, n1 + n2];

            for (int i = 0; i < k1; i++)
            {
                for (int j = 0; j < n1; j++)
                {
                    unionMatrix[i, j] = firstMatrix[i, j];
                }

                for (int j = n1; j < n1 + n2; j++)
                {
                    unionMatrix[i, j] = secondMatrix[i, j - n1];
                }
            }

            return unionMatrix;
        }

        public static void Print<T>(T[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write("{0:0.0}\t", matrix[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
