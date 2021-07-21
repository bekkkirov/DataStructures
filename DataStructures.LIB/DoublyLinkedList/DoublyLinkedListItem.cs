using System;

namespace DataStructures.LIB.DoublyLinkedList
{
    /// <summary>
    /// Элемент двусвязного списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DoublyLinkedListItem<T>
    {
        #region Fields

        private T _data;

        #endregion

        #region Properties

        /// <summary>
        /// Данные, которые хранит элемент
        /// </summary>
        public T Data
        {
            get => _data;
            set => _data = value ?? throw new ArgumentNullException(nameof(value), "Value can`t be null");
        }

        /// <summary>
        /// Указатель на следующий элемент
        /// </summary>
        public DoublyLinkedListItem<T> Next { get; set; }

        /// <summary>
        /// Указатель на предыдущий элемент
        /// </summary>
        public DoublyLinkedListItem<T> Previous { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает элемент двусвязного списка
        /// </summary>
        /// <param name="data"></param>
        public DoublyLinkedListItem(T data)
        {
            Data = data;
        }

        #endregion

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}