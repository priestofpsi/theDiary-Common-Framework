using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Syncronization
{
    public sealed class SyncronizedEntityCollection<T>
        : List<SyncronizedEntity<T>>,
        IBinarySerializable
    {
        #region Constructors
        public SyncronizedEntityCollection()
        {
        }

        internal SyncronizedEntityCollection(SerializationInfo info, StreamingContext context)
            : this()
        {
            this.AddRange(info.GetValue<SyncronizedEntity<T>[]>("items"));
        }
        #endregion

        #region Public Properties
        public T this[Guid syncronizationId]
        {
            get
            {
                return this.FirstOrDefault(entity => entity.SyncronizationId.Equals(syncronizationId));
            }
        }

        public SyncronizedEntity<T> this[T entity]
        {
            get
            {
                return this.FirstOrDefault(syncEntity => syncEntity.Entity.GetHashCode().Equals(entity.GetHashCode()));
            }
        }
        #endregion

        #region Public Methods & Functions
        public bool ContainsEntity(T entity)
        {
            return this.GetSyncronizedEntity(entity) != null;
        }

        public SyncronizedEntity<T> GetSyncronizedEntity(T entity)
        {
            return this.FirstOrDefault(syncEntity => syncEntity.Entity.GetHashCode().Equals(entity.GetHashCode()));
        }

        public bool TryGetSyncronizedEntity(T entity, out SyncronizedEntity<T> syncronizedEntity)
        {
            syncronizedEntity = this.FirstOrDefault(syncEntity => syncEntity.Entity.GetHashCode().Equals(entity.GetHashCode()));
            return syncronizedEntity != null;
        }

        public void Add(T entity)
        {
            SyncronizedEntity<T> syncronizedEntity;
            if (!this.TryGetSyncronizedEntity(entity, out syncronizedEntity))
                this.Add(SyncronizedEntity<T>.Create(entity));
        }
        #endregion

        #region ISerializable Implementation
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue<SyncronizedEntity<T>[]>("items", this.ToArray());
        }
        #endregion
    }
}
