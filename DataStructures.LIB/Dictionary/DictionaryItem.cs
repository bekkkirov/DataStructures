namespace DataStructures.LIB.Dictionary
{
    public class DictionaryItem<TKey, TValue>
    {
        public TKey Key { get; }

        public TValue Value { get; }

        public DictionaryItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }

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