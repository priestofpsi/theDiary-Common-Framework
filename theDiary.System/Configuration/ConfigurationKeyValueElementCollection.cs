using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    /// <summary>
    /// Represents a configuration element containing a collection of <see cref="ConfigurationKeyValueElement{TKey, TValue}"/> instances.
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> used as the key for the configuration elements.</typeparam>
    /// <typeparam name="TValue">The <see cref="Type"/> used for the value contained by the configuration elements.</typeparam>
    public class ConfigurationKeyValueElementCollection<TKey, TValue>
        : ConfigurationElementCollection<ConfigurationKeyValueElement<TKey, TValue>>
    {
        #region Consturctors
        /// <summary>
        /// Initializes a new instance of a <see cref="ConfigurationKeyValueElementCollection{TKey, TValue}"/> class.
        /// </summary>
        public ConfigurationKeyValueElementCollection()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of a <see cref="ConfigurationKeyValueElementCollection{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer"/> to use.</param>
        public ConfigurationKeyValueElementCollection(IComparer comparer)
            : base(comparer)
        {
        }
        #endregion

        #region Public Property
        /// <summary>
        /// Gets or sets the value of a configuration element contained in the <see cref="ConfigurationKeyValueElementCollection{TKey, TValue}"/>.
        /// </summary>
        /// <param name="key">The value used to identify the configuration element.</param>
        /// <returns>The value of the configuration element.</returns>
        public TValue this[TKey key]
        {
            get
            {
                return ((ConfigurationKeyValueElement<TKey, TValue>) base.BaseGet(key)).Value;
            }
            set
            {
                int indexOf = this.GetIndexOf(key);
                var element = new ConfigurationKeyValueElement<TKey, TValue>(key, value);
                if (indexOf != -1)
                {
                    base.BaseRemoveAt(indexOf);
                    base.BaseAdd(indexOf, element);
                }
                else
                {
                    base.BaseAdd(element);
                }
            }
        }
        #endregion

        #region Protected Methods & Functions
        /// <summary>
        /// Creates a new <see cref="ConfigurationKeyValueElementCollection{TKey, TValue}"/> instance.
        /// </summary>
        /// <returns>An instance of <see cref="ConfigurationKeyValueElementCollection{TKey, TValue}"/>.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ConfigurationKeyValueElement<TKey, TValue>();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>An <see cref="System.Object"/> that acts as the key for the specified <see cref="System.Configuration.ConfigurationElement"/>.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((ConfigurationKeyValueElement<TKey, TValue>) element).Key;
        }
        #endregion

        #region Private Methods & Functions
        private int GetIndexOf(TKey key)
        {
            for (int i = 0; i < this.Count; i++)
                if ((this[i] as ConfigurationKeyValueElement<TKey, TValue>).Key.Equals(key))
                    return i;
            return -1;
        }
        #endregion
    }
}
