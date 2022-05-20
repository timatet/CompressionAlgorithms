using System;
using System.Collections.Generic;
using System.Linq;

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
        public static Matrix GetGeneratingMatrix(int n, int k) //G
        {
            int r = n - k; // number of verification characters

            Matrix IKMatrix = new Matrix(k, k);
            for (int i = 0; i < k; i++)
            {
                IKMatrix[i, i] = 1;
            }

            Matrix QMatrix = new Matrix(k, n - k);

            if (n == 5 && k == 2)
            {
                QMatrix = new Matrix(2, 3, new int[,] { 
                    { 1, 1, 1 }, 
                    { 0, 1, 1 } 
                });
            }

            return IKMatrix.GetUnion(QMatrix);
        }
               

        public static Matrix GetCheckMatrix(Matrix generatingMatrix) //H
        {
            int k = generatingMatrix.k; // строки
            int n = generatingMatrix.n; // столбцы

            var QMatrix = generatingMatrix.Slice(0, k, n - k - 1, n);
            var TransQMatrix = QMatrix.GetTransMatrix();

            int kQ = TransQMatrix.k; 

            Matrix IKMatrix = new Matrix(kQ, kQ);
            for (int i = 0; i < kQ; i++)
            {
                IKMatrix[i, i] = 1;
            }

            return TransQMatrix.GetUnion(IKMatrix);
        }

        private static Vector GetLinearCombination(Vector koef, Matrix generatingMatrix)
        {
            int k = generatingMatrix.k;
            int n = generatingMatrix.n;

            Vector result = new Vector(n);

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

        public static Vector GetBinary(int m, int l)
        {
            Vector bin = new Vector(l);
            int binLen = bin.length;
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

        public static Matrix GetMatrixCodeWords(Matrix generatingMatrix)
        {
            int k = generatingMatrix.k; // строки
            int n = generatingMatrix.n; // столбцы

            int codeCount = 2 << (k - 1); // количество кодовых слов
            // как линейных комбинаций строк генерирующей матрицы

            Matrix codeWords = new Matrix(codeCount, n);

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

        public static Matrix GetAdjanсencyClass(Matrix adjClass, Vector vector)
        {
            int k = adjClass.k; // строки
            int n = adjClass.n; // столбцы

            Matrix newClass = new Matrix(k, n);
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    newClass[i, j] = (adjClass[i, j] + vector[j]) % 2;
                }
            }

            return newClass;
        }

        public static List<Vector> GetAdjancencyClassLeaders(Matrix adjacencyClass)
        {
            int k = adjacencyClass.k; // строки
            int n = adjacencyClass.n; // столбцы

            List<Vector> leaders = new List<Vector>();
            int minWeight = int.MaxValue;
            for (int i = 0; i < k; i++)
            {
                var vector = adjacencyClass.SliceRow(i).ToVector();

                int weight = vector.GetWeight();
                if (weight < minWeight)
                {
                    minWeight = weight;

                    leaders = new List<Vector> { vector };
                }
                else if (weight == minWeight)
                {
                    leaders.Add(vector);
                }
            }

            return leaders;
        }

        public static bool IsContainsVectorInAdjClasses(List<Matrix> adjClasess, Vector vector)
        {
            int cnt = adjClasess.Count; // count of adjancency classes

            bool isVectorContains;
            for (int adj = 0; adj < cnt; adj++)
            {
                int k = adjClasess[adj].k; // строки
                int n = adjClasess[adj].n; // столбцы
                for (int i = 0; i < k; i++)
                {
                    isVectorContains = true;
                    for (int j = 0; j < n; j++)
                    {
                        if (adjClasess[adj][i, j] != vector[j])
                        {
                            isVectorContains = false;
                            break;
                        }
                    }

                    if (isVectorContains is true)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void Print(Matrix matrix)
        {
            for (int i = 0; i < matrix.k; i++)
            {
                for (int j = 0; j < matrix.n; j++)
                {
                    Console.Write("{0:0.0}\t", matrix[i, j]);
                }

                Console.WriteLine();
            }
        }
    }
}
