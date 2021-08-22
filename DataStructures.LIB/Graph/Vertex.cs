using System;

namespace DataStructures.LIB.Graph
{
    /// <summary>
    /// Вершина графа
    /// </summary>
    public class Vertex
    {
        #region Properties

        /// <summary>
        /// Номер вершины
        /// </summary>
        public int Number { get; }

        #endregion

        #region Constructors

        public Vertex(int number)
        {
            if (number <= 0)
                throw new ArgumentException("Number can`t be less or equal to zero");

            Number = number;
        }

        #endregion

        public override string ToString()
        {
            return Number.ToString();
        }

        public override bool Equals(object? obj)
        {
            if (obj is Vertex vertex)
                return Number == vertex.Number;

            throw new ArgumentException("Object isn`t vertex", nameof(obj));
        }

        public override int GetHashCode()
        {
            return Number.GetHashCode();
        }
    }
}