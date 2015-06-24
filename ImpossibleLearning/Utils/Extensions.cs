using System;
using System.Collections.Generic;
using System.Linq;

namespace ImpossibleLearning.Utils
{
    public static class Extensions
    {
        public static void RemoveAll<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, Func<KeyValuePair<TKey, TValue>, bool> predicate)
        {
            foreach (KeyValuePair<TKey, TValue> pair in dictionary.Where(predicate).ToList())
            {
                dictionary.Remove(pair.Key);
            }
        }
    }
}

