using System;
using System.Collections.Generic;

namespace DataStructures.LIB.Trie
{
    /// <summary>
    /// Узел префиксного дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TrieNode<T>
    {
        #region Fields

        private T _data;

        #endregion

        #region Properties

        /// <summary>
        /// Буква соответствующая узлу
        /// </summary>
        public char Symbol { get; set; }

        /// <summary>
        /// Значение, которое хранит узел
        /// </summary>
        public T Data
        {
            get => _data;
            set => _data = value ?? throw new ArgumentNullException(nameof(Data), "Node data can`t be null");
        }

        /// <summary>
        /// Префикс
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Дочерние узлы
        /// </summary>
        public Dictionary<char, TrieNode<T>> SubNodes { get; set; } = new Dictionary<char, TrieNode<T>>(new CaseInsensitiveCharComparer());

        /// <summary>
        /// Показывает является ли узел концом слова
        /// </summary>
        public bool IsEndOfWord { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает узел префиксного дерева
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="data"></param>
        /// <param name="prefix"></param>
        public TrieNode(char symbol, T data, string prefix)
        {
            Symbol = symbol;
            Data = data;
            Prefix = prefix;
        }

        #endregion

        /// <summary>
        /// Возвращает дочерний узел с заданной буквой
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TrieNode<T> TryGetSubNode(char key)
        {
            if (SubNodes.TryGetValue(key, out TrieNode<T> value))
            {
                return value;
            }

            return null;
        }

        public override string ToString()
        {
            return $"{Symbol}, {Data}, {Prefix}";
        }
    }
}