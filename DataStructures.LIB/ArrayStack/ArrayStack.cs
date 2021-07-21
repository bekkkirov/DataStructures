using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LIB.ArrayStack
{
    /// <summary>
    /// Стек на основе массива
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayStack<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// Массив, который хранит элементы стека
        /// </summary>
        private T[] _stack;

        /// <summary>
        /// Индекс верхнего элемента стека
        /// </summary>
        private int _current = -1;

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов стека
        /// </summary>
        public int Count => _current + 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустой стек
        /// </summary>
        public ArrayStack()
        {
            _stack = new T[0];
        }

        /// <summary>
        /// Создает стек с заданным начальным размером
        /// </summary>
        /// <param name="capacity"></param>
        public ArrayStack(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity can`t be less than zero");

            _stack = new T[capacity];
        }

        /// <summary>
        /// Создает стек и наполняет его элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public ArrayStack(IEnumerable<T> collection) : this()
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
            _current++;

            if (_current == _stack.Length)
            {
                Array.Resize(ref _stack, _stack.Length == 0 ? 5 : _stack.Length * 2);
            }

            _stack[_current] = data;

        }

        /// <summary>
        /// Возвращает верхний элемент стека и удаляет его
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (_current == -1)
                throw new InvalidOperationException("Stack is empty");

            var item = _stack[_current];

            _stack[_current] = default(T);
            _current--;

            return item;
        }

        /// <summary>
        /// Возвращает значение, которое показывает есть ли на вершине стека элемент. Если есть копирует его в переменную с параметром out и удаляет его
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryPop(out T result)
        {
            if (_current >= 0)
            {
                result = _stack[_current];
                _stack[_current] = default(T);
                _current--;

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
            if (_current == -1)
                throw new InvalidOperationException("Stack is empty");

            return _stack[_current];
        }

        /// <summary>
        /// Возвращает значение, которое показывает есть ли на вершине стека элемент. Если есть копирует его в переменную с параметром out
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryPeek(out T result)
        {
            if (_current >= 0)
            {
                result = _stack[_current];
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
            _stack = new T[0];
            _current = -1;
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _current; i >= 0; i--)
            {
                yield return _stack[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}