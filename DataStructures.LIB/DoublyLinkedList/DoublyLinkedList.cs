using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LIB.DoublyLinkedList
{
    public class DoublyLinkedList<T> : IEnumerable<T>
    {

        #region Properties

        /// <summary>
        /// Первый элемент списка
        /// </summary>
        public DoublyLinkedListItem<T> Head { get; private set; }

        /// <summary>
        /// Последний элемент списка
        /// </summary>
        public DoublyLinkedListItem<T> Tail { get; private set; }

        /// <summary>
        /// Количество элементов списка
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт пустой список
        /// </summary>
        public DoublyLinkedList()
        {

        }

        /// <summary>
        /// Создает список и наполняет его элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public DoublyLinkedList(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);   
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в конец списка
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            var item = new DoublyLinkedListItem<T>(data);

            if (Head == null)
            {
                Head = Tail = item;
            }

            else
            {
                Tail.Next = item;
                item.Previous = Tail;

                Tail = Tail.Next;
            }

            Count++;
        }

        /// <summary>
        /// Добавляет элемент в начало списка
        /// </summary>
        /// <param name="data"></param>
        public void AddFirst(T data)
        {
            DoublyLinkedListItem<T> item = new DoublyLinkedListItem<T>(data);

            if (Head == null)
            {
                Head = Tail = item;
            }

            else
            {
                item.Next = Head;

                Head.Previous = item;
                Head = item;

            }

            Count++;
        }

        /// <summary>
        /// Удаляет первый элемент списка
        /// </summary>
        public void RemoveFirst()
        {
            if (Count == 0)
                throw new InvalidOperationException("List is empty");

            if(Count == 1)
                Clear();

            else
            {
                Head = Head.Next;
                Head.Previous = null;

                Count--;
            }
        }

        /// <summary>
        /// Удаляет последний элемент списка
        /// </summary>
        public void RemoveLast()
        {
            if (Count == 0)
                throw new InvalidOperationException("List is empty");

            if (Count == 1)
                Clear();

            else
            {
                Tail = Tail.Previous;
                Tail.Next = null;

                Count--;
            }
        }

        /// <summary>
        /// Удаляет первое вхождение заданного элемента
        /// </summary>
        /// <param name="data"></param>
        public bool Remove(T data)
        {
            if (Head == null) { return false; }

            if (Head.Data.Equals(data))
            {
                RemoveFirst();

                return true;
            }

            var current = Head.Next;

            if (current == null)
                return false;

            while (current.Next != null)
            {
                if (current.Data.Equals(data))
                {
                    current.Previous.Next = current.Next;
                    current.Next.Previous = current.Previous;

                    Count--;

                    return true;
                }

                current = current.Next;
            }

            if (Tail.Data.Equals(data))
            {
                RemoveLast();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Очищает содержимое списка
        /// </summary>
        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        #endregion

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;

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