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

        private T[] _stack;

        /// <summary>
        /// Индекс верхнего элемента стека
        /// </summary>
        private int _head = -1;

        private bool _isEmpty => Count == 0;

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов стека
        /// </summary>
        public int Count => _head + 1;

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустой стек
        /// </summary>
        public ArrayStack()
        {
            _stack = Array.Empty<T>();
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
            _head++;

            if (_head == _stack.Length)
            {
                Array.Resize(ref _stack, _stack.Length == 0 ? 5 : _stack.Length * 2);
            }

            _stack[_head] = data;

        }

        /// <summary>
        /// Возвращает верхний элемент стека и удаляет его
        /// </summary>
        /// <returns></returns>
        public T Pop()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Stack is empty");

            var item = _stack[_head];
            MoveHead();

            return item;
        }

        /// <summary>
        /// Удаляет верхний элемент стека.
        /// Если операция удалась записывает его значение в переменную с параметром out и возвращает true.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryPop(out T result)
        {
            if (_isEmpty)
            {
                result = default(T);
                return false;
            }

            result = _stack[_head];
            MoveHead();

            return true;
        }

        /// <summary>
        /// Возвращает верхний элемент стека не удаляя его
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Stack is empty");

            return _stack[_head];
        }

        /// <summary>
        /// Если на вершине стека есть элемент, записывает его в переменную с параметром out и возвращает true.
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryPeek(out T result)
        {
            if (_isEmpty)
            {
                result = default(T);
                return false;
            }

            result = _stack[_head];
            return true;
        }

        /// <summary>
        /// Очищает стек
        /// </summary>
        public void Clear()
        {
            _stack = Array.Empty<T>();
            _head = -1;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Передвигает вершину стека на 1 элемент 
        /// </summary>
        private void MoveHead()
        {
            _stack[_head] = default(T);
            _head--;
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _head; i >= 0; i--)
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