using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// Represents a collection of keys and values as a grouping.
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> of the keys in the <see cref="T:GroupedList"/>.</typeparam>
    /// <typeparam name="TValue">The <see cref="Type"/> of the values in the <see cref="T:GroupedList"/>.</typeparam>
    public class GroupedList<TKey,TValue>
        : IEnumerable<KeyValuePair<TKey,TValue>>
    {
        #region Constructors
        /// <summary>
        /// initializes a new instance of the <see cref="T:DoubleList"/>.
        /// </summary>
        public GroupedList()
            : base()
        {
            this.values = new Dictionary<TKey, List<TValue>>();
        }
        #endregion

        #region Private Declarations
        private Dictionary<TKey, List<TValue>> values;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the total number of items contained in the list.
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;
                this.values.ForEachAsParallel(a => count += a.Value.Count);
                return count;
            }
        }

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="T:GroupedList"/>.
        /// </summary>
        public IEnumerable<TKey> Keys
        {
            get
            {
                return this.values.Keys;
            }
        }

        /// <summary>
        /// Gets a collection containing the values in the <see cref="T:GroupedList"/>.
        /// </summary>
        public IEnumerable<TValue> Values
        {
            get
            {
                List<TValue> returnValue = new List<TValue>();
                this.values.OrderBy(a=>a.Key).ForEachAsParallel(a => returnValue.AddRange(a.Value));

                return returnValue.AsEnumerable();
            }
        }

        /// <summary>
        /// Gets a collection containing the values in the <see cref="T:GroupedList"/> that are associated with the specified <paramref name="key"/>..
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IEnumerable<TValue> this[TKey key]
        {
            get
            {
                if (!this.values.ContainsKey(key))
                    throw new ArgumentOutOfRangeException("key");

                return this.values[key].AsEnumerable();
            }
        }
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Returns the number of items grouped by the parameter <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The key of the element to find</param>
        /// <returns>The numebr of elements associated to the <paramref name="key"/>.</returns>
        public int CountBy(TKey key)
        {
            if (!this.ContainsKey(key))
                return 0;

            return this.values[key].Count;
        }

        /// <summary>
        /// Adds the specified key and value to the <see cref="T:GroupedList"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add. The value can be <c>null</c> for reference types.</param>
        public virtual void Add(TKey key, TValue value)
        {
            if (!this.ContainsKey(key))
                this.values.Add(key, new List<TValue>());

            this.values[key].Add(value);
        }

        /// <summary>
        /// Removes the value with the specified key from the <see cref="T:GroupedList"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>True</c> if the element is successfully found and removed; otherwise, <c>False</c>.<para/>
        /// This method returns <c>false</c> if <paramref name="key"/> is not found in the <see cref="T:GroupedList"/>.</returns>
        public bool Remove(TKey key)
        {
            if (!this.ContainsKey(key))
                return false;

            return this.values.Remove(key);
        }

        /// <summary>
        /// Removes the <paramref name="value"/> with the specified <paramref name="key"/> from the <see cref="T:GroupedList"/>.
        /// </summary>
        /// <param name="key">>The key of the element to remove</param>
        /// <param name="value">>The value of the element associated to the <paramref name="key"/> parameter.</param>
        /// <returns>><c>True</c> if the element is successfully found and removed; otherwise, <c>False</c>.<para/>
        /// This method returns <c>false</c> if <paramref name="key"/> is not found in the <see cref="T:GroupedList"/>.</returns>
        public bool Remove(TKey key, TValue value)
        {
            if (this.ContainsKey(key))
                return this.values[key].Remove(value);

            return false;
        }

        /// <summary>
        /// Remove all the elements that contains the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value"></param>
        public void RemoveValue(TValue value)
        {
            foreach(TKey key in this.values.Keys)
                this.values[key].RemoveAll(a=>a.Equals(value));
        }

        /// <summary>
        ///  Removes all the elements that match the conditions defined by the specified <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The <see cref="T:Predicate"/> delegate that defines the conditions of the elements
        /// to remove.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="predicate"/> is <c>Null</c>.</exception>
        public void RemoveAll(Predicate<TValue> predicate)
        {
        }

        /// <summary>
        /// Returns a value indicating if the <see cref="T:GroupedList"/> contains the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The value to locate</param>
        /// <returns><c>True</c> if the list contains the specified key; otherwise <c>False</c>.</returns>
        public bool ContainsKey(TKey key)
        {
            return this.values.ContainsKey(key);
        }

        /// <summary>
        /// Removes all items from the list.
        /// </summary>
        public void Clear()
        {
            this.values.Clear();
        }

        /// <summary>
        /// Clears all items with the associated <paramref name="key"/>.
        /// </summary>
        /// <param name="key">The value identifing the items to remove.</param>
        public void Clear(TKey key)
        {
            if (!this.values.ContainsKey(key))
                throw new ArgumentOutOfRangeException("key");

            this.values[key].Clear();
        }        

        /// <summary>
        /// Gets an <see cref="T:IEnumerator"/> used to iterate through the <see cref="T:GroupedList"/>.
        /// </summary>
        /// <returns>A <see cref="T:GroupedList"/ .Enumerator structure for the <see cref="T:GroupedList"/>.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (TKey key in this.values.Keys)
                foreach (TValue value in this.values[key])
                    yield return new KeyValuePair<TKey, TValue>(key, value);
        }

        /// <summary>
        /// Gets an <see cref="T:IEnumerator"/> used to iterate through the <see cref="T:GroupedList"/>.
        /// </summary>
        /// <returns>A <see cref="T:GroupedList"/ .Enumerator structure for the <see cref="T:GroupedList"/>.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.values.GetEnumerator();
        }
        #endregion
    }
}
