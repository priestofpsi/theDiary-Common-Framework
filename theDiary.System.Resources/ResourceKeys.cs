using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Resources
{
    public sealed class ResourceKeys
        : IEnumerable<string>,
        IEnumerable<CultureInfo>,
        ICollection<ResourceKeyValues>,
        IEnumerable<ResourceKeyValues>
    {
        #region Constructors
        private ResourceKeys(Type resourceOwnerType)
            : base()
        {
            this.ResourceOwnerType = resourceOwnerType;
            this.resourceKeys = new Dictionary<string, ResourceKeyValues>();
        }
        #endregion

        #region Private Declarations
        private Dictionary<string, ResourceKeyValues> resourceKeys;
        #endregion

        #region Public Read-Only Properties
        public Type ResourceOwnerType { get; private set; }

        public int Count
        {
            get
            {
                return this.resourceKeys.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public ResourceKeyValues this[string key]
        {
            get
            {
                if (!this.resourceKeys.ContainsKey(key))
                    throw new ArgumentOutOfRangeException("key");

                return this.resourceKeys[key];
            }
        }

        public string this[string key, CultureInfo culture]
        {
            get
            {
                if (!this.resourceKeys.ContainsKey(key))
                    throw new ArgumentOutOfRangeException("key");

                if (culture == null)
                    throw new ArgumentNullException("culture");

                return this.resourceKeys[key][culture];
            }
        }
        #endregion

        #region Public Methods & Functions
        public void Add(ResourceKeyValues item)
        {
            if (item == null)
                throw new ArgumentNullException("item");

            if (string.IsNullOrWhiteSpace(item.ResourceKey))
                throw new ArgumentNullException("ResourcreKey missing or null.", "item");

            this.resourceKeys.Add(item.ResourceKey, item);
        }
        
        internal void RegisterResourceKey(string resourceKey)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
                throw new ArgumentNullException("resourceKey");

            this.resourceKeys.Add(resourceKey, new ResourceKeyValues(resourceKey));
        }

        public void Add(string resourceKey, string value, CultureInfo culture)
        {
            if (string.IsNullOrWhiteSpace(resourceKey))
                throw new ArgumentNullException("resourceKey");

            if (culture == null)
                throw new ArgumentNullException("culture");

            if (value == null)
                throw new ArgumentNullException("value");
            ResourceValue rv = new ResourceValue(value, culture);
            this.Add(new ResourceKeyValues(resourceKey, rv));
        }

        /// <summary>
        /// Removes all <see cref="ResourceKeyValue"/> elements from this instance.
        /// </summary>
        public void Clear()
        {
            this.resourceKeys.Clear();
        }

        bool ICollection<ResourceKeyValues>.Contains(ResourceKeyValues item)
        {
            return this.resourceKeys.Values.Contains(a => a.Equals(item));
        }

        void ICollection<ResourceKeyValues>.CopyTo(ResourceKeyValues[] array, int arrayIndex)
        {

        }        

        bool ICollection<ResourceKeyValues>.Remove(ResourceKeyValues item)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<ResourceKeyValues> GetEnumerator()
        {
            return this.resourceKeys.Values.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return this.resourceKeys.Values.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            List<string> resourceKeys = new List<string>();
            this.resourceKeys.Values.Select<ResourceKeyValues, string>(rkv => rkv.ResourceKey).ForEachAsParallel(a => resourceKeys.Add(a));
            return resourceKeys.Distinct().GetEnumerator();
        }

        IEnumerator<CultureInfo> IEnumerable<CultureInfo>.GetEnumerator()
        {
            List<CultureInfo> resourceCultures = new List<CultureInfo>();
            this.resourceKeys.Values.Select<ResourceKeyValues, IEnumerable<CultureInfo>>(rkv => rkv.ResourceCultures).ForEachAsParallel(a => resourceCultures.AddRange(a.Distinct()));

            return resourceCultures.Distinct().GetEnumerator();
        }
        #endregion

        #region Public Static Methods & Functions
        public static ResourceKeys Open<T>()
            where T: class
        {
            return ResourceKeys.Open(typeof(T));
        }

        public static ResourceKeys Open(Type owningResourceType)
        {
            if (owningResourceType == null)
                throw new ArgumentNullException("owningResourceType");

            return new ResourceKeys(owningResourceType);
        }
        #endregion
    }

}
