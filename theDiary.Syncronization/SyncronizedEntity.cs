using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Syncronization
{
    public sealed class SyncronizedEntity<T>
        : IComparable<SyncronizedEntity<T>>,
        IBinarySerializable,
        ISyncronizable<T>
    {
        public SyncronizedEntity()
            : base()
        {
        }

        public SyncronizedEntity(ISyncronizable<T> syncronizedEntity)
            : this()
        {
            this.CopyFromInterface<ISyncronizable<T>>(syncronizedEntity);
        }

        internal SyncronizedEntity(SerializationInfo info, StreamingContext context)
        {
            this.SyncronizationId = info.GetValue<Guid>("SyncronizationId");
            this.LastSyncronized = info.GetValue<DateTime?>("LastSyncronized");
            this.Deleted = info.GetValue<bool>("Deleted");
            this.Entity = info.GetValue<T>("Entity");
        }

        private SyncronizedEntity(T entity)
        {
            this.Entity = entity;
        }       

        public Guid SyncronizationId { get; private set; }

        public DateTime? LastSyncronized { get; private set; }

        public bool Deleted { get; private set; }
        
        public T Entity { get; private set; }

        int IComparable<SyncronizedEntity<T>>.CompareTo(SyncronizedEntity<T> other)
        {
            if (this.SyncronizationId == Guid.Empty || other.LastSyncronized == null)
                return 1;

            if (this.LastSyncronized == null 
                || this.LastSyncronized < other.LastSyncronized
                || other.SyncronizationId == null)
                return -1;

            return this.Entity.GetHashCode().CompareTo(other.Entity.GetHashCode());
        }

        public void MarkSyncronized()
        {
            if (this.SyncronizationId == Guid.Empty)
                this.SyncronizationId = Guid.NewGuid();

            this.LastSyncronized = DateTime.Now;
        }

        public static SyncronizedEntity<T> Create(T entity)
        {
            return new SyncronizedEntity<T>(entity);
        }

        public static implicit operator T(SyncronizedEntity<T> value)
        {
            return value.Entity;
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue<Guid>("SyncronizationId", this.SyncronizationId);
            info.AddValue<DateTime?>("LastSyncronized", this.LastSyncronized);
            info.AddValue<bool>("Deleted", this.Deleted);
            info.AddValue<T>("Entity", this.Entity);
        }
    }
}
