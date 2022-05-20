using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsLibrary
{
    public static class IENumberableExt
    {
        public static Vector ToVector(this IEnumerable<int> @this)
        {
            return new Vector(@this);
        }
    }

    public class Vector : Matrix
    {
        public int length;
        /// <summary>
        /// Checks the list of vectors for linear independence.
        /// All vectors must be of the same length.
        /// The choice of the length of the vectors is entrusted to the first test subject.
        /// </summary>
        /// <param name="vectors">Set of vectors</param>
        /// <returns>Verification result</returns>
        public bool IsVectorsLinearlyIndependent()
        {
            var TriangleMatrix = GetTriangleMatrix();

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

        public int GetWeight()
        {
            int weight = 0;

            for (int i = 0; i < n; i++)
            {
                if (base[0, i] != 0)
                {
                    weight++;
                }
            }

            return weight;
        }

        public int this[int i]
        {
            get => base[0, i];
            set => base[0, i] = value;
        }

        public Vector(int n) : this(new int[n]) { }
        public Vector(IEnumerable<int> numbers) : base(1, numbers.Count())
        {
            length = numbers.Count();

            int i = 0;
            foreach (var number in numbers)
            {
                base[0, i++] = number;
            }
        }
    }
}
