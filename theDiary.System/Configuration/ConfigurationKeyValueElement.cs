using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    /// <summary>
    /// Represents a configuration key/value element within a configuration file.
    /// </summary>
    /// <typeparam name="TKey">The <see cref="Type"/> used as the key for the configuration element.</typeparam>
    /// <typeparam name="TValue">The <see cref="Type"/> used for the value contained by the configuration element.</typeparam>
    public class ConfigurationKeyValueElement<TKey, TValue>
        : System.Configuration.ConfigurationElement
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationKeyValueElement"/> class.
        /// </summary>
        public ConfigurationKeyValueElement()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationKeyValueElement{TKey, TValue}"/> class with the specified <paramref name="key"/>.
        /// </summary>
        /// <param name="key">A value of <typeparamref name="TKey"/> used to identify the instance.</param>
        public ConfigurationKeyValueElement(TKey key)
            : this()
        {
            if (key.IsNull())
                throw new ArgumentNullException("key");

            this.Key = key;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationKeyValueElement{TKey, TValue}"/> class with the specified <paramref name="key"/> and <paramref name="value"/>.
        /// </summary>
        /// <param name="key">A value of <typeparamref name="TKey"/> used to identify the instance.</param>
        /// <param name="value">A value of <typeparamref name="TValue"/> for the instance.</param>
        public ConfigurationKeyValueElement(TKey key, TValue value)
            : this(key)
        {
            this.Value = value;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the <see cref="T:ConfigurationElement"/> key.
        /// </summary>
        [ConfigurationProperty("Key", IsRequired = true, IsKey=true)]
        public TKey Key
        {
            get
            {
                return (TKey)this["key"];
            }
            set
            {
                if (value.IsNull())
                    throw new ArgumentNullException("value", "The key can not be Null");

                this["key"] = value;
            }
        }
        /// <summary>
        /// Gets or sets the value for this instance.
        /// </summary>

        [ConfigurationProperty("value", IsRequired = false)]
        public TValue Value
        {
            get
            {
                return (TValue)this["value"];
            }
            set
            {
                this["value"] = value;
            }
        }
        #endregion

        /// <summary>
        /// Returns a <see cref="KeyValuePair{TKey, TValue}"/> instance from the configuration element.
        /// </summary>
        /// <param name="element">The <see cref="ConfigurationKeyValueElement{TKey, TValue}"/> instance used to create the <see cref="KeyValuePair{TKey, TValue}"/> instance.</param>
        public static implicit operator KeyValuePair<TKey, TValue>(ConfigurationKeyValueElement<TKey, TValue> element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return new KeyValuePair<TKey, TValue>(element.Key, element.Value);
        }
    }
}
