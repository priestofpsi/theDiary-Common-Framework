using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Optimization
{
    public class BundleConfigurationSection
        : ConfigurationSection<string, BundleConfigurationSection>
    {
        [ConfigurationCollection(typeof(BundleConfigurationsCollection), AddItemName="Add", ClearItemsName="Clear", RemoveItemName="Remove")]
        public BundleConfigurationsCollection Bundles
        {
            get
            {
                return (BundleConfigurationsCollection) base[""];
            }
        }
    }



    

    
}
