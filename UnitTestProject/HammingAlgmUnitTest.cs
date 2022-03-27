using Xunit;
using AlgorithmsLibrary;
using System;

namespace UnitTestProject
{
    public class HammingAlgmUnitTest
    {
        [Fact]
        public void EncodeHamming()
        {
            var encoded = HammingAlgm.Encode("10101011100");
            string expected = "011101001011100";
            Assert.Equal(expected, encoded);
        }
        
        [Theory]
        [InlineData("011101001011100", 0)]
        [InlineData("111101001011100", 1)]
        [InlineData("001101001011100", 2)]
        [InlineData("010101001011100", 3)]
        [InlineData("011001001011100", 4)]
        [InlineData("011111001011100", 5)]
        [InlineData("011100001011100", 6)]
        [InlineData("011101101011100", 7)]
        [InlineData("011101011011100", 8)]
        [InlineData("011101000011100", 9)]
        [InlineData("011101001111100", 10)]
        [InlineData("011101001001100", 11)]
        [InlineData("011101001010100", 12)]
        [InlineData("011101001011000", 13)]
        [InlineData("011101001011110", 14)]
        [InlineData("011101001011101", 15)]
        public void DecodeHamming(string input, int errorbit)
        {
            var encoded = HammingAlgm.Decode(input);
            string expected = "10101011100"; 
            Assert.Equal(expected, encoded.decodedMessage);
            Assert.Equal(errorbit, encoded.ErrorBit);
        }
        
        [Fact]
        public void EncodeHammingOneChar()
        {
            var encoded = HammingAlgm.Encode("1");
            string expected = "111";
            Assert.Equal(expected, encoded);
        }

        [Theory]
        [InlineData("111", 0)]
        [InlineData("011", 1)]
        [InlineData("101", 2)]
        [InlineData("110", 3)]
        public void DecodeHammingOneChar(string input, int errorbit)
        {
            var encoded = HammingAlgm.Decode(input);
            string expected = "1";
            Assert.Equal(expected, encoded.decodedMessage);
            Assert.Equal(errorbit, encoded.ErrorBit);
        }

        [Fact]
        public void ThrowingArgumentExceptionWithEncoding()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                HammingAlgm.Encode("10102000");
            });
        }

        [Fact]
        public void ThrowingArgumentExceptionWithDecoding()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                HammingAlgm.Decode("10102000");
            });
        }

        [Fact]
        public void Test_EncodeCharsStringASCII()
        {
            var actual = HammingAlgm.DecodeASCII("1000100110000111001010110110001110110001101111");
            Assert.Equal("Hello", actual);
        }
    }
}