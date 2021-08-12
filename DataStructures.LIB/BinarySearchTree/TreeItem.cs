using System;

namespace DataStructures.LIB.BinarySearchTree
{
    /// <summary>
    /// Узел бинарного дерева
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TreeItem<T> where T: IComparable<T>
    {
        #region Fields

        private T _data;

        #endregion

        #region Properties

        public T Data
        {
            get => _data;

            set => _data = value ?? throw new ArgumentNullException(nameof(value), "Value can`t be null");
        }

        public TreeItem<T> Left { get; set; }

        public TreeItem<T> Right { get; set; }

        #endregion

        #region Constructors

        public TreeItem(T data)
        {
            Data = data;
        }

        #endregion

        public bool Add(T data)
        {
            if(data.CompareTo(Data) == 0)
                return false;

            if (data.CompareTo(Data) < 0)
            {
                if (Left == null)
                {
                    Left = new TreeItem<T>(data);

                    return true;
                }

                return Left.Add(data);
            }

            else
            {
                if (Right == null)
                {
                    Right = new TreeItem<T>(data);

                    return true;
                }

                return Right.Add(data);
            }
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}