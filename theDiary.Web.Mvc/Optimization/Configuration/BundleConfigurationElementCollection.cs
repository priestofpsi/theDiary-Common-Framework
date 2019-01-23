using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Optimization
{
    public class BundleConfigurationElementCollection
        : ConfigurationElementCollection<BundleConfigurationElement>
    {
        #region Constructors
        public BundleConfigurationElementCollection()
            : base()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or Sets the name identifing the bundle.
        /// </summary>
        public string BundleName
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        public Type BundleType
        {
            get
            {
                Type returnValue = null;
                string typeName = (string) base["Type"];
                if (!typeName.IsNullEmptyOrWhiteSpace())
                    returnValue = Type.GetType(typeName);

                if (returnValue.IsNull())
                    returnValue = Type.GetType("System.Web.Optimization.Bundle");

                return returnValue;
            }
        }

        [ConfigurationProperty("", IsDefaultCollection=true, IsRequired=true)]
        public BundleConfigurationElement Items
        {
            get
            {
                return (BundleConfigurationElement) base[""];
            }
            set
            {
                base[""] = value;
            }
        }
        #endregion
    }
}
