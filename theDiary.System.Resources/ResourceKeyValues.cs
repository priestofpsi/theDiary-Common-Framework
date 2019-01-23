using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Resources
{
    public class ResourceKeyValues
        : IResourceKey,
        IEnumerable<CultureInfo>,
        IEnumerable<string>,
        IEnumerable<ResourceValue>
    {
        #region Constructors
        public ResourceKeyValues(string resourceKey)
        {
            this.ResourceKey = resourceKey;
            this.resourceValues = new Dictionary<CultureInfo, ResourceValue>();
        }

        public ResourceKeyValues(string resourceKey, ResourceValue resourceValue)
            : this(resourceKey)
        {
            this.resourceValues.Add(resourceValue.Culture, resourceValue);
        }

        protected internal ResourceKeyValues(SerializationInfo info, StreamingContext context)
        {
            this.ResourceKey = info.GetString("name");
            this.resourceValues = (Dictionary<CultureInfo, ResourceValue>)info.GetValue("data", typeof(Dictionary<CultureInfo, ResourceValue>));
        }
        #endregion

        #region Private Declarations
        private Dictionary<CultureInfo, ResourceValue> resourceValues;
        #endregion
        
        #region Public Read-Only Properties
        /// <summary>
        /// Gets the key identifing the resource.
        /// </summary>
        public string ResourceKey { get; private set; }

        /// <summary>
        /// Gets the Value associated to the <value>ResourceKey</value> for the default Cultrue.
        /// </summary>
        public string DefaultValue
        {
            get
            {
                return this.resourceValues[ResourceController.Instance.DefaultCulture].Value;
            }
        }
        
        public string this[CultureInfo culture]
        {
            get
            {
                if (culture == null)
                    throw new ArgumentNullException("culture");

                return this.resourceValues.Values.FirstOrDefault(a => a.CultureISOCode.Equals(culture.TwoLetterISOLanguageName));
            }
        }

        public string this[string cultureISO]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(cultureISO))
                    throw new ArgumentNullException("cultureISO");

                return this.resourceValues.Values.FirstOrDefault(a => a.CultureISOCode.Equals(cultureISO));
            }
        }

        /// <summary>
        /// Gets a sequence of <see cref="ResourceValue"/> elements for all available cultures for the <see cref="ResourceKeyValues"/> instance.
        /// </summary>
        public IEnumerable<ResourceValue> ResourceValues
        {
            get
            {
                return this.resourceValues.Values.AsEnumerable();
            }
        }

        public IEnumerable<CultureInfo> ResourceCultures
        {
            get
            {
                return this.resourceValues.Keys.AsEnumerable();
            }
        }

        string IResourceValue.Value
        {
            get
            {
                return this.DefaultValue;
            }
        }
        #endregion

        #region Public Methods & Functions
        public void Add(CultureInfo culture, string value)
        {
            this.resourceValues.Add(culture, new ResourceValue(value, culture));
        }

        public void Add(string cultureISO, string value)
        {
            CultureInfo culture = CultureInfo.GetCultureInfo(cultureISO);
            this.resourceValues.Add(culture, new ResourceValue(value, culture));
        }

        public bool Contains(string cultureISO)
        {
            if (string.IsNullOrWhiteSpace(cultureISO))
                throw new ArgumentNullException("cultureISO");

            CultureInfo culture = CultureInfo.GetCultureInfo(cultureISO);
            return this.resourceValues.ContainsKey(culture);
        }

        public bool Contains(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException("culture");

            return this.resourceValues.ContainsKey(culture);
        }

        public void Remove(CultureInfo culture)
        {
            if (culture == null)
                throw new ArgumentNullException("culture");

            if (!this.resourceValues.ContainsKey(culture))
                throw new ArgumentOutOfRangeException("culture");

            this.resourceValues.Remove(culture);
        }

        public void Remove(string cultureISO)
        {
            if (string.IsNullOrWhiteSpace(cultureISO))
                throw new ArgumentNullException("cultureISO");

            CultureInfo culture = CultureInfo.GetCultureInfo(cultureISO);
            this.Remove(cultureISO);
        }

        /// <summary>
        /// Removes all <see cref="ResourceKeyValue"/> elements from this instance.
        /// </summary>
        public void Clear()
        {
            this.resourceValues.Clear();
        }
        

        public IEnumerator<CultureInfo> GetEnumerator()
        {
            return this.resourceValues.Keys.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return this.resourceValues.GetEnumerator();
        }

        IEnumerator<string> IEnumerable<string>.GetEnumerator()
        {
            return this.resourceValues.Values.Select(a => a.Value).GetEnumerator();
        }

        IEnumerator<ResourceValue> IEnumerable<ResourceValue>.GetEnumerator()
        {
            return this.resourceValues.Values.GetEnumerator();
        }
        #endregion
    }
}
