using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Client.Configuration;

namespace System.Windows.Forms.Client
{
    public static class ConfigurationExtensions
    {
        public static string ReadConfigurationString(this IConfigurationWrapper configurationWrapper, string configurationName)
        {
            return string.Empty;
        }

        public static T ReadConfigurationEnum<T>(this IConfigurationWrapper configurationWrapper, string configurationName, Enum defaultValue)
        {
            return default(T);
        }

        public static bool ReadConfiguration(this IConfigurationWrapper configurationwrapper, string configurationName, bool defaultValue = false)
        {
            return configurationwrapper.ReadConfiguration<bool>(configurationName, defaultValue);
        }
    }
}
