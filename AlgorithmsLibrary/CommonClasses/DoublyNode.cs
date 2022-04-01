using System;
using System.Collections.Generic;

namespace AlgorithmsLibrary.CommonClasses
{
    /// <summary>
    /// Узел двусвязного списка (взвешенного дерева). Previous ~ Left. Next ~ Right.
    /// </summary>
    /// <typeparam name="T">Тип хранения данных в узле</typeparam>
    public class DoublyNode<T> : IComparable<DoublyNode<T>>, IEquatable<DoublyNode<T>>
    {
        private int _Level = 1;
        private int _CountBinaryVertex = 0;

        /// <summary>
        /// Данные, хранящиеся в узле.
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Вес узла. По стандарту сумма весов предыдщего и следующего связных узлов. (0)
        /// </summary>
        public int Weight { get; private set; }
        /// <summary>
        /// Предыдущий (левый) связный узел.
        /// </summary>
        public DoublyNode<T> Previous { get; set; }
        /// <summary>
        /// Следующий (правый) связный узел.
        /// </summary>
        public DoublyNode<T> Next { get; set; }
        /// <summary>
        /// Родительский узел.
        /// </summary>
        public DoublyNode<T> Parent { get; private set; }
        /// <summary>
        /// Количество уровней глубины поддерева.
        /// </summary>
        public int Level
        {
            get
            {
                return _Level;
            }
            private set
            {
                _Level = value;
                if (Previous != null)
                    Previous.Level = value - 1;
                if (Next != null)
                    Next.Level = value - 1;
            }
        }
        /// <summary>
        /// Число вершин в поддереве из всех вершин поддерева, включая корневую, у которых два потомка.
        /// </summary>
        public int CountBinaryVertex
        {
            get
            {
                return _CountBinaryVertex;
            }
            private set
            {
                _CountBinaryVertex++;
                if (Parent != null)
                    Parent.CountBinaryVertex++;
            }
        }
        /// <summary>
        /// Количество висячих (свободных) узлов в поддереве.
        /// </summary>
        public int CountHangingVertex { get { return CountBinaryVertex + 1; } private set { } }

        public DoublyNode(T data) : this(data, 0) { }
        public DoublyNode(T data, int weight)
        {
            Data = data;
            Weight = weight;
        }
        /// <summary>
        /// Создает новый узел как объединение двух потомков.
        /// </summary>
        public DoublyNode(DoublyNode<T> left, DoublyNode<T> right)
        {
            Previous = left;
            Next = right;
            Weight = Previous.Weight + Next.Weight;
            Data = default;
            left.Parent = right.Parent = this;
            //Пересчитываем уровень дерева
            int MaxHeight = Math.Max(left.Level, right.Level);
            left.Level = right.Level = MaxHeight;
            Level = MaxHeight + 1;
            //Увеличваем количество бинарных вершин
            CountBinaryVertex = left.CountBinaryVertex + right.CountBinaryVertex + 1;
        }

        public Dictionary<T, string> InOrderTraversal()
        {
            Dictionary<T, string> codes = new Dictionary<T, string>();
            Stack<DoublyNode<T>> stack = new Stack<DoublyNode<T>>();
            var current = this;
            bool goLeftNext = true;

            stack.Push(current);

            string buffer = string.Empty;
            while (stack.Count > 0)
            {
                if (goLeftNext)
                {
                    while (current.Previous != null)
                    {
                        stack.Push(current);
                        current = current.Previous;
                        buffer += "0";
                    }
                }

                //проверка является ли вершина обхода листом
                //вершина является листом если ей присвоено значение != default
                if (!current.Data.Equals(default(T)))
                {
                    codes.Add(current.Data, buffer);
                }

                if (current.Next != null)
                {
                    current = current.Next;
                    buffer += "1";
                    goLeftNext = true;
                }
                else
                {
                    int actualLevel = current.Level;
                    current = stack.Pop();
                    while (current.Level - actualLevel++ > 0)
                    {
                        buffer = buffer.Remove(buffer.Length - 1);
                    }
                    goLeftNext = false;
                }
            }

            return codes;
        }

        public int CompareTo(DoublyNode<T> other)
        {
            return Weight.CompareTo(other.Weight);
        }
        public bool Equals(DoublyNode<T> other)
        {
            return Data.Equals(other.Data) && Weight == other.Weight;
        }
    }
}
