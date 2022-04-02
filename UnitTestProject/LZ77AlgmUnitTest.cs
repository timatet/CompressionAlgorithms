using AlgorithmsLibrary;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class LZ77AlgmUnitTest
    {
        [Fact]
        public void EncodeEmptyString()
        {
            var result = LZ77Algm.Encode("");

            var expectedCodeBlocks = new List<CodeBlock>();

            Assert.Equal(expectedCodeBlocks, result.GetAnswer());
        }

        [Fact]
        public void EncodeOneCharString()
        {
            var result = LZ77Algm.Encode("a");

            var expectedCodeBlocks = new List<CodeBlock>
            {
                new CodeBlock(0,0,'a')
            };

            Assert.Equal(expectedCodeBlocks, result.GetAnswer());
        }

        [Fact]
        public void EncodeStringWithoutRepeats()
        {
            var result = LZ77Algm.Encode("abcd");

            var expectedCodeBlocks = new List<CodeBlock> {
                new CodeBlock(0,0,'a'),
                new CodeBlock(0,0,'b'),
                new CodeBlock(0,0,'c'),
                new CodeBlock(0,0,'d')
            };

            var test = LZ77Algm.Decode(expectedCodeBlocks);

            Assert.Equal(expectedCodeBlocks, result.GetAnswer());
        }

        [Fact]
        public void EncodeStringWithRepeats()
        {
            var result = LZ77Algm.Encode("abab");

            var expectedCodeBlocks = new List<CodeBlock> {
                new CodeBlock(0,0,'a'),
                new CodeBlock(0,0,'b'),
                new CodeBlock(2,2,'$')
            };

            Assert.Equal(expectedCodeBlocks, result.GetAnswer());
        }

        [Fact]
        public void EncodeStringWithRepeatsLongString()
        {
            var result = LZ77Algm.Encode("abracadabraabracadabra");

            var expectedCodeBlocks = new List<CodeBlock> {
                new CodeBlock(0,0,'a'),
                new CodeBlock(0,0,'b'),
                new CodeBlock(0,0,'r'),
                new CodeBlock(3,1,'c'),
                new CodeBlock(5,1,'d'),
                new CodeBlock(7,4,'a'),
                new CodeBlock(11,10,'$') };

            Assert.Equal(expectedCodeBlocks, result.GetAnswer());
        }

        [Fact]
        public void CalculateCompressionRatioOfLongStringWithRepeats()
        {
            string stringForEncoding = "abracadabraabracadabra";
            var result = LZ77Algm.Encode(stringForEncoding);

            double actual = Math.Round(LZ77Algm.CalculateCompressionRatio(stringForEncoding, result.ToString()), 2);
            double expected = 2.05;

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("abcde")]
        [InlineData("abab")]
        [InlineData("abracadabraabracadabra")]
        [InlineData("aaaa")]
        public void DecodingOfEmptyString(string stringForEncoding)
        {
            var result = LZ77Algm.Encode(stringForEncoding);

            var decodeResult = LZ77Algm.Decode(result.GetAnswer());

            Assert.Equal(stringForEncoding, decodeResult.GetAnswer());
        }


    }
}