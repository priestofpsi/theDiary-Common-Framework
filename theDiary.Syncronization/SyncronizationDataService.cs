using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Syncronization
{
    public class SyncronizationDataServiceProxy
        : System.Data.Services.Client.DataServiceContext
    {
        public SyncronizationDataServiceProxy(Uri serviceRoot)
            : this(serviceRoot, IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForDomain())
        {

        }

        public SyncronizationDataServiceProxy(Uri serviceRoot, IO.IsolatedStorage.IsolatedStorage storage)
            : base(serviceRoot)
        {
        }

        private IO.IsolatedStorage.IsolatedStorage cacheStorage;
        private readonly object syncObject = new object();

        public IO.IsolatedStorage.IsolatedStorage CacheStorage
        {
            get
            {
                lock (this.syncObject)
                {
                    return this.cacheStorage;
                }
            }
        }

        public bool Online { get; set; }

        public IQueryable<T> ModelEntities<T>()
        {
            return this.ModelEntities<T>(typeof(T).Name);
        }

        public IQueryable<T> ModelEntities<T>(string modelName)
        {
            if (this.Online)
            {
                base.CreateQuery<T>(modelName);
            }

            return null;
        }

        public void CacheQuery<T>(IQueryable<T> query)
        {
            var enumerator = query.AsEnumerable().GetEnumerator();
            T entity;
            while (enumerator.MoveNext())
            {
                entity = enumerator.Current;
            }
            System.IO.IsolatedStorage.IsolatedStorageFile.GetUserStoreForDomain().OpenFile(SyncronizationDataServiceProxy.GetCacheFileName<T>(), System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read);
        }

        private SyncronizedEntityCollection<T> GetOfflineCache<T>()
        {
            return null;
        }

        private static string GetCacheFileName<T>()
        {
            return string.Format("{0}.OfflineData", typeof(T).Name);
        }
    }
}
