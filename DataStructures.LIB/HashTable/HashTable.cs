using DataStructures.LIB.DoublyLinkedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.LIB.HashTable
{
    /// <summary>
    /// Хеш таблица
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class HashTable<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {

        #region Fields

        private DoublyLinkedList<KeyValuePair<TKey, TValue>>[] _items;

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов хеш таблицы
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает хеш таблицу с заданным максимальным размером
        /// </summary>
        /// <param name="size"></param>
        public HashTable(int size)
        {
            _items = new DoublyLinkedList<KeyValuePair<TKey, TValue>>[size];

            for (int i = 0; i < size; i++)
            {
                _items[i] = new DoublyLinkedList<KeyValuePair<TKey, TValue>>();
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент с заданным ключем и значением в хеш таблицу
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        public void Add(TKey key, TValue value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key can`t be null");
            if (Count == _items.Length)
                throw new InvalidOperationException("Hashtable is full");

            int index = GetHash(key);

            if (_items[index].Any(i => i.Key.Equals(key)))
                throw new ArgumentException("Hashtable already contains the same key");

            _items[index].Add(new KeyValuePair<TKey, TValue>(key, value));
            Count++;
        }

        /// <summary>
        /// Удаляет элемент с заданным ключем из хеш таблицы
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key can`t be null");

            var index = GetHash(key);

            foreach (var keyValuePair in _items[index])
            {
                if (keyValuePair.Key.Equals(key))
                {
                    _items[index].Remove(keyValuePair);

                    Count--;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Ищет значение соответствующее заданному ключу
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue Find(TKey key)
        {
            var index = GetHash(key);

            foreach (var keyValuePair in _items[index])
            {
                if (keyValuePair.Key.Equals(key))
                {
                    return keyValuePair.Value;
                }
            }

            return default(TValue);
        }

        /// <summary>
        /// Очищает хеш таблицу
        /// </summary>
        public void Clear()
        {
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = new DoublyLinkedList<KeyValuePair<TKey, TValue>>();
            }

            Count = 0;
        }

        /// <summary>
        /// Возвращает значение, которое показывает есть ли в хеш таблице заданный ключ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key), "Key can`t be null");

            var index = GetHash(key);

            foreach (var keyValuePair in _items[index])
            {
                if (keyValuePair.Key.Equals(key))
                    return true;
            }

            return false;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Вычисляет хеш для заданного ключа
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int GetHash(TKey key)
        {
            return Math.Abs(key.GetHashCode() % _items.Length);
        }

        #endregion

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var keyValuePairList in _items)
            {
                foreach (var keyValuePair in keyValuePairList)
                {
                    yield return keyValuePair;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}