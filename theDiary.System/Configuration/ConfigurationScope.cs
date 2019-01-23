using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    /// <summary>
    /// Provides an abstract class used to quickly load a custom <see cref="ConfigurationSection"/> from a configuration file.
    /// </summary>
    /// <typeparam name="T">The <see cref="Type"/> of <see cref="ConfigurationSection"/>.</typeparam>
    public class ConfigurationScope<T>
        where T : ConfigurationSection
    {
        #region Protected Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationScope"/>.
        /// </summary>
        /// <param name="configuration">The <see cref="System.Configuration"/> instance that represents the configuration file.</param>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="configuration"/> parameter or the <paramref name="sectionName"/> parmeter is <c>Null</c> or <c>Empty</c>.</exception>
        public ConfigurationScope(System.Configuration.Configuration configuration, string sectionName)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            if (string.IsNullOrWhiteSpace("sectionName"))
                throw new ArgumentNullException("sectionName");

            this.configuration = configuration.GetSection<T>(sectionName);
            this.sectionName = sectionName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationScope"/>.
        /// </summary>
        /// <param name="configurationFile">A <see cref="FileInfo"/> instance that points to the configuration file.</param>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="configurationFile"/> parameter or the <paramref name="sectionName"/> parmeter is <c>Null</c> or <c>Empty</c>.</exception>
        /// <exception cref="FileNotFoundException">thrown if the configuration file is not found.</exception>
        public ConfigurationScope(FileInfo configurationFile, string sectionName)
        {
            if (configurationFile == null)
                throw new ArgumentNullException("configurationFile");

            if (!configurationFile.Exists)
                throw new FileNotFoundException("Configuration file not found.", configurationFile.FullName);

            if (string.IsNullOrWhiteSpace("sectionName"))
                throw new ArgumentNullException("sectionName");

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = configurationFile.FullName;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            
            this.configuration = config.GetSection<T>(sectionName);
            this.sectionName = sectionName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationScope"/>.
        /// </summary>
        /// <param name="configurationFile">The file path to the configuration file.</param>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="configurationFile"/> parameter or the <paramref name="sectionName"/> parmeter is <c>Null</c> or <c>Empty</c>.</exception>
        /// <exception cref="FileNotFoundException">thrown if the configuration file is not found.</exception>
        public ConfigurationScope(string configurationFile, string sectionName)
        {
            if (string.IsNullOrWhiteSpace(configurationFile))
                throw new ArgumentNullException("configurationFile");

            if (!System.IO.File.Exists(configurationFile))
                throw new FileNotFoundException("Configuration file not found.", configurationFile);

            if (string.IsNullOrWhiteSpace("sectionName"))
                throw new ArgumentNullException("sectionName");

            ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
            configMap.ExeConfigFilename = configurationFile;
            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);
            
            this.configuration = config.GetSection<T>(sectionName);
            this.sectionName = sectionName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationScope"/>.
        /// </summary>
        /// <param name="sectionName">The name of the <typeparamref name="T"/> <see cref="ConfigurationSection"/> to load.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="sectionName"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public ConfigurationScope(string sectionName)
        {
            if (string.IsNullOrWhiteSpace("sectionName"))
                throw new ArgumentNullException("sectionName");

            this.configuration = (T) System.Configuration.ConfigurationManager.GetSection(sectionName);
            this.sectionName = sectionName;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ConfigurationScope"/>, using the <typeparamref name="T"/> name as the sectionName.
        /// </summary>
        public ConfigurationScope()
            : this(typeof(T).Name)
        {

        }
        #endregion

        #region Private Declarations
        private T configuration;
        private string sectionName;
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Gets the <typeparamref name="T"/> <see cref="ConfigurationSection"/>.
        /// </summary>
        public T Configuration
        {
            get
            {
                return this.configuration;
            }
            protected set
            {
                this.configuration = value;
            }
        }

        /// <summary>
        /// Gets the name of the section where the <see cref="ConfigurationSection"/> was loaded from.
        /// </summary>
        public string SectionName
        {
            get
            {
                return this.sectionName;
            }
            protected set
            {
                this.sectionName = value;
            }
        }
        #endregion
    }
}
