using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DataStructures.LIB.ListStack
{
    /// <summary>
    /// Стек на основе списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListStack<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// Список содержащий элементы стека
        /// </summary>
        private List<T> _items;

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов стека
        /// </summary>
        public int Count => _items.Count;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустой стек
        /// </summary>
        public ListStack()
        {
            _items = new List<T>();
        }

        /// <summary>
        /// Создает стек и наполняет его элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public ListStack(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                this.Push(item);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в стек
        /// </summary>
        /// <param name="data"></param>
        public void Push(T data)
        {
            _items.Add(data);
        }

        /// <summary>
        /// Возвращает верхний элемент стека и удаляет его
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty");

            var item = _items.Last();
            _items.RemoveAt(Count - 1);

            return item;
        }

        /// <summary>
        /// Возвращает значение, которое показывает есть ли на вершине стека элемент. Если есть копирует его в переменную с параметром out и удаляет его
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryPop(out T result)
        {
            if (Count > 0)
            {
                result = _items.Last();
                _items.RemoveAt(Count - 1);
                return true;
            }

            result = default(T);
            return false;
        }

        /// <summary>
        /// Возвращает верхний элемент стека не удаляя его
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty");

            return _items.Last();
        }

        /// <summary>
        /// Возвращает значение, которое показывает есть ли на вершине стека элемент. Если есть копирует его в переменную с параметром out
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryPeek(out T result)
        {
            if (Count > 0)
            {
                result = _items.Last();
                return true;
            }

            result = default(T);
            return false;
        }

        /// <summary>
        /// Очищает стек
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                yield return _items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}