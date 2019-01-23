using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    /// <summary>
    /// Represents a configuration element containing a collection of <see cref="ConfigurationElement"/> instances of <see cref="Type"/> <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ConfigurationElementCollection<T>
        : ConfigurationElementCollection, IEnumerable<T>
        where T: ConfigurationElement, new()
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationElementCollection"/> class.
        /// </summary>
        public ConfigurationElementCollection()
            : base()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationElementCollection"/> class.
        /// </summary>
        /// <param name="comparer">The <see cref="IComparer"/> to use.</param>
        public ConfigurationElementCollection(IComparer comparer)
            : base(comparer)
        {
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Gets the <see cref="ConfigurationElement"/> at the specified <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the <see cref="ConfigurationElement"/>.</param>
        /// <returns>The <see cref="ConfigurationElement"/> instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the <paramref name="index"/> is less than zero or greater than the number of <see cref="ConfigurationElement"/> entities contained in this instance.</exception>
        public T this[int index]
        {
            get
            {
                if (index < 0 || index > this.Count)
                    throw new ArgumentOutOfRangeException("index");

                return (T) base.BaseGet(index);
            }
        }
        #endregion

        /// <summary>
        /// Creates a new <see cref="AuthenticationParameterConfigurationElemement"/>.
        /// </summary>
        /// <returns>An instance of <see cref="AuthenticationParameterConfigurationElemement"/>.</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new T();
        }

        /// <summary>
        /// Gets the element key for a specified configuration element.
        /// </summary>
        /// <param name="element">The <see cref="System.Configuration.ConfigurationElement"/> to return the key for.</param>
        /// <returns>An <see cref="System.Object"/> that acts as the key for the specified <see cref="System.Configuration.ConfigurationElement"/>.</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((T) element).GetKey();
        }

        /// <summary>
        /// Returns an enumerator used to iterate through the <see cref="T:ConfigurationElement"/>.
        /// </summary>
        /// <returns>A <see cref="T:System.Collections.Generic.IEnumerator"/> that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            List<T> items = new List<T>();
            foreach (var item in this)
                items.Add(item as T);

            return items.GetEnumerator();
        }

        /// <summary>
        /// Gets an <see cref="System.Collections.IEnumerator"/> which is used to iterate through the <see cref="AuthenticationParameterConfigurationElemementCollection"/>.
        /// </summary>
        /// <returns>An <see cref="System.Collections.IEnumerator"/>which is used to iterate through the <see cref="AuthenticationParameterConfigurationElemementCollection"/></returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
