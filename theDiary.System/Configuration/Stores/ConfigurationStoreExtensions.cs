using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration.Stores
{
    public static class ConfigurationStoreExtensions
    {
        public static T OpenCreate<T>(this IConfigurationSectionHandler<T> configurationLocation, string relativeLocation)
        {
            if (!configurationLocation.Exists())
                return configurationLocation.Create(relativeLocation);

            return configurationLocation.Open(relativeLocation);
        }

        public static T OpenCreate<T>(this IConfigurationSectionHandler<T> configurationLocation)
        {
            if (!configurationLocation.Exists())
                return configurationLocation.Create();

            return configurationLocation.Open();
        }
    }
}
