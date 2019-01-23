using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
    public class UniqueList<T>
        : IList<T>
    {
        #region Constructors
        public UniqueList()
            : base()
        {
            this.items = new List<T>();
        }

        public UniqueList(int capacity)
            : base()
        {
            this.items = new List<T>(capacity);
        }

        public UniqueList(IEnumerable<T> items)
            : this()
        {
            items.Distinct().ForEachAsParallel(item => this.Add(item));
        }
        #endregion

        #region Private Declarations
        private readonly List<T> items;
        private readonly object syncObject = new object();
        #endregion

        #region Private Properties
        private List<T> Items
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.items;
                }
            }
        }
        #endregion

        #region Public Properties
        public int Count
        {
            get
            {
                return this.Items.Count;
            }
        }

        bool ICollection<T>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public T this[int index]
        {
            get
            {
                return this.Items[index];
            }
            set
            {
                if (this.Contains(value))
                    throw new ArgumentException("Item already exists in Unique List.");

                this.Items[index] = value;
            }
        }
        #endregion

        #region Public Methods & Functions
        public int IndexOf(T item)
        {
            return this.Items.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            if (this.Contains(item))
                throw new ArgumentException("Item already exists in Unique List.");

            this.Items.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            this.Items.RemoveAt(index);
        }

        public bool TryAdd(T item)
        {
            if (this.Contains(item))
                return false;

            this.Items.Add(item);
            return true;
        }

        public void Add(T item)
        {
            if (this.Contains(item))
                throw new ArgumentException("Item already exists in Unique List.");

            this.Items.Add(item);
        }

        protected void AddRange(IEnumerable<T> items)
        {
            items.AsParallel().ForAll(item => this.Items.Add(item));
        }
        public void Clear()
        {
            this.Items.Clear();
        }

        public bool Contains(T item)
        {
            return this.Items.Contains(item);
        }

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            this.Items.CopyTo(array, arrayIndex);
        }        

        public bool Remove(T item)
        {
            return this.Items.Remove(item);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.Items.GetEnumerator();
        }
        #endregion
    }
}
