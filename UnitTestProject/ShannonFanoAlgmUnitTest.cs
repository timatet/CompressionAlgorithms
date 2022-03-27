using Xunit;
using AlgorithmsLibrary;
using System.Collections.Generic;

namespace UnitTestProject
{
    public class ShannonFanoAlgmUnitTest
    {
        [Fact]
        public void EncodingStringAbracadabra()
        {
            string source = "abracadabra";

            string encoded = ShannonFanoAlgm.Encode(source);
            string expected = "001001001110011000100100";

            Assert.Equal(expected, encoded);
        }

        [Fact]
        public void EncodingStringAabbccdd()
        {
            string source = "aabbccdd";

            string encoded = ShannonFanoAlgm.Encode(source);
            string expected = "1111101001010000";

            Assert.Equal(expected, encoded);
        }
        [Fact]
        public void DecodeStringAbracadabraWithFrequencies()
        {
            string decoded = "001001001110011000100100";

            Dictionary<char, int> frequencies = new Dictionary<char, int>
            {
                {'a', 5}, {'b', 2}, {'r', 2}, {'c', 1}, {'d', 1}
            };

            string encoded = ShannonFanoAlgm.Decode(frequencies, decoded);
            string expected = "abracadabra";

            Assert.Equal(expected, encoded);
        }

        [Fact]
        public void DecodeStringAbracadabraWithCodes()
        {
            string decoded = "01101001110011110110100";

            Dictionary<char, string> codes = new Dictionary<char, string>
            {
                {'a', "0"}, {'b', "110"}, {'r', "10"}, {'c', "1110"}, {'d', "1111"}
            };

            string encoded = ShannonFanoAlgm.Decode(codes, decoded);
            string expected = "abracadabra";

            Assert.Equal(expected, encoded);
        }
    }
}