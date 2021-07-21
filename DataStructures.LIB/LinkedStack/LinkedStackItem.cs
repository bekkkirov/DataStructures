using System;

namespace DataStructures.LIB.LinkedStack
{
    /// <summary>
    /// Элемент связного стека
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LinkedStackItem<T>
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
        /// Указатель на следующий элемент стека
        /// </summary>
        public LinkedStackItem<T> Next { get; set; }

        #endregion

        #region Constructors

        public LinkedStackItem(T data)
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