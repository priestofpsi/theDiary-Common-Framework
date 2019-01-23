using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds the specified item to the dictionary.
        /// </summary>
        /// <typeparam name="TKey">The key of the element to add.</typeparam>
        /// <typeparam name="TValue">The value of the element to add. The value can be null for reference types.</typeparam>
        /// <param name="dictionary">The Dictionary to add the item too.</param>
        /// <param name="item">The <see cref="T:KeyValuePair"/> item to be added.</param>
        public static void Add<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> item)
        {
            if (dictionary.IsNull())
                throw new ArgumentNullException("dictionary");

            dictionary.Add(item.Key, item.Value);
        }
    }
}
