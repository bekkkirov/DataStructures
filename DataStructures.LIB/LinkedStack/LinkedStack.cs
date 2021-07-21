using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LIB.LinkedStack
{
    /// <summary>
    /// Связной стек
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedStack<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// Вершина стека
        /// </summary>
        private LinkedStackItem<T> _head;

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов стека
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустой связной стек
        /// </summary>
        public LinkedStack()
        {

        }

        /// <summary>
        /// Создает стек и наполняет его элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public LinkedStack(IEnumerable<T> collection)
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
            var item = new LinkedStackItem<T>(data);

            if (_head == null)
            {
                _head = item;
            }

            else
            {
                item.Next = _head;
                _head = item;
            }

            Count++;
        }

        /// <summary>
        /// Возвращает верхний элемент стека и удаляет его
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("Stack is empty");

            var item = _head;

            _head = _head.Next;
            Count--;

            return item.Data;
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
                result = _head.Data;

                _head = _head.Next;

                Count--;
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

            return _head.Data;
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
                result = _head.Data;
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
            _head = null;
            Count = 0;
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            var current = _head;

            while (current != null)
            {
                yield return current.Data;

                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}