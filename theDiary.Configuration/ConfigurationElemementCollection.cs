using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    public abstract class ConfigurationElementCollection<T>
        : ConfigurationElementCollection, IEnumerable<T>
        where T: ConfigurationElement
    {
        #region Constructors
        protected ConfigurationElementCollection()
            : base()
        { 
        }

        protected ConfigurationElementCollection(IComparer comparer)
            : base(comparer)
        {
        }
        #endregion

        #region Public Read-Only Properties
        public T this[int index]
        {
            get
            {
                return (T) base.BaseGet(index);
            }
        }
        #endregion

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
