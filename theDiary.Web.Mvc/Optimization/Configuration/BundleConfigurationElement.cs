using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Optimization
{
    public class BundleConfigurationElement
        : ConfigurationElement
    {
        #region Constructors
        public BundleConfigurationElement()
            : base()
        {
        }
        #endregion

        #region Public Properties
        [ConfigurationProperty("", IsRequired = true)]
        public string VirtualPath
        {
            get
            {
                return (string)base[""];
            }
            set
            {
                base[""] = value;
            }
        }
        #endregion
    }
}
