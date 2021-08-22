using System;
using System.Collections.Generic;

namespace DataStructures.LIB.Trie
{
    /// <summary>
    /// Префиксное дерево
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Trie<T>
    {

        #region Fields

        /// <summary>
        /// Корень дерева
        /// </summary>
        private TrieNode<T> _root;

        #endregion

        #region Properties

        /// <summary>
        /// Количество слов в дереве
        /// </summary>
        public int Count { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает префиксное дерево
        /// </summary>
        public Trie()
        {
            _root = new TrieNode<T>('\0', default(T), "");
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в дерево
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Add(string key, T data)
        {
            Add(key, data, _root);
        }

        /// <summary>
        /// Удаляет элемент из дерева
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)
        {
            return Remove(key, _root);
        }

        /// <summary>
        /// Возвращает значение, которое хранит заданное слово
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Search(string key)
        {
            return Search(key, _root);
        }

        /// <summary>
        /// Возвращает список слов, начинающихся с заданного фрагмента
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<string> StartsWith(string prefix)
        {
            return StartsWith(prefix, _root);
        }

        /// <summary>
        /// Очищает префиксное дерево
        /// </summary>
        public void Clear()
        {
            _root = new TrieNode<T>('\0', default(T), ""); ;
            Count = 0;
        }

        #endregion

        #region Private methods

        private List<string> StartsWith(string prefix, TrieNode<T> item)
        {
            if (string.IsNullOrEmpty(prefix))
            {
                var words = new List<string>();

                if (item.IsEndOfWord)
                {
                    words.Add(item.Prefix);
                }

                FindAllWords(item, ref words);

                return words;
            }

            else
            {
                var subNode = item.TryGetSubNode(prefix[0]);

                if (subNode != null)
                {
                    return StartsWith(prefix.Substring(1), subNode);
                }
            }

            return new List<string>();
        }

        private void FindAllWords(TrieNode<T> item, ref List<string> words)
        {
            foreach (var subNode in item.SubNodes)
            {
                if (subNode.Value.IsEndOfWord)
                {
                    words.Add(subNode.Value.Prefix);
                }

                FindAllWords(subNode.Value, ref words);
            }
        }

        private void Add(string key, T data, TrieNode<T> item)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (!item.IsEndOfWord)
                {
                    item.Data = data;
                    item.IsEndOfWord = true;

                    Count++;
                }
            }

            else
            {
                var subNode = item.TryGetSubNode(key[0]);

                if (subNode != null)
                {
                    Add(key.Substring(1), data, subNode);
                }

                else
                {
                    var node = new TrieNode<T>(key[0], data, item.Prefix + key[0]);
                    item.SubNodes.Add(key[0], node);
                    Add(key.Substring(1), data, node);
                }
            }
        }

        private bool Remove(string key, TrieNode<T> item)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (item.IsEndOfWord)
                {
                    item.IsEndOfWord = false;
                    Count--;

                    return true;
                }
            }

            else
            {
                var subNode = item.TryGetSubNode(key[0]);

                if (subNode != null)
                {
                    return Remove(key.Substring(1), subNode);
                }
            }

            return false;
        }

        private T Search(string key, TrieNode<T> item)
        {
            if (string.IsNullOrEmpty(key))
            {
                if (item.IsEndOfWord)
                {
                    return item.Data;
                }
            }

            else
            {
                var subNode = item.TryGetSubNode(key[0]);

                if (subNode != null)
                {
                    return Search(key.Substring(1), subNode);
                }
            }

            return default(T);
        } 

        #endregion

    }
}