using Xunit;
using AlgorithmsLibrary;

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
        public void CalculateReverseBWTForAbracadabraString()
        {
            string input = "rdarcaaaabb";

            var actual = BurrowsWheelerTransform.Decode(input, 2);
            string expected = "abracadabra";

            Assert.Equal(expected, actual);
        }
    }
}