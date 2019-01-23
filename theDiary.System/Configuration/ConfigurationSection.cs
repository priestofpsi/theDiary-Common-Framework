using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace System.Configuration
{
    /// <summary>
    /// Represents a section within a configuration file.
    /// </summary>
    public class ConfigurationSection<TKey, TValue>
        : System.Configuration.ConfigurationSection
    {
        /// <summary>
        /// Returns the parameters contaubed the <see cref="AuthenticationSection"/>.
        /// </summary>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ConfigurationKeyValueElementCollection<,>))]
        public ConfigurationKeyValueElementCollection<TKey,TValue> Values
        {
            get
            {
                return (ConfigurationKeyValueElementCollection<TKey, TValue>)this[""];
            }
            set
            {
                this[""] = value;
            }
        }

        public bool HasValuesKey(TKey key)
        {
            return this.Values.HasKey(key);
        }
    }

    /// <summary>
    /// Represents a section within a configuration file.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of <see cref="ConfigurationElement"/> contained in the <see cref="T:ConfigurationSection"/>.</typeparam>
    public abstract class ConfigurationSection<T>
            : System.Configuration.ConfigurationSection
            where T : ConfigurationElementCollection<T>, new()
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationSection"/>.
        /// </summary>
        public ConfigurationSection()
            : base()
        {
        }
        #endregion

        /// <summary>
        /// Returns the parameters contaubed the <see cref="AuthenticationSection"/>.
        /// </summary>
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(ConfigurationElementCollection<>))]
        public ConfigurationElementCollection<T> Values
        {
            get
            {
                return (ConfigurationElementCollection<T>)this[""];
            }
            set
            {
                this[""] = value;
            }
        }
    }
}
