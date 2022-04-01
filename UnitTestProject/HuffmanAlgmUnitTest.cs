using Xunit;
using AlgorithmsLibrary;
using System.Collections.Generic;

namespace UnitTestProject
{
    public class HuffmanAlgmUnitTest
    {
        [Fact]
        public void EncodingStringAabbccdd()
        {
            string source = "aabbccdd";

            var encoded = HuffmanAlgm.Encode(source);
            
            string expected = "1010111100000101";
            Assert.Equal(expected, encoded.GetAnswer());
            Assert.Equal(8 * 2, encoded.GetAnswer().Length);
        }
        [Fact]
        public void EncodingStringAbracadabra()
        {
            string source = "abracadabra";

            var encoded = HuffmanAlgm.Encode(source);
            string expected = "01101001110011110110100";
            
            Assert.Equal(expected, encoded.GetAnswer());
        }
        
        [Fact]
        public void DecodeStringAbracadabraWithFrequencies()
        {
            string decoded = "01101001110011110110100";

            Dictionary<char, int> frequencies = new Dictionary<char, int>
            {
                {'a', 5}, {'b', 2}, {'r', 2}, {'c', 1}, {'d', 1}
            };

            var encoded = HuffmanAlgm.Decode(frequencies, decoded);
            string expected = "abracadabra";
            
            Assert.Equal(expected, encoded.GetAnswer());
        }
        
        [Fact]
        public void DecodeStringAbracadabraWithCodes()
        {
            string decoded = "01101001110011110110100";

            Dictionary<char, string> codes = new Dictionary<char, string>
            {
                {'a', "0"}, {'b', "110"}, {'r', "10"}, {'c', "1110"}, {'d', "1111"}
            };

            var encoded = HuffmanAlgm.Decode(codes, decoded);
            string expected = "abracadabra";

            Assert.Equal(expected, encoded.GetAnswer());
        }
    }
}