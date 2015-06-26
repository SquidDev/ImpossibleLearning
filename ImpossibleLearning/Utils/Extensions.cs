using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        
        public static IEnumerable<TResult> WithIndex<TValue, TResult>(this IEnumerable<TValue> enumerable, Func<int, TValue, TResult> selector)
        {
        	int index = 0;
        	foreach(TValue item in enumerable)
        	{
        		yield return selector(index, item);
        		index++;
        	}
        }
        
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.RandomElement(new Random());
		}
		
		public static T RandomElement<T>(this IEnumerable<T> enumerable, Random rand)
		{
			int index = rand.Next(0, enumerable.Count());
			return enumerable.ElementAt(index);
		}
		
		public static string Format<T>(this IEnumerable<T> items)
		{ 
			StringBuilder builder = new StringBuilder().Append('[');
			bool first = true;
			
			foreach(var item in items) 
			{
				if(!first) builder.Append(',');
				first = false;
				
				builder.Append('\n').Append(item.ToString());
			}
		
			return builder.Append(']').ToString();
		}
    }
}

