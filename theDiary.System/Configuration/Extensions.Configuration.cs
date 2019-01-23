using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    /// <summary>
    /// Contains a set of extension methods for use with <see cref="System.Configuration.Configuration"/> instances.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Returns the specified <see cref="ConfigurationSection"/> object.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the specified <see cref="ConfigurationSection"/>.</typeparam>
        /// <param name="configuration">The <see cref="Configuration"/> instance.</param>
        /// <param name="sectionName">The path to the section to be returned.</param>
        /// <returns></returns>
        public static T GetSection<T>(this System.Configuration.Configuration configuration, string sectionName)
            where T : ConfigurationSection
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentNullException("sectionName");

            return (T) configuration.GetSection(sectionName);
        }

        /// <summary>
        /// Determines if a <see cref="ConfigurationElementCollection"/> contains a configuration element specified by the parameter <paramref name="key"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection">The collection to search.</param>
        /// <param name="key">The key identifing the <see cref="ConfigurationElement"/> to locate.</param>
        /// <returns><c>True</c> if the <paramref name="collection"/> contains the specified <paramref name="key"/>; otherwise <c>False</c>.</returns>
        public static bool HasKey<T, TKey>(this T collection, TKey key)
            where T : ConfigurationElementCollection
        {
            TKey searchKey;
            foreach (ConfigurationElement configurationElement in collection)
                if (configurationElement.TryGetKey(out searchKey)
                    && object.Equals(searchKey, key))
                    return true;

            return false;
        }

        public static bool TryGetKey<T>(this T element, out object key)
            where T : ConfigurationElement
        {
            key = null;
            if (element == null)
                throw new ArgumentNullException("element");

            var property = typeof(T).GetProperties().Where(a => a.GetAttribute<ConfigurationPropertyAttribute>(true).IsKey).FirstOrDefault();
            if (property == null)
                return false;

            key = property.GetValue(element, null);
            return true;
        }

        public static bool TryGetKey<T, TKey>(this T element, out TKey key)
            where T : ConfigurationElement
        {
            key = default(TKey);
            return element.TryGetKey(out key);
        }

        /// <summary>
        /// Gets the Key for the specified <paramref name="element"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <see cref="ConfigurationElement"/> for <paramref name="key"/> parameter.</typeparam>
        /// <param name="element">The <see cref="ConfigurationElement"/> to containing the key.</param>
        /// <returns>The value of the key.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="element"/> is <c>Null</c>.</exception>
        /// <exception cref="InvalidOperationException">thrown if the <paramref name="element"/> does not contain a key property.</exception>
        public static object GetKey<T>(this T element)
            where T : ConfigurationElement
        {
            if (element == null)
                throw new ArgumentNullException("element");

            var property = typeof(T).GetProperties().Where(a => a.CanRead 
                && a.CanWrite 
                && a.HasAttribute<ConfigurationPropertyAttribute>() 
                && a.GetAttribute<ConfigurationPropertyAttribute>(true).IsKey).FirstOrDefault();
            if (property == null)
                throw new InvalidOperationException("Key not found.");

            return property.GetValue(element, null);
        }

        /// <summary>
        /// Gets the Key for the specified <paramref name="element"/>.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of <see cref="ConfigurationElement"/> for <paramref name="key"/> parameter.</typeparam>
        /// <typeparam name="TKey">The <see cref="Type"/> of the <paramref name="key"/> value.</typeparam>
        /// <param name="element"></param>
        /// <returns>The value of the key.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="element"/> is <c>Null</c>.</exception>
        /// <exception cref="InvalidOperationException">thrown if the <paramref name="element"/> does not contain a key property.</exception>
        public static TKey GetKey<T, TKey>(this T element)
            where T : ConfigurationElement
        {
            return (TKey)element.GetKey<T>();
        }
    }
}
