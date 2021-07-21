using System;

namespace DataStructures.LIB.SinglyLinkedList
{
    /// <summary>
    /// Элемент односвязного списка
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SinglyLinkedListItem<T>
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
            set
            {
                _data = value ?? throw new ArgumentNullException(nameof(value), "Value can`t be null");
            }
        }

        /// <summary>
        /// Указатель на следующий элемент
        /// </summary>
        public SinglyLinkedListItem<T> Next { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создаёт элемент односвязного списка 
        /// </summary>
        /// <param name="data"></param>
        public SinglyLinkedListItem(T data)
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