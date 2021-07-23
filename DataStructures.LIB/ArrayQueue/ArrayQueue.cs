using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LIB.ArrayQueue
{
    /// <summary>
    /// Очередь на основе массива
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ArrayQueue<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// Очередь
        /// </summary>
        private T[] _queue;

        /// <summary>
        /// Индекс начала очереди
        /// </summary>
        private int _head;

        /// <summary>
        /// Индекс конца очереди
        /// </summary>
        private int _tail;

        private bool _isEmpty => Count == 0;

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов очереди
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустую очерель
        /// </summary>
        public ArrayQueue()
        {
            _queue = new T[0];
        }

        /// <summary>
        /// Создает очередь и наполняет её элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public ArrayQueue(IEnumerable<T> collection) : this()
        {
            foreach (var item in collection)
            {
                this.Enqueue(item);
            }
        }

        /// <summary>
        /// Создаёт очередь с заданным начальным размером
        /// </summary>
        /// <param name="capacity"></param>
        public ArrayQueue(int capacity)
        {
            _queue = new T[capacity];
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в конец очереди
        /// </summary>
        /// <param name="data"></param>
        public void Enqueue(T data)
        {
            if (_tail == _queue.Length)
            {
                Array.Resize(ref _queue, _queue.Length == 0 ? 5 : _queue.Length * 2);
            }

            _queue[_tail] = data;

            Count++;
            _tail++;
        }

        /// <summary>
        /// Возвращает элемент из начала очереди и удаляет его
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (_isEmpty)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            var item = _queue[_head];

            MoveHead();
            return item;
        }

        /// <summary>
        /// Возвращает элемент из начала очереди не удаляя его
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (_isEmpty)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return _queue[_head];
        }

        /// <summary>
        /// Возвращает значение, которое показывает есть ли в начале очереди элемент. Если есть копирует его в переменную с параметром out и удаляет его
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryDequeue(out T result)
        {
            if (_isEmpty)
            {
                result = default(T);
                return false;
            }

            result = _queue[_head];

            MoveHead();
            return true;
        }

        /// <summary>
        /// Возвращает значение, которое показывает есть ли в начале очереди элемент. Если есть копирует его в переменную с параметром out
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

            result = _queue[_head];
            return true;
        }

        /// <summary>
        /// Очищает очередь
        /// </summary>
        public void Clear()
        {
            _queue = Array.Empty<T>();

            _head = 0;
            _tail = 0;

            Count = 0;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Перемещает начало очереди на 1 элемент вперед
        /// </summary>
        private void MoveHead()
        {
            _queue[_head] = default(T);
            _head++;
            Count--;
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = _head; i < _tail; i++)
            {
                yield return _queue[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}