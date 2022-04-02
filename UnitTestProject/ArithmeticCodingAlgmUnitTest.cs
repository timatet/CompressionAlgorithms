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

            var encoded = ArithmeticCodingAlgm.Encode(source);
            string expected = "121";

            Assert.Equal(expected, encoded.GetAnswer());
        }
        [Fact]
        public void DecodingStringAabcb()
        {
            string encoded = "125";
            Dictionary<char, int> frequencies = new Dictionary<char, int>
            {
                {'a', 2}, {'b', 2}, {'c', 1}
            };

            var decoded = ArithmeticCodingAlgm.Decode(frequencies, encoded, 5);
            string expected = "aabcb";

            Assert.Equal(expected, decoded.GetAnswer());
        }
    }
}