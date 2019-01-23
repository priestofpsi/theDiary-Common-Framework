using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Syncronization.Server
{
    public sealed class EntitySyncronizationInfoCollection
        : IDictionary<Type, EntitySyncronizationInfo>,
        IEnumerable<EntitySyncronizationInfo>,
        IBinarySerializable
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySyncronizationInfoCollection"/> class.
        /// </summary>
        public EntitySyncronizationInfoCollection()
            : base()
        {
            this.items = new Dictionary<Type, EntitySyncronizationInfo>();
        }

        internal EntitySyncronizationInfoCollection(SerializationInfo info, StreamingContext context)
            : this()
        {
            this.AddRange(info.GetValue<EntitySyncronizationInfo[]>("items"));
        }
        #endregion

        #region Private Declarations
        private readonly object syncObject = new object();
        private Dictionary<Type, EntitySyncronizationInfo> items;
        #endregion

        #region Public Properties

        public int Count
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.items.Count;
                }
            }
        }

        public EntitySyncronizationInfo this[Type key]
        {
            get
            {
                return ((IDictionary<Type, EntitySyncronizationInfo>)this)[key];
            }
        }
        #endregion

        #region Public Methods & Functions
        public void Add(EntitySyncronizationInfo value)
        {
            if (!((IDictionary<Type, EntitySyncronizationInfo>)this).ContainsKey(value.EntityType))
                ((IDictionary<Type, EntitySyncronizationInfo>)this).Add(value.EntityType, value);
        }

        public void AddRange(IEnumerable<EntitySyncronizationInfo> values)
        {
            values.ForEachAsParallel(value => this.Add(value));
        }

        public void Remove(EntitySyncronizationInfo value)
        {
            ((IDictionary<Type, EntitySyncronizationInfo>)this).Remove(value.EntityType);
        }

        public void Remove(Type entityType)
        {
            ((IDictionary<Type, EntitySyncronizationInfo>)this).Remove(entityType);
        }

        public bool Contains(Type entityType)
        {
            return ((IDictionary<Type, EntitySyncronizationInfo>)this).ContainsKey(entityType);
        }

        public void Clear()
        {
            ((IDictionary<Type, EntitySyncronizationInfo>)this).Clear();
        }
        
        public IEnumerator<EntitySyncronizationInfo> GetEnumerator()
        {
            lock (this.syncObject)
            {
                return this.items.Values.GetEnumerator();
            }
        }
        #endregion

        #region ISerializable Implementation
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            lock (this.syncObject)
            {
                info.AddValue<EntitySyncronizationInfo[]>("items", this.items.Values.ToArray());
            }
        }
        #endregion

        #region IDictionary Implementation

        ICollection<Type> IDictionary<Type, EntitySyncronizationInfo>.Keys
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.items.Keys;
                }
            }
        }

        ICollection<EntitySyncronizationInfo> IDictionary<Type, EntitySyncronizationInfo>.Values
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.items.Values;
                }
            }
        }

        EntitySyncronizationInfo IDictionary<Type, EntitySyncronizationInfo>.this[Type key]
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.items[key];
                }
            }
            set
            {
                lock (this.syncObject)
                {
                    this.items[key] = value;
                }
            }
        }

        bool ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>.IsReadOnly
        {
            get
            {
                return false;
            }
        }

        void IDictionary<Type, EntitySyncronizationInfo>.Add(Type key, EntitySyncronizationInfo value)
        {
            lock (this.syncObject)
            {
                this.items.Add(key, value);
            }
        }

        bool IDictionary<Type, EntitySyncronizationInfo>.ContainsKey(Type key)
        {
            lock (this.syncObject)
            {
                return this.items.ContainsKey(key);
            }
        }

        bool IDictionary<Type, EntitySyncronizationInfo>.Remove(Type key)
        {
            lock (this.syncObject)
            {
                return this.items.Remove(key);
            }
        }

        bool IDictionary<Type, EntitySyncronizationInfo>.TryGetValue(Type key, out EntitySyncronizationInfo value)
        {
            lock (this.syncObject)
            {
                return this.items.TryGetValue(key, out value);
            }
        }        

        void ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>.Add(KeyValuePair<Type, EntitySyncronizationInfo> item)
        {
            lock (this.syncObject)
            {
                this.items.Add(item.Key, item.Value);
            }
        }

        void ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>.Clear()
        {
            lock (this.syncObject)
            {
                this.items.Clear();
            }
        }

        bool ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>.Contains(KeyValuePair<Type, EntitySyncronizationInfo> item)
        {
            lock (this.syncObject)
            {
                return ((ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>)this.items).Contains(item);
            }
        }

        void ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>.CopyTo(KeyValuePair<Type, EntitySyncronizationInfo>[] array, int arrayIndex)
        {
            lock (this.syncObject)
            {
                ((ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>)this.items).CopyTo(array, arrayIndex);
            }
        }


        bool ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>.Remove(KeyValuePair<Type, EntitySyncronizationInfo> item)
        {
            lock (this.syncObject)
            {
                return ((ICollection<KeyValuePair<Type, EntitySyncronizationInfo>>)this.items).Remove(item);
            }
        }

        IEnumerator<KeyValuePair<Type, EntitySyncronizationInfo>> IEnumerable<KeyValuePair<Type, EntitySyncronizationInfo>>.GetEnumerator()
        {
            lock (this.syncObject)
            {
                return this.items.GetEnumerator();
            }
        }        
        #endregion

        #region IEnumerable Implementation
        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            lock (this.syncObject)
            {
                return this.items.Values.GetEnumerator();
            }
        }
        #endregion
    }
}
