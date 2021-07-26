using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.LIB.SinglyLinkedList;

namespace DataStructures.LIB.LinkedQueue
{
    /// <summary>
    /// Очередь на основе односвязного списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedQueue<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// Указатель на начало очереди
        /// </summary>
        private SinglyLinkedListItem<T> _head;

        /// <summary>
        /// Указатель на конец очереди
        /// </summary>
        private SinglyLinkedListItem<T> _tail;

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
        /// Создает пустую очередь
        /// </summary>
        public LinkedQueue()
        {

        }

        /// <summary>
        /// Создает очередь и наполняет её элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public LinkedQueue(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Enqueue(item);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в конец очереди
        /// </summary>
        /// <param name="data"></param>
        public void Enqueue(T data)
        {
            var item = new SinglyLinkedListItem<T>(data);

            if (_isEmpty)
            {
                _head = _tail = item;
            }

            else
            {
                _tail.Next = item;
                _tail = _tail.Next;
            }

            Count++;
        }

        /// <summary>
        /// Возвращает элемент из начала очереди и удаляет его
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Queue is empty");

            var item = _head.Data;
            MoveHead();

            return item;
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

            result = _head.Data;
            MoveHead();

            return true;
        }

        /// <summary>
        /// Возвращает элемент из начала очереди не удаляя его
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Queue is empty");

            return _head.Data;
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

            result = _head.Data;
            return true;
        }

        /// <summary>
        /// Очищает очередь
        /// </summary>
        public void Clear()
        {
            _head = null;
            _tail = null;
            Count = 0;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Перемещает начало очереди на 1 элемент вперед
        /// </summary>
        private void MoveHead()
        {
            _head = _head.Next;
            Count--;
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
            return GetEnumerator();
        }
    }
}