using AlgorithmsLibrary;
using System;
using System.Collections.Generic;
using Xunit;

namespace UnitTestProject
{
    public class RLEAlgmBWTUnitTest
    {
        [Fact]
        public void CalculateBWTForAbracadabraString()
        {
            string input = "abracadabra";

            var actual = BurrowsWheelerTransform.Encode(input);
            string expected = "rdarcaaaabb";

            Assert.Equal(expected, actual.encoded);
        }
        
        [Fact]
        public void CalculateCompressionRatio()
        {
            string input = "abracadabra";

            var encoded = RLEAlgm.Encode(input);
            double expected = 1.294;
            double actual = encoded.GetCompressionRatio();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CalculateReverseBWTForAbracadabraString()
        {
            string input = "rdarcaaaabb";

            var actual = BurrowsWheelerTransform.Decode(input, 2);
            string expected = "abracadabra";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void EncodeAbracadabraWithRLE()
        {
            string input = "abracadabra";

            var actual = RLEAlgm.Encode(input);
            var expected = new List<RLECodeBlock> {
                new RLECodeBlock(default, 2),
                new RLECodeBlock('r', 1),
                new RLECodeBlock('d', 1),
                new RLECodeBlock('a', 1),
                new RLECodeBlock('r', 1),
                new RLECodeBlock('c', 1),
                new RLECodeBlock('a', 4),
                new RLECodeBlock('b', 2),
            };

            Assert.Equal(expected, actual.GetAnswer());
        }

        [Fact]
        public void DecodeAbracadabraWithRLE()
        {
            var decoded = new List<RLECodeBlock> {
                new RLECodeBlock(default, 2),
                new RLECodeBlock('r', 1),
                new RLECodeBlock('d', 1),
                new RLECodeBlock('a', 1),
                new RLECodeBlock('r', 1),
                new RLECodeBlock('c', 1),
                new RLECodeBlock('a', 4),
                new RLECodeBlock('b', 2),
            };

            var actual = RLEAlgm.Decode(decoded);
            var expected = "abracadabra";

            Assert.Equal(expected, actual.GetAnswer());
        }

        [Fact]
        public void EncodeEmptyString()
        {
            var input = string.Empty;
            Assert.Throws<ArgumentNullException>(() =>
            {
                var decoded = RLEAlgm.Encode(input);
            });
        }

        [Fact]
        public void DecodeEmptyString()
        {
            var input = new List<RLECodeBlock> { };
            Assert.Throws<ArgumentNullException>(() =>
            {
                var decoded = RLEAlgm.Decode(input);
            });
        }

        [Fact]
        public void EncodeWhitespaceString()
        {
            var input = " ";
            var decoded = RLEAlgm.Encode(input);
            var expected = new List<RLECodeBlock>
            {
                new RLECodeBlock(default, 0),
                new RLECodeBlock(' ', 1)
            };

            Assert.Equal(expected, decoded.GetAnswer());
        }

        [Fact]
        public void DecodeWhitespaceString()
        {
            var decoded = new List<RLECodeBlock>
            {
                new RLECodeBlock(default, 0),
                new RLECodeBlock(' ', 1)
            };
            var encoded = RLEAlgm.Decode(decoded);
            var expected = " ";

            Assert.Equal(expected, encoded.GetAnswer());
        }
    }
}