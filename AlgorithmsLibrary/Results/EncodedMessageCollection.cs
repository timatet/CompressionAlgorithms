using System.Collections.Generic;

namespace AlgorithmsLibrary
{
    internal class EncodedMessage<T1, T2> : IAlgmEncoded<T1, T2>
    {
        private T1 answer;
        private T2 data;
        public T1 GetAnswer()
        {
            return answer;
        }

        public T2 GetData()
        {
            return data;
        }
        public override string ToString()
        {
            if (typeof(T1) == typeof(string))
            {
                return answer.ToString();
            }

            return base.ToString();
        }

        public EncodedMessage(T1 answer, T2 frequencies)
        {
            this.answer = answer;
            this.data = frequencies;
        }
    }
}
