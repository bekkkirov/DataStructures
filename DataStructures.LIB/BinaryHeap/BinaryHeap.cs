using System;
using System.Collections.Generic;

namespace DataStructures.LIB.BinaryHeap
{
    /// <summary>
    /// Бинарная куча
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinaryHeap<T> where T: IComparable, IComparable<T>
    {

        #region Fields

        /// <summary>
        /// Список элементов кучи
        /// </summary>
        private List<T> _items = new List<T>();  /* Для доступа к элементу используются такие формулы, где i - индекс элемента в списке
                                                 * Родительский элемент: (i - 1) / 2
                                                 * Левый потомок: 2i + 1 
                                                 * Правый потомок: 2i + 2
                                                 */

        #endregion

        #region Properties

        /// <summary>
        /// Количество элементов кучи
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Максимальный элемент кучи
        /// </summary>
        public T Max => Count > 0 ? _items[0] : default(T);

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустую бинарную кучу
        /// </summary>
        public BinaryHeap()
        {

        }

        /// <summary>
        /// Создает бинарную кучу и наполняет её элементами заданной коллекции
        /// </summary>
        /// <param name="collection"></param>
        public BinaryHeap(IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                this.Add(item);
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в бинарную кучу
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            _items.Add(data);

            var currentIndex = Count - 1;
            var parentIndex = (Count - 2) / 2;

            while (currentIndex > 0 && _items[currentIndex].CompareTo(_items[parentIndex]) > 0)
            {
                Swap(currentIndex, parentIndex);

                currentIndex = parentIndex;
                parentIndex = (currentIndex - 1) / 2;
            }
        }

        /// <summary>
        /// Возвращает максимальный элемент бинарной кучи и удаляет его
        /// </summary>
        /// <returns></returns>
        public T RemoveMax()
        {
            if (Count == 0)
                throw new InvalidOperationException("Binary heap is empty");

            var result = _items[0];

            _items[0] = _items[Count - 1];
            _items.RemoveAt(Count - 1);

            Sort();

            return result;
        }

        /// <summary>
        /// Сортировка кучей 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> HeapSort()
        {
            var count = Count;

            for (int i = 0; i < count; i++)
            {
                yield return RemoveMax();
            }
        }

        /// <summary>
        /// Очищает бинарную кучу
        /// </summary>
        public void Clear()
        {
            _items = new List<T>();
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Сортирует бинарную кучу после удаления элемента
        /// </summary>
        private void Sort()
        {
            int currentIndex = 0;
            int maxIndex = currentIndex;
            int leftChildIndex;
            int rightChildIndex;

            while (currentIndex < Count)
            {
                leftChildIndex = 2 * currentIndex + 1;
                rightChildIndex = 2 * currentIndex + 2;


                if (leftChildIndex < Count &&
                    _items[leftChildIndex].CompareTo(_items[currentIndex]) > 0)
                {
                    maxIndex = leftChildIndex;
                }

                if (rightChildIndex < Count &&
                    _items[rightChildIndex].CompareTo(_items[currentIndex]) > 0)
                {
                    maxIndex = rightChildIndex;
                }

                if (maxIndex == currentIndex)
                    return;

                Swap(currentIndex, maxIndex);
                currentIndex = maxIndex;
            }
        }

        /// <summary>
        /// Обменивает местами два элемента кучи с заданными индексами
        /// </summary>
        /// <param name="firstIndex"></param>
        /// <param name="secondIndex"></param>
        private void Swap(int firstIndex, int secondIndex)
        {
            var temp = _items[firstIndex];
            _items[firstIndex] = _items[secondIndex];
            _items[secondIndex] = temp;
        }

        #endregion

    }
}