using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string source = "aabbccdd";

            string encoded = HuffmanAlgm.Encode(source);
        }
    }
}
