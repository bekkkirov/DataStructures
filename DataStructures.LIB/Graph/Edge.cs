using System;

namespace DataStructures.LIB.Graph
{
    /// <summary>
    /// Ребро графа
    /// </summary>
    public class Edge
    {
        #region Properties

        /// <summary>
        /// Начальная вершина ребра
        /// </summary>
        public Vertex From { get; }

        /// <summary>
        /// Конечная вершина ребра
        /// </summary>
        public Vertex To { get; }

        /// <summary>
        /// Показывает является ли ребро ориентированным
        /// </summary>
        public bool Oriented { get; }

        /// <summary>
        /// Вес ребра
        /// </summary>
        public double Weight { get; set; }

        #endregion

        #region Constructors

        public Edge(Vertex from, Vertex to, bool oriented = false, double weight = 1)
        {
            From = from ?? throw new ArgumentNullException(nameof(from), "\"From\" vertex can`t be null");
            To = to ?? throw new ArgumentNullException(nameof(to), "\"To\" vertex can`t be null");
            Oriented = oriented;
            Weight = weight;
        }

        #endregion

        public override string ToString()
        {
            return $"({From},{To})";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Edge edge)
                return this.From.Equals(edge.From) && this.To.Equals(edge.To);

            throw new ArgumentException("Object isn`t edge");
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}