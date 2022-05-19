using System;

namespace AlgorithmsLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            decimal[,] vs = { { 1, 0, 1, 0, 1}, {0, 1, 0, 1, 1 } };
            LinearCodesType52.Print(LinearCodesType52.GetMatrixCodeWords(vs));

            Console.ReadKey();
        }
    }
}
