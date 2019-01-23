using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Optimization
{
    public class BundleConfigurationsCollection
            : ConfigurationElementCollection<BundleConfigurationElementCollection>
    {
        [ConfigurationProperty("", IsDefaultCollection = true, IsRequired = true)]
        public BundleConfigurationElementCollection Bundles
        {
            get
            {
                return (BundleConfigurationElementCollection) base[""];
            }
            set
            {
                base[""] = value;
            }
        }
    }
}
