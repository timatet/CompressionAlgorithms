using System;
using System.Collections.Generic;

namespace AlgorithmsLibrary
{
    public class Matrix
    {
        private int[,] numbers;
        /// <summary>
        /// Count of rows in matrix.
        /// </summary>
        public int k;
        /// <summary>
        /// Count of columns in matrix.
        /// </summary>
        public int n;

        public int this[int i, int j]
        {
            get => numbers[i, j];
            set => numbers[i, j] = value;
        }

        public static Vector operator *(Vector a, Matrix b)
        {
            Vector resultMatrix = new Vector(b.n);

            for (int j = 0; j < b.n; j++)
            {
                for (int k = 0; k < a.n; k++)
                {
                    resultMatrix[j] += a[k] * b[k, j];
                }
                resultMatrix[j] %= 2;
            }

            return resultMatrix;
        }

        public static Matrix operator *(Matrix a, Matrix b)
        {
            Matrix resultMatrix = new Matrix(a.k, b.n);

            for (int i = 0; i < a.k; i++)
            {
                for (int j = 0; j < b.n; j++)
                {
                    for (int k = 0; k < a.n; k++)
                    {
                        resultMatrix[i, j] += a[i, k] * b[k, j];
                    }
                    resultMatrix[i, j] %= 2;
                }

            }

            return resultMatrix;
        }

        public Matrix GetTransMatrix()
        {
            Matrix transMatrix = new Matrix(n, k);

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    transMatrix[j, i] = numbers[i, j];
                }
            }

            return transMatrix;
        }

        /// <summary>
        /// The method reduces the matrix to a stepwise form.
        /// </summary>
        /// <param name="matrix">Input matrix or vectors set</param>
        /// <returns>The transformed matrix</returns>
        public Matrix GetTriangleMatrix()
        {
            Matrix matrix = GetCopy();
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < k; j++)
                {
                    int koef = matrix[j, i] / matrix[i, i];
                    for (int t = i; t < n; t++)
                    {
                        matrix[j, t] -= matrix[i, t] * koef;
                    }
                }
            }

            return matrix;
        }

        public int GetDeterminant()
        {
            if (k != n)
            {
                return 0;
            }

            if (n == 2)
            {
                return numbers[0, 0] * numbers[1, 1] - numbers[0, 1] * numbers[1, 0];
            }

            int result = 0;
            for (int j = 0; j < n; j++)
            {
                var subMatrix = Slice(1, k, 0, j).GetUnion(Slice(1, k, j + 1, n));
                result += (j % 2 == 1 ? 1 : -1) * numbers[0, j] * subMatrix.GetDeterminant();
            }

            return result;
        }

        private int CalculateMinor(int i, int j)
        {
            Matrix minor = new Matrix(k - 1, n - 1);

            for (int ii = 0; ii < k; ii++)
            {
                for (int jj = 0; jj < n; jj++)
                {
                    if (ii == i || jj == j)
                        continue;

                    if (ii < i && jj < j)
                    {
                        minor[ii, jj] = numbers[i, j];
                    } else if (ii > j && jj > j)
                    {
                        minor[ii - 1, jj - 1] = numbers[i, j];
                    } else if (ii > j && jj < j)
                    {
                        minor[ii - 1, jj] = numbers[i, j];
                    } else if (ii < j && jj > j)
                    {
                        minor[ii, jj - 1] = numbers[i, j];
                    }
                }
            }

            return minor.GetDeterminant();
        }

        public Matrix GetInverseMatrix()
        {
            if (k != n)
                return null;
            int determinant = GetDeterminant();

            if (determinant == 0)
                return null;

            var result = new Matrix(k, n);
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = ((i + j) % 2 == 1 ? -1 : 1) *
                    CalculateMinor(i, j) / determinant;
                }
            }          

            return result.GetTransMatrix();
        }

        public Matrix GetPseudoInverseMatrix()
        {
            var T = GetTransMatrix();
            var AA = T * this;
            var AInverse = AA.GetInverseMatrix();

            if (AInverse == null)
                return null;

            return AInverse * T;
        }

        public void Union(Matrix other)
        {
            if (k != other.k)
            {
                return;
            }

            int[,] unionMatrix = new int[k, n + other.n];

            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    unionMatrix[i, j] = numbers[i, j];
                }

                for (int j = n; j < n + other.n; j++)
                {
                    unionMatrix[i, j] = other[i, j - n];
                }
            }

            this.numbers = unionMatrix;
            n += other.n;
        }

        public Matrix GetUnion(Matrix other)
        {
            Matrix copyThis = GetCopy();
            copyThis.Union(other);
            return copyThis;
        }

        public Matrix Slice(int kS, int kE, int nS, int nE)
        {
            Matrix newMatrix = new Matrix(kE - kS, nE - nS);

            for (int i = kS; i < kE; i++)
            {
                for (int j = nS; j < nE; j++)
                {
                    newMatrix[i - kS, j - nS] = numbers[i, j];
                }
            }

            return newMatrix;
        }

        public IEnumerable<int> SliceRow(int row)
        {
            for (var i = 0; i < n; i++)
            {
                yield return numbers[row, i];
            }
        }

        public IEnumerable<int> SliceColumn(int column)
        {
            for (var i = 0; i < k; i++)
            {
                yield return numbers[i, column];
            }
        }

        public Matrix GetCopy()
        {
            return new Matrix(k, n, numbers);
        }

        public Matrix(int k, int n) : this(k, n, new int[k, n])
        { }
        public Matrix(int k, int n, int[,] numbers)
        {
            this.numbers = numbers;
            this.k = k;
            this.n = n;
        }
    }
}
