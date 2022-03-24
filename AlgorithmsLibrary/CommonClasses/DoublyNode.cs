﻿using System;

namespace AlgorithmsLibrary.CommonClasses
{
    /// <summary>
    /// Узел двусвязного списка (взвешенного дерева). Previous ~ Left. Next ~ Right.
    /// </summary>
    /// <typeparam name="T">Тип хранения данных в узле</typeparam>
    public class DoublyNode<T> : IComparable<DoublyNode<T>>, IEquatable<DoublyNode<T>>
    {
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

        public DoublyNode(T data) : this(data, 0) { }
        public DoublyNode(T data, int weight)
        {
            Data = data;
            Weight = weight;
        }
        public DoublyNode(DoublyNode<T> left, DoublyNode<T> right)
        {
            Previous = left;
            Next = right;
            Weight = Previous.Weight + Next.Weight;
            Data = default;
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
