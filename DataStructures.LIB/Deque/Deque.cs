using System;
using System.Collections;
using System.Collections.Generic;
using DataStructures.LIB.DoublyLinkedList;

namespace DataStructures.LIB.Deque
{
    public class Deque<T> : IEnumerable<T>
    {
        #region Fields

        /// <summary>
        /// Начало дека
        /// </summary>
        private DoublyLinkedListItem<T> _head;

        /// <summary>
        /// Конец дека
        /// </summary>
        private DoublyLinkedListItem<T> _tail;

        private bool _isEmpty => Count == 0;

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов дека
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустой дек
        /// </summary>
        public Deque()
        {

        }

        /// <summary>
        /// Создает дек и наполняет его элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public Deque(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.AddLast(item);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в начало дека
        /// </summary>
        /// <param name="data"></param>
        public void AddFirst(T data)
        {
            var item = new DoublyLinkedListItem<T>(data);

            if (_isEmpty)
            {
                SetHeadAndTail(item);
                return;
            }

            item.Next = _head;
            _head.Previous = item;

            _head = item;

            Count++;
        }

        /// <summary>
        /// Добавляет элемент в конец дека
        /// </summary>
        /// <param name="data"></param>
        public void AddLast(T data)
        {
            var item = new DoublyLinkedListItem<T>(data);

            if (_isEmpty)
            {
                SetHeadAndTail(item);
                return;
            }

            _tail.Next = item;
            item.Previous = _tail;

            _tail = _tail.Next;

            Count++;
        }

        /// <summary>
        /// Возвращает элемент из начала дека и удаляет его
        /// </summary>
        public T RemoveFirst()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Deque is empty");

            var item = _head.Data;

            if (Count == 1)
            {
                Clear();
                return item;
            }

            _head = _head.Next;
            _head.Previous = null;
            Count--;

            return item;
        }

        /// <summary>
        /// Возвращает элемент из конца дека и удаляет его
        /// </summary>
        public T RemoveLast()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Deque is empty");

            var item = _tail.Data;

            if (Count == 1)
            {
                Clear();
                return item;
            }

            _tail = _tail.Previous;
            _tail.Next = null;

            Count--;

            return item;
        }

        /// <summary>
        /// Возвращает элемент из начала дека не удаляя его
        /// </summary>
        public T PeekFirst()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Deque is empty");

            return _head.Data;
        }

        /// <summary>
        /// Возвращает элемент из конца дека не удаляя его
        /// </summary>
        public T PeekLast()
        {
            if (_isEmpty)
                throw new InvalidOperationException("Deque is empty");

            return _tail.Data;
        }

        /// <summary>
        /// Очищает дек
        /// </summary>
        public void Clear()
        {
            _head = _tail = null;
            Count = 0;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Устанавливает значения начала и конца дека
        /// </summary>
        /// <param name="item"></param>
        private void SetHeadAndTail(DoublyLinkedListItem<T> item)
        {
            _head = _tail = item;

            Count++;
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