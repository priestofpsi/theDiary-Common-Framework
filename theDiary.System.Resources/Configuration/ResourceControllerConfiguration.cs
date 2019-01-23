using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Resources.Configuration
{

    /// <summary>
    /// Contains the scope of configuration for the <see cref="ResourceControllerConfigurationSection"/>.
    /// </summary>
    public sealed class ResourceControllerConfiguration
        : ConfigurationScope<ResourceControllerConfigurationSection>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceControllerConfiguration"/>.
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        public ResourceControllerConfiguration(System.Configuration.Configuration configuration, string sectionName)
            : base(configuration, sectionName)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceControllerConfiguration"/>.
        /// </summary>
        /// <param name="configurationFile"></param>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        public ResourceControllerConfiguration(FileInfo configurationFile, string sectionName)
            : base(configurationFile,sectionName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceControllerConfiguration"/>.
        /// </summary>
        /// <param name="configurationFile"></param>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        public ResourceControllerConfiguration(string configurationFile, string sectionName)
            : base(configurationFile, sectionName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceControllerConfiguration"/>.
        /// </summary>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        public ResourceControllerConfiguration(string sectionName)
            : base(sectionName)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceControllerConfiguration"/>.
        /// </summary>
        public ResourceControllerConfiguration()
            : base()
        {
        }
        #endregion
    }
}
