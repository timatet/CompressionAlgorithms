using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmsLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int N = 5;
            int K = 2;
            int R = N - K;

            List<Matrix> AdjClasses = new List<Matrix>();
            var GeneratingMatrix = LinearCodesType52.GetGeneratingMatrix(N, K);
            LinearCodesType52.Print(GeneratingMatrix);
            Console.WriteLine();

            AdjClasses.Add(LinearCodesType52.GetMatrixCodeWords(GeneratingMatrix));
            LinearCodesType52.Print(AdjClasses.Last());
            List<List<Vector>> AdjancencyClassesLeaders = new List<List<Vector>> { new List<Vector> { new Vector(N) } };
            Console.WriteLine("Leaders: ");
            for (int z = 0; z < N; z++)
            {
                Console.Write(AdjancencyClassesLeaders[0][0][z] + " ");
            }
            Console.WriteLine('\n');

            int limit = 2 << (N - 1);
            for (int i = 0; i < limit; i++)
            {
                var binary = LinearCodesType52.GetBinary(i, N);

                if (!LinearCodesType52.IsContainsVectorInAdjClasses(AdjClasses, binary))
                {
                    var AdjClass = LinearCodesType52.GetAdjanсencyClass(AdjClasses[0], binary);
                    AdjClasses.Add(AdjClass);
                    Console.Write("V: ");
                    for (int t = 0; t < N; t++)
                    {
                        Console.Write(binary[t] + " ");
                    }
                    Console.WriteLine();
                    LinearCodesType52.Print(AdjClass);
                    var leaders = LinearCodesType52.GetAdjancencyClassLeaders(AdjClass);
                    Console.WriteLine("Leaders: ");
                    for (int t = 0; t < leaders.Count; t++)
                    {
                        for (int z = 0; z < N; z++)
                        {
                            Console.Write(leaders[t][z] + " ");
                        }
                        Console.WriteLine();
                    }
                    Console.WriteLine();
                    AdjancencyClassesLeaders.Add(leaders);
                }
            }

            Console.WriteLine("*****");

            var CheckMatrix = LinearCodesType52.GetCheckMatrix(GeneratingMatrix);
            LinearCodesType52.Print(CheckMatrix);
            Console.WriteLine();

            var TransCheckMatrix = CheckMatrix.GetTransMatrix();
            LinearCodesType52.Print(TransCheckMatrix);
            Console.WriteLine();

            Console.WriteLine("***** Синдромы");

            int adjClassesCount = 2 << (R - 1);
            List<Vector> syndromes = new List<Vector>();
            for (int i = 0; i < adjClassesCount; i++)
            {
                //какого лидера надо умножать??
                var syndrome = AdjancencyClassesLeaders[i][0] * TransCheckMatrix;
                syndromes.Add(syndrome);
                for (int t = 0; t < R; t++)
                {
                    Console.Write(syndrome[0, t] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("*****");

            var input = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            Vector inputMatrix = new Vector(input);

            var decoded = inputMatrix * GeneratingMatrix;

            Console.Write("Encoded: ");
            for (int t = 0; t < N; t++)
            {
                Console.Write(decoded[0, t] + " ");
            }
            Console.WriteLine();

            var inputWithError = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
            var encoded = new Vector(inputWithError);

            var errorSyndrome = encoded * TransCheckMatrix;
            Console.Write("Syndrome Error: ");
            for (int t = 0; t < R; t++)
            {
                Console.Write(errorSyndrome[0, t] + " ");
            }
            Console.WriteLine();

            decimal position = 0;
            for (int i = 0; i < R; i++)
            {
                position += errorSyndrome[0, i] * (int)Math.Pow(2, R - i - 1);
            }

            Console.WriteLine("Position: " + position + "\n");
            var adjClass = AdjClasses[(int)position];
            LinearCodesType52.Print(adjClass);
            var leaders2 = AdjancencyClassesLeaders[(int)position];
            Console.WriteLine("Leaders: ");
            foreach (var leader in leaders2)
            {
                for (int i = 0; i < N; i++)
                {
                    Console.Write(leader[i] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n*****");

            if (leaders2.Count > 1)
            {
                Console.WriteLine("Correction can be ambiguous\n" +
                    "Possible variants:");
            } else
            {
                Console.WriteLine("Correction: ");
            }

            for (int i = 0; i < leaders2.Count; i++)
            {
                Vector vector = new Vector(N);
                for (int j = 0; j < N; j++)
                {
                    vector[j] = (inputWithError[j] + leaders2[i][j]) % 2;
                    Console.Write(vector[j] + " ");
                }
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}