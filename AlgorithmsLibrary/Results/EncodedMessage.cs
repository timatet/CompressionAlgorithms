using System.Collections.Generic;
using System.Text;

namespace AlgorithmsLibrary
{
    internal class EncodedMessage<T> : IAlgmEncoded<T>
    {
        private T answer;

        public T GetAnswer()
        {
            return answer;
        }

        public override string ToString()
        {
            // В некоторых алгоритмах выхлопной ответ выглядит где то как строка
            // где то как массив "своих" объектов
            StringBuilder stringBuilder = new StringBuilder();
            if (typeof(T).Name == typeof(List<>).Name)
            {
                var m = (IEnumerable<object>)answer;
                foreach (var item in m)
                {
                    stringBuilder.Append(item.ToString());
                }

                return stringBuilder.ToString();
            }
            else if (typeof(T) == typeof(string))
            {
                return answer.ToString();
            }

            return base.ToString();
        }

        public EncodedMessage(T answer)
        {
            this.answer = answer;
        }
    }
}
