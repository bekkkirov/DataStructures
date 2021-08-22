using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LIB.Dictionary
{
    /// <summary>
    /// Словарь
    /// </summary>
    /// <typeparam name="TKey">Ключ</typeparam>
    /// <typeparam name="TValue">Значение</typeparam>
    public class Dictionary<TKey, TValue> : IEnumerable<DictionaryItem<TKey, TValue>>
    {

        #region Fields

        private DictionaryItem<TKey, TValue>[] _items;

        #endregion

        #region Properties

        /// <summary>
        /// Список ключей, хранящихся в словаре
        /// </summary>
        public List<TKey> Keys { get; } = new List<TKey>();

        /// <summary>
        /// Список значений, хранящихся в словаре
        /// </summary>
        public List<TValue> Values { get; } = new List<TValue>();

        /// <summary>
        /// Количество элементов словаря
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        public Dictionary(int size)
        {
            if (size < 0)
                throw new ArgumentException("Size can`t be less than zero", nameof(size));

            _items = new DictionaryItem<TKey, TValue>[size];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Возвращает true, если словарь содержит элемент с заданным ключом
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            if (Find(key) != null)
                return true;

            return false;
        }

        /// <summary>
        /// Добавляет элемент в словарь
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            if (this.ContainsKey(key))
                throw new ArgumentException("Dictionary already contains the same key");

            var item = new DictionaryItem<TKey, TValue>(key, value);
            var hash = GetHash(item.Key);

            for (int i = hash; i < _items.Length; i++)
            {
                if (TryInsert(i, item))
                    return;
            }

            for (int i = 0; i < hash; i++)
            {
                if (TryInsert(i, item))
                    return;
            }

            throw new InvalidOperationException("Dictionary is full");
        }

        /// <summary>
        /// Удаляет элемент из словаря
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(TKey key)
        {
            var hash = GetHash(key);

            for (int i = hash; i < _items.Length; i++)
            {
                if (TryRemove(i, key))
                    return true;
            }

            for (int i = 0; i < hash; i++)
            {
                if (TryRemove(i, key))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Добавляет элемент в словарь. Возвращает true, если операция удалась
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryAdd(TKey key, TValue value)
        {
            if (this.ContainsKey(key))
                return false;

            var item = new DictionaryItem<TKey, TValue>(key, value);
            var hash = GetHash(item.Key);

            for (int i = hash; i < _items.Length; i++)
            {
                if (TryInsert(i, item))
                    return true;
            }

            for (int i = 0; i < hash; i++)
            {
                if (TryInsert(i, item))
                    return true;
            }

            throw new InvalidOperationException("Dictionary is full");
        }

        /// <summary>
        /// Получает значение элемента с заданным ключом.
        /// Возвращает true, если удалось найти нужный элемент и записывает его значение в переменную с параметром out
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetValue(TKey key, out TValue value)
        {
            var dictItem = Find(key);

            if (dictItem != null)
            {
                value = dictItem.Value;
                return true;
            }

            value = default(TValue);
            return false;
        }

        /// <summary>
        /// Очищает словарь
        /// </summary>
        public void Clear()
        {
            int size = _items.Length;
            _items = new DictionaryItem<TKey, TValue>[size];

            Keys.Clear();
            Values.Clear();

            Count = 0;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Вставляет элемент в словарь по индексу. Возвращает true, если операция удалась
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool TryInsert(int index, DictionaryItem<TKey, TValue> item)
        {
            if (_items[index] == null)
            {
                _items[index] = item;

                Keys.Add(item.Key);
                Values.Add(item.Value);

                Count++;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Удаляет элемент из словаря по индексу. Возвращает true, если операция удалась
        /// </summary>
        /// <param name="index"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private bool TryRemove(int index, TKey key)
        {
            if (_items[index] != null && _items[index].Key.Equals(key))
            {
                Keys.Remove(key);
                Values.Remove(_items[index].Value);

                _items[index] = null;
                Count--;

                return true;
            }

            return false;
        }

        /// <summary>
        /// Возвращает элемент словаря с заданным ключом
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private DictionaryItem<TKey, TValue> Find(TKey key)
        {
            var hash = GetHash(key);

            for (int i = hash; i < _items.Length; i++)
            {
                if (_items[i] != null && _items[i].Key.Equals(key))
                    return _items[i];
            }

            for (int i = 0; i < hash; i++)
            {
                if (_items[i] != null && _items[i].Key.Equals(key))
                    return _items[i];
            }

            return null;
        }

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

        public TValue this[TKey key]
        {
            get
            {
                TValue result;

                if (TryGetValue(key, out result))
                    return result;
                throw new ArgumentException("Dictionary doesn`t contain item with specified key");

            }
        }

        public IEnumerator<DictionaryItem<TKey, TValue>> GetEnumerator()
        {
            foreach (var dictionaryItem in _items)
            {
                if (dictionaryItem != null)
                    yield return dictionaryItem;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}