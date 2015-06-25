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

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> def)
        {
            TValue val;
            if (dictionary.TryGetValue(key, out val)) return val;

            return def(key);
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue def)
        {
            TValue val;
            if (dictionary.TryGetValue(key, out val)) return val;

            return def;
        }

        public static TValue GetOrSetDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> def)
        {
            TValue val;
            if (!dictionary.TryGetValue(key, out val))
            {
                val = def(key);
                dictionary.Add(key, val);
            }

            return val;
        }

        public static TValue GetOrSetDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue def)
        {
            TValue val;
            if (!dictionary.TryGetValue(key, out val))
            {
                val = def;
                dictionary.Add(key, val);
            }

            return val;
        }
    }
}

