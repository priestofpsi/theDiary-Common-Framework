using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Resources.Configuration
{
    public class ResourceControllerConfigurationSection
        : ConfigurationSection<string, dynamic>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceControllerConfigurationSection"/> class.
        /// </summary>
        public ResourceControllerConfigurationSection()
            : base()
        {
            
        }
        #endregion

        #region Public Read-Only Properties
        [ConfigurationProperty("DefaultCultureCode", IsRequired=false, DefaultValue="en")]
        public string DefaultCultureCode
        {
            get
            {
                return this["DefaultCultureCode"] as string;
            }
        }

        public bool ContainsTranslationEngine
        {
            get
            {
                return this.TranslationEngineType != null;
            }
        }

        public Type TranslationEngineType
        {
            get
            {
                if (!this.HasValuesKey("TranslationEngineType"))
                    return null;

                return Type.GetType(this.Values["TranslationEngineType"]);
            }
        }
        #endregion
    }
}
