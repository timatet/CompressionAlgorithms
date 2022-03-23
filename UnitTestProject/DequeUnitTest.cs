using Xunit;
using AlgorithmsLibrary.CommonClasses;
using System.Collections.Generic;

namespace UnitTestProject
{
    public class DequeUnitTest
    {
        [Fact]
        public void ItemMustBeReturnedWithRequestAtIndex()
        {
            Deque<int> deque = new Deque<int>();

            deque.AddLeft(1);
            deque.AddRight(2);
            deque.AddLeft(3);
            deque.AddRight(4);

            List<int> expected = new List<int> { 3, 1, 2, 4 };
            
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.Equal(expected[i], deque[i]);
            }
        }
        
        [Fact]
        public void HotTest()
        {
            string inputString = "testString";
            Deque<char> establishingBuffer = new Deque<char>(inputString.ToLower().ToCharArray());

            
        }
    }
}