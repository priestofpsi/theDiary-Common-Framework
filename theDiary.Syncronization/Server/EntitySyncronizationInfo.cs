using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Syncronization.Server
{
    public class EntitySyncronizationInfo
        : ISyncronizable,
        IEntityInfo,
        IBinarySerializable
    {
        #region Constructors
        public EntitySyncronizationInfo()
            : base()
        {
        }

        internal EntitySyncronizationInfo(Type entityType)
            : this()
        {
            this.EntityType = entityType;
        }

        internal EntitySyncronizationInfo(SerializationInfo info, StreamingContext context)
            : this()
        {
            this.EntityType = info.GetValue<Type>("EntityType");
            this.entityIdentifierNames = info.GetValue<string[]>("EntityIdentifierNames");
        }
        #endregion

        #region Private Declarations
        private string[] entityIdentifierNames;
        private PropertyInfo[] entityIdentifierProperties;
        #endregion

        #region Public Properties
        public Guid SyncronizationId { get; set; }

        public bool Deleted { get; set; }

        public Type EntityType { get; set; }

        public string[] EntityIdentifierNames
        {
            get
            {
                if (this.entityIdentifierNames == null)
                    this.entityIdentifierNames = ((IEntityInfo)this).GetEntityIdentifierNames();

                return this.entityIdentifierNames;
            }
        }
        #endregion

        #region Internal Properties
        internal PropertyInfo[] EntityIdentifierProperties
        {
            get
            {
                if (this.entityIdentifierProperties.IsNull())
                    this.entityIdentifierProperties = (from prop in this.EntityType.GetProperties()
                                                       where this.EntityIdentifierNames.Contains(prop.Name)
                                                       select prop).ToArray();

                return this.entityIdentifierProperties;
            }
        }
        #endregion

        #region ISyncronizable Implementation
        Guid ISyncronizable.SyncronizationId
        {
            get
            {
                return this.SyncronizationId;
            }
        }

        bool ISyncronizable.Deleted
        {
            get
            {
                return this.Deleted;
            }
        }
        #endregion

        #region IEntityInfo Implementation
        Type IEntityInfo.EntityType
        {
            get
            {
                return this.EntityType;
            }
        }


        string[] IEntityInfo.GetEntityIdentifierNames()
        {
            return (from prop in this.EntityType.GetProperties()
                    where prop.HasAttribute<System.ComponentModel.DataAnnotations.KeyAttribute>()
                    || prop.Name.IsAny(GetIdentifierPropertyNames(this.EntityType).ToArray())
                    select prop.Name).ToArray();
        }

        object[] IEntityInfo.GetEntityIdentifiers(object entity)
        {
            return (from prop in this.EntityIdentifierProperties
                    select prop.GetValue(entity, null)).ToArray();

        }
        #endregion

        #region ISerializable Implementation
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue<Type>("EntityType", this.EntityType);
            info.AddValue<string[]>("EntityIdentifierNames", this.EntityIdentifierNames);
        }
        #endregion

        public static EntitySyncronizationInfo Create(Type entityType)
        {
            return new EntitySyncronizationInfo(entityType);
        }

        public static EntitySyncronizationInfo Create<T>()
        {
            return new EntitySyncronizationInfo(typeof(T));
        }

        #region Private Static Methods & Functions
        private static IEnumerable<string> GetIdentifierPropertyNames(Type type)
        {
            yield return string.Format("{0}Id", type.Name);
            yield return string.Format("{0}Key", type.Name);
            yield return string.Format("Id");
            yield return "UniqueIdentifier";
            yield return "UniqueId";

        }
        #endregion
    }

    public class EntitySyncronizationInfo<T>
        : EntitySyncronizationInfo
    {
        #region Constructors
        public EntitySyncronizationInfo()
            : base(typeof(T))
        {
        }
        #endregion
    }
}
