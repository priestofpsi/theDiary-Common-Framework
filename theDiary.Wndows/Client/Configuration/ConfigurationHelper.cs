using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Seth.Tools.Development.HostFileManager
{
    internal static class ConfigurationHelper
    {
        private static readonly string RootRegistryKey = "Software\\Iterative\\HostFileManager";                

        internal static bool ReadConfiguration(string configurationName)
        {
            bool returnValue = false;
            bool.TryParse(ConfigurationHelper.ReadStringConfiguration(configurationName), out returnValue);

            return returnValue;
        }

        internal static string ReadStringConfiguration(string configurationName, string defaultValue = "")
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(ConfigurationHelper.RootRegistryKey, false);
            if (key == null)
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ConfigurationHelper.RootRegistryKey);

            return (string)key.GetValue(configurationName, defaultValue);
        }

        internal static T ReadConfiguration<T>(string configurationName, T defaultValue = default(T))
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(ConfigurationHelper.RootRegistryKey, false);
            if (key == null)
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ConfigurationHelper.RootRegistryKey);

            return (T)key.GetValue(configurationName, defaultValue);
        }

        internal static TOut ReadConfiguration<TOut, TIn>(string configurationName, TIn defaultValue = default(TIn))
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(ConfigurationHelper.RootRegistryKey, false);
            if (key == null)
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ConfigurationHelper.RootRegistryKey);

            return (TOut)key.GetValue(configurationName, defaultValue);
        }

        internal static void SaveConfiguration<T>(string configurationName, T value, bool setConfigurationInstance = true)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(ConfigurationHelper.RootRegistryKey, true);
            if (key == null)
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ConfigurationHelper.RootRegistryKey);

            key.SetValue(configurationName, value);
            key.Close();
            if (setConfigurationInstance)
            {
                object propertyValue = value;
                PropertyInfo property = typeof(Configuration).GetProperty(configurationName);
                if (typeof(T) == typeof(string) 
                    && property.PropertyType.IsEnum)
                    propertyValue = Enum.Parse(property.PropertyType, value as string);

                property.SetValue(Configuration.Instance, propertyValue, null);
            }
        }

        internal static void SaveConfiguration(string configurationName, bool value)
        {
            ConfigurationHelper.SaveConfiguration(configurationName, value.ToString(), false);
            typeof(Configuration).GetProperty(configurationName).SetValue(Configuration.Instance, value, null);
        }

        internal static void SaveConfiguration(string configurationName, string value, bool setConfigurationInstance = true)
        {
            ConfigurationHelper.SaveConfiguration<string>(configurationName, value, setConfigurationInstance);
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(ConfigurationHelper.RootRegistryKey, true);
            if (key == null)
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(ConfigurationHelper.RootRegistryKey);

            key.SetValue(configurationName, value);
            key.Close();
            if (setConfigurationInstance)
            {
                object propertyValue = value;
                PropertyInfo property = typeof(Configuration).GetProperty(configurationName);
                if (property.PropertyType.IsEnum)
                    propertyValue = Enum.Parse(property.PropertyType, value);

                property.SetValue(Configuration.Instance, propertyValue, null);
            }
        }
    }
}
