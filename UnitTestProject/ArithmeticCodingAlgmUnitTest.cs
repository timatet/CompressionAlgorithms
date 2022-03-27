using AlgorithmsLibrary;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class ArithmeticCodingAlgmUnitTest
    {
        [Fact]
        public void EncodingStringAabcb()
        {
            string source = "aabcb";

            string encoded = ArithmeticCodingAlgm.Encode(source);
            string expected = "125";

            Assert.Equal(expected, encoded);
        }
        [Fact]
        public void DecodingStringAabcb()
        {
            string encoded = "125";
            Dictionary<char, int> frequencies = new Dictionary<char, int>
            {
                {'a', 2}, {'b', 2}, {'c', 1}
            };

            string decoded = ArithmeticCodingAlgm.Decode(frequencies, encoded, 5);
            string expected = "aabcb";

            Assert.Equal(expected, decoded);
        }
    }
}