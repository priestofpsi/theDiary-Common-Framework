using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    public class ConfigurationKeyValueElementCollection<TKey, TValue>
        : ConfigurationElementCollection<ConfigurationKeyValueElement<TKey, TValue>>
    {
        #region Consturctors
        public ConfigurationKeyValueElementCollection()
            : base()
        {
        }

        public ConfigurationKeyValueElementCollection(IComparer comparer)
            : base(comparer)
        {
        }
        #endregion
        
        public TValue this[TKey key]
        {
            get
            {
                return ((ConfigurationKeyValueElement<TKey, TValue>) base.BaseGet(key)).Value;
            }
        }
    }
}
