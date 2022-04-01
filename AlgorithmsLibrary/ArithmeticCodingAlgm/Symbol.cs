using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgorithmsLibrary
{
    public class Symbol : IComparable<Symbol>
    {
        public char Data { get; set; }
        private decimal lowRange;
        public decimal LowRange
        {
            get { return lowRange; }
            set { lowRange = value; HighRange = value + Frequency; }
        }
        public decimal HighRange { get; private set; }
        public decimal Frequency { get; set; }
        public Symbol(char data, decimal frequency)
        {
            Data = data;
            Frequency = frequency;
        }
        public Symbol(char data, decimal frequency, decimal lowRange)
        {
            Data = data;
            Frequency = frequency;
            LowRange = lowRange;
        }
        public int CompareTo(Symbol other)
        {
            return Frequency.CompareTo(other.Frequency);
        }
    }
}
