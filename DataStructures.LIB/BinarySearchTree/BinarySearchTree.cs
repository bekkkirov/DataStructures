using System;
using System.Collections.Generic;

namespace DataStructures.LIB.BinarySearchTree
{
    /// <summary>
    /// Бинарное дерево поиска
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BinarySearchTree<T> where T: IComparable<T>
    {
        #region Properties

        /// <summary>
        /// Корень
        /// </summary>
        public TreeItem<T> Root { get; private set; }

        /// <summary>
        /// Количество элементов дерева
        /// </summary>
        public int Count { get; private set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает пустое бинарное дерево поиска
        /// </summary>
        public BinarySearchTree()
        {

        }

        /// <summary>
        /// Создает бинарное дерево поиска с заданным корнем
        /// </summary>
        /// <param name="root"></param>
        public BinarySearchTree(T root)
        {
            this.Add(root);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавляет элемент в дерево
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            if (Root == null)
            {
                Root = new TreeItem<T>(data);
                Count++;

                return;
            }

            if (Root.Add(data))
            {
                Count++;
            }
        }

        /// <summary>
        /// Удаляет элемент из дерева
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Remove(T data)
        {
            if (Root == null)
            {
                return false;
            }

            var current = Root;
            TreeItem<T> parent = null;

            // Поиск заданного элемента
            while (current != null && current.Data.CompareTo(data) != 0)
            {
                parent = current;

                if (current.Data.CompareTo(data) > 0)
                {
                    current = current.Left;
                }

                else
                {
                    current = current.Right;
                }
            }

            // Если элемент не найден функция возвращает false
            if (current == null)
                return false;

            #region Удаление, если у элемента нет дочерних узлов или есть только 1 дочерний узел

            if (current.Left == null)
            {
                if (parent.Left == current)
                {
                    parent.Left = current.Right;
                }

                else
                {
                    parent.Right = current.Right;
                }

                Count--;
                return true;
            }

            if (current.Right == null)
            {
                if (parent.Left == current)
                {
                    parent.Left = current.Left;
                }

                else
                {
                    parent.Right = current.Left;
                }

                Count--;
                return true;
            }

            #endregion

            #region Удаление, если у элемента есть 2 дочерних узла

            // На место удаляемого элемента ставим минимальный элемент из его правого поддерева
            var min = Min(current.Right);
            Remove(min);
            current.Data = min;

            return true;

            #endregion
        }

        /// <summary>
        /// Находит минимальный элемент дерева
        /// </summary>
        /// <returns></returns>
        public T Min()
        {
            if (Root == null)
                throw new InvalidOperationException("Tree is empty");

            return Min(Root);
        }

        /// <summary>
        /// Находит максимальный элемент дерева
        /// </summary>
        /// <returns></returns>
        public T Max()
        {
            if (Root == null)
                throw new InvalidOperationException("Tree is empty");

            return Max(Root);
        }

        /// <summary>
        /// Находит элемент в дереве. Возвращает null, если элемент не найден
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public TreeItem<T> Search(T data)
        {
            if (Root == null)
                throw new InvalidOperationException("Tree is empty");

            return Search(Root, data);
        }

        /// <summary>
        /// Прямой обход
        /// </summary>
        /// <returns></returns>
        public List<T> PreOrder()
        {
            var list = new List<T>();

            if (Root == null)
            {
                return new List<T>();
            }

            return PreOrder(Root);
        }

        /// <summary>
        /// Обратный обход
        /// </summary>
        /// <returns></returns>
        public List<T> PostOrder()
        {
            var list = new List<T>();

            if (Root == null)
            {
                return new List<T>();
            }

            return PostOrder(Root);
        }

        /// <summary>
        /// Центрированный обход дерева
        /// </summary>
        /// <returns></returns>
        public List<T> InOrder()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            return InOrder(Root);
        }

        /// <summary>
        /// Прямой обход заданного поддерева 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<T> PreOrder(TreeItem<T> item)
        {
            var list = new List<T>();

            list.Add(item.Data);

            if (item.Left != null)
            {
                list.AddRange(PreOrder(item.Left));
            }

            if (item.Right != null)
            {
                list.AddRange(PreOrder(item.Right));
            }

            return list;
        }

        /// <summary>
        /// Обратный обход заданного поддерева
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<T> PostOrder(TreeItem<T> item)
        {
            var list = new List<T>();

            if (item.Left != null)
            {
                list.AddRange(PostOrder(item.Left));
            }

            if (item.Right != null)
            {
                list.AddRange(PostOrder(item.Right));
            }

            list.Add(item.Data);

            return list;
        }

        /// <summary>
        /// Центрированный обход заданного поддерева
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<T> InOrder(TreeItem<T> item)
        {
            var list = new List<T>();

            if (item.Left != null)
            {
                list.AddRange(InOrder(item.Left));
            }

            list.Add(item.Data);

            if (item.Right != null)
            {
                list.AddRange(InOrder(item.Right));
            }

            return list;
        }

        /// <summary>
        /// Находит максимальный элемент заданного поддерева
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T Max(TreeItem<T> item)
        {
            if (item.Right == null)
            {
                return item.Data;
            }

            return Max(item.Right);
        }

        /// <summary>
        /// Находит минимальный элемент заданного поддерева
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public T Min(TreeItem<T> item)
        {
            if (item.Left == null)
            {
                return item.Data;
            }

            return Min(item.Left);
        }

        /// <summary>
        /// Находит элемент в заданном поддереве. Возвращает null, если элемент не найден
        /// </summary>
        /// <param name="item"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public TreeItem<T> Search(TreeItem<T> item, T data)
        {
            if (item == null || item.Data.Equals(data))
                return item;

            if (data.CompareTo(item.Data) < 0)
                return Search(item.Left, data);
            else
                return Search(item.Right, data);
        }

        #endregion
    }
}