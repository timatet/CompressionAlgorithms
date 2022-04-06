using System;

namespace AlgorithmsLibrary.CommonClasses
{
    public class CodingException : Exception
    {
        public CodingException()
            : base("Error in coding!") { }
        public CodingException(string message)
            : base(message) { }
    }
}
