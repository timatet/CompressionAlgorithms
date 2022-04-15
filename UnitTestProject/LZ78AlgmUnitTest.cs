using AlgorithmsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace UnitTestProject
{
    public class LZ78AlgmUnitTest
    {
        [Fact]
        public void EncodeStringWithRepeatsLongString()
        {
            var result = LZ78Algm.Encode("abracadabraabracadabra");

            var expectedCodeBlocks = new List<LZ78CodeBlock> {
                new LZ78CodeBlock(0, 'a'),
                new LZ78CodeBlock(0, 'b'),
                new LZ78CodeBlock(0, 'r'),
                new LZ78CodeBlock(1, 'c'),
                new LZ78CodeBlock(1, 'd'),
                new LZ78CodeBlock(1, 'b'),
                new LZ78CodeBlock(3, 'a'),
                new LZ78CodeBlock(6, 'r'),
                new LZ78CodeBlock(4, 'a'),
                new LZ78CodeBlock(0, 'd'),
                new LZ78CodeBlock(8, 'a'),
            };

            Assert.Equal(expectedCodeBlocks, result.GetAnswer());
        }
        
        [Fact]
        public void DecodedStringWithRepeatsLongString()
        {
            var result = LZ78Algm.Decode("(0,a)(0,b)(0,r)(1,c)(1,d)(1,b)(3,a)(6,r)(4,a)(0,d)(8,a)");

            var expectedCodeBlocks = "abracadabraabracadabra";

            Assert.Equal(expectedCodeBlocks, result.GetAnswer());
        }
    }
}
