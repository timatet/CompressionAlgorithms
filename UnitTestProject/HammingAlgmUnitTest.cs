using Xunit;
using AlgorithmsLibrary;

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
        
        [Fact]
        public void EncodeHammingOneChar()
        {
            var encoded = HammingAlgm.Encode("1");
            string expected = "111";
            Assert.Equal(expected, encoded);
        }
    }
}