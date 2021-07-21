using System;
using System.Collections;
using System.Collections.Generic;

namespace DataStructures.LIB.SinglyLinkedList
{
    /// <summary>
    /// Односвязный список
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SinglyLinkedList<T> : IEnumerable<T>
    {
        #region Properties

        /// <summary>
        /// Указатель на первый элемент списка
        /// </summary>
        public SinglyLinkedListItem<T> Head { get; private set; }

        /// <summary>
        /// Указатель на последний элемент списка
        /// </summary>
        public SinglyLinkedListItem<T> Tail { get; private set; }

        /// <summary>
        /// Количество элементов списка
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт пустой список
        /// </summary>
        public SinglyLinkedList()
        {
            
        }

        /// <summary>
        /// Создает список и наполняет его элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public SinglyLinkedList(IEnumerable<T> collection)
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
            var item = new SinglyLinkedListItem<T>(data);

            if (Head == null)
            {
                Head = Tail = item;
            }

            else
            {
                Tail.Next = item;
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
            var item = new SinglyLinkedListItem<T>(data);

            if (Head == null)
            {
                Head = Tail = item;
            }

            else
            {
                item.Next = Head;
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

            if (Count == 1)
            {
                Clear();
            }

            else
            {
                Head = Head.Next;
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
            {
                Clear();
            }

            else
            {
                var item = Head;

                while (item.Next != Tail)
                {
                    item = item.Next;
                }

                Tail = item;
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

            //Если значение первого элемента совпадает с искомым
            if (Head.Data.Equals(data))
            {
                RemoveFirst();

                return true;
            }

            var current = Head.Next;
            var previous = Head;

            if (current == null)
                return false;

            //Перебирает список и ищет заданное значение
            while (current.Next != null)
            {
                if (current.Data.Equals(data))
                {
                    previous.Next = current.Next;
                    Count--;

                    return true;
                }

                current = current.Next;
                previous = previous.Next;
            }

            //Если искомый элемент последний в списке
            if (current.Data.Equals(data))
            {
                Tail = previous;
                Tail.Next = null;
                Count--;

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
