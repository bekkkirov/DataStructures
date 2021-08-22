namespace DataStructures.LIB.Dictionary
{
    /// <summary>
    /// Элемент словаря
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class DictionaryItem<TKey, TValue>
    {
        #region Properties

        /// <summary>
        /// Ключ
        /// </summary>
        public TKey Key { get; }

        /// <summary>
        /// Значение
        /// </summary>
        public TValue Value { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Создает элемент словаря
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public DictionaryItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

        #endregion

        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }

        public override string ToString()
        {
            return $"[{Key}, {Value}]";
        }
    }
}