using AlgorithmsLibrary;
using System;
using Xunit;

namespace UnitTestProject
{
    public class HammingAlgmUnitTest
    {
        [Fact]
        public void EncodeHamming()
        {
            var encoded = HammingAlgm.Encode("10101011100");
            string expected = "011101001011100";
            Assert.Equal(expected, encoded.GetAnswer());
        }

        [Fact]
        public void EncodeHamming2()
        {
            var encoded = HammingAlgm.Encode("10100101010010100101");
            string expected = "1011010001010100010100101";
            Assert.Equal(expected, encoded.GetAnswer());
        }

        [Theory]
        [InlineData("1011010001010100010100101", 0)] // 0bit
        [InlineData("1011010001010100010100100", 25)] // 25bit
        [InlineData("1011010001010100010100111", 24)] // 24bit
        [InlineData("1011010001010100010100001", 23)] // 23bit
        [InlineData("1011010001010100010101101", 22)] // 22bit
        [InlineData("1011010001010100010110101", 21)] // 21bit
        [InlineData("1011010001010100010000101", 20)] // 20bit
        [InlineData("1011010001010100011100101", 19)] // 19bit
        [InlineData("1011010001010100000100101", 18)] // 18bit
        [InlineData("1011010001010100110100101", 17)] // 17bit
        [InlineData("1011010001010101010100101", 16)] // 16bit
        [InlineData("1011010001010110010100101", 15)] // 15bit
        [InlineData("1011010001010000010100101", 14)] // 14bit
        [InlineData("1011010001011100010100101", 13)] // 13bit
        [InlineData("1011010001000100010100101", 12)] // 12bit
        [InlineData("1011010001110100010100101", 11)] // 11bit
        [InlineData("1011010000010100010100101", 10)] // 10bit
        [InlineData("1011010011010100010100101", 9)] // 9bit
        [InlineData("1011010101010100010100101", 8)] // 8bit
        [InlineData("1011011001010100010100101", 7)] // 7bit
        [InlineData("1011000001010100010100101", 6)] // 6bit
        [InlineData("1011110001010100010100101", 5)] // 5bit
        [InlineData("1010010001010100010100101", 4)] // 4bit
        [InlineData("1001010001010100010100101", 3)] // 3bit
        [InlineData("1111010001010100010100101", 2)] // 2bit
        [InlineData("0011010001010100010100101", 1)] // 1bit
        public void DecodeHamming2(string decode, int biterr)
        {
            var encoded = HammingAlgm.Decode(decode);
            string expected = "10100101010010100101";
            Assert.Equal(expected, encoded.GetAnswer());
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
            Assert.Equal(expected, encoded.GetAnswer());
            //Assert.Equal(errorbit, encoded.ErrorBit);
        }

        [Fact]
        public void EncodeHammingOneChar()
        {
            var encoded = HammingAlgm.Encode("1");
            string expected = "111";
            Assert.Equal(expected, encoded.GetAnswer());
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
            Assert.Equal(expected, encoded.GetAnswer());
            //Assert.Equal(errorbit, encoded.ErrorBit);
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
            Assert.Equal("Hello", actual.GetAnswer());
        }
    }
}