using System.Collections.Generic;

namespace DataStructures.LIB.Trie
{
    public class CaseInsensitiveCharComparer : IEqualityComparer<char>
    {
        public bool Equals(char x, char y)
        {
            return char.ToUpper(x).Equals(char.ToUpper(y));
        }

        public int GetHashCode(char obj)
        {
            return char.ToUpper(obj).GetHashCode();
        }
    }
}