using System.Collections.Generic;
using System.Linq;

namespace DataStructures.LIB.Graph
{
    /// <summary>
    /// Граф
    /// </summary>
    public class Graph
    {
        #region Properties

        /// <summary>
        /// Список вершин
        /// </summary>
        public HashSet<Vertex> Vertices { get; private set; } = new HashSet<Vertex>();

        /// <summary>
        /// Список ребер
        /// </summary>
        public HashSet<Edge> Edges { get; private set; } = new HashSet<Edge>();

        /// <summary>
        /// Количество вершин
        /// </summary>
        public int VerticesCount => Vertices.Count;

        /// <summary>
        /// Количество ребер
        /// </summary>
        public int EdgesCount => Edges.Count;

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет вершину
        /// </summary>
        /// <param name="vertex"></param>
        public void AddVertex(Vertex vertex)
        {
            Vertices.Add(vertex);
        }

        /// <summary>
        /// Удаляет вершину из графа
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public bool RemoveVertex(Vertex vertex)
        {
            if (Vertices.Remove(vertex))
            {
                foreach (var edge in Edges.ToList())
                {
                    if (edge.From.Equals(vertex) || edge.To.Equals(vertex))
                    {
                        Edges.Remove(edge);
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Создает ребро
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="oriented"></param>
        /// <param name="weight"></param>
        public void AddEdge(Vertex from, Vertex to, bool oriented = false, double weight = 1)
        {
            Edges.Add(new Edge(from, to, oriented, weight));
        }

        /// <summary>
        /// Удаляет ребро из графа
        /// </summary>
        /// <param name="edge"></param>
        /// <returns></returns>
        public bool RemoveEdge(Edge edge)
        {
            return Edges.Remove(edge);
        }

        /// <summary>
        /// Возвращает матрицу смежности
        /// </summary>
        /// <returns></returns>
        public double[,] AdjacencyMatrix()
        {
            var adjacencyMatrix = new double[Vertices.Count, Vertices.Count];

            foreach (var edge in Edges)
            {
                var row = edge.From.Number - 1;
                var column = edge.To.Number - 1;

                adjacencyMatrix[row, column] = edge.Weight;

                if (!edge.Oriented)
                {
                    adjacencyMatrix[column, row] = edge.Weight;
                }
            }

            return adjacencyMatrix;
        }

        /// <summary>
        /// Возвращает словарь смежных вершин
        /// </summary>
        /// <returns></returns>
        public Dictionary<Vertex, List<Vertex>> AdjacentVertices()
        {
            var result = new Dictionary<Vertex, List<Vertex>>();

            foreach (var vertex in Vertices)
            {
                result.Add(vertex, AdjacentVertices(vertex));
            }

            return result;
        }

        /// <summary>
        /// Возвращает список вершин смежных к заданной
        /// </summary>
        /// <param name="vertex"></param>
        /// <returns></returns>
        public List<Vertex> AdjacentVertices(Vertex vertex)
        {
            var adjacentVertices = new List<Vertex>();

            foreach (var edge in Edges)
            {
                if (edge.From.Equals(vertex))
                {
                    adjacentVertices.Add(edge.To);
                }

                else if (edge.To.Equals(vertex) && !edge.Oriented)
                {
                    adjacentVertices.Add(edge.From);
                }
            }

            return adjacentVertices;
        }

        /// <summary>
        /// Обход в ширину
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public string BFS(Vertex start)
        {
            var result = new List<Vertex>() { start };

            for (int i = 0; i < result.Count; i++)
            {
                var v = result[i];

                foreach (var vertex in this.AdjacentVertices(v))
                {
                    if (!result.Contains(vertex))
                    {
                        result.Add(vertex);
                    }
                }
            }

            return string.Join(" ", result);
        }

        /// <summary>
        /// Обход в глубину
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public string DFS(Vertex start)
        {
            var result = new List<Vertex>();

            DFS(start, ref result);

            return string.Join(" ", result);
        }

        /// <summary>
        /// Очищает граф
        /// </summary>
        public void Clear()
        {
            Vertices = new HashSet<Vertex>();
            Edges = new HashSet<Edge>();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Обход в глубину
        /// </summary>
        /// <param name="start"></param>
        /// <param name="res"></param>
        /// <returns></returns>
        private void DFS(Vertex start, ref List<Vertex> res)
        {
            res.Add(start);

            var temp = res;

            foreach (var vertex in AdjacentVertices(start).Where(v => !temp.Contains(v)))
            {
                DFS(vertex, ref res);
            }
        }

        #endregion
    }
}