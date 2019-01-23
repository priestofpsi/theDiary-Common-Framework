using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace System.Xml.Linq
{
    /// <summary>
    /// Contains Linq Xml extension methods.
    /// </summary>
    public static class XmlExtensions
    {
        #region XElement Methods & Functions
        /// <summary>
        /// Determines if the specified <paramref name="element"/> contains a child element with the specified <paramref name="name"/>,
        /// </summary>
        /// <param name="element">The node to search for a matching child element.</param>
        /// <param name="name">The name of the child element to locate.</param>
        /// <returns><c>True</c> if the <paramref name="element"/> contains a matching child element; else <c>False</c>.</returns>
        public static bool HasElement(this XElement element, string name)
        {
            return (element.Element(name) != null);
        }

        /// <summary>
        /// Determines if the specified <paramref name="element"/> contains a child elements with the specified <paramref name="name"/>,
        /// </summary>
        /// <param name="element">The node to search for a matching child element.</param>
        /// <param name="name">The name of the child elements to locate.</param>
        /// <returns><c>True</c> if the <paramref name="element"/> contains matching child elements; else <c>False</c>.</returns>
        public static bool HasElements(this XElement element, string name)
        {
            return (element.Elements(name).Count() > 0);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="element"/> to an Enum type.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <returns>An Enum value parsed from the element name.</returns>
        public static TEnum AsEnum<TEnum>(this XElement element)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.Parse<TEnum>(element.Name.LocalName);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="element"/> to an Enum type, if the value can not be
        /// converted then the <paramref name="defaultValue"/> is returned.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="element"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the element name.</returns>
        public static TEnum AsEnum<TEnum>(this XElement element, TEnum defaultValue)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return element.AsEnum<TEnum>(StringComparison.OrdinalIgnoreCase, defaultValue);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="element"/> to an Enum type, if the value can not be
        /// converted then the <paramref name="defaultValue"/> is returned.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the element name should be parsed.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="element"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the element name.</returns>
        public static TEnum AsEnum<TEnum>(this XElement element, StringComparison comparisonType, TEnum defaultValue)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.Parse<TEnum>(element.Name.LocalName, comparisonType, defaultValue);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="element"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="element"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XElement element, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.TryParse<TEnum>(element.Name.LocalName, out result);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="element"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the element name should be parsed.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="element"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XElement element, StringComparison comparisonType, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.TryParse<TEnum>(element.Name.LocalName, comparisonType, out result);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="element"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="element"/>.</param>
        /// <returns>An Enum value parsed from the element name.</returns>
        public static TEnum AsEnum<TEnum>(this XElement element, string prefix)
            where TEnum : struct
        {
            return element.AsEnum<TEnum>(prefix, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="element"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="element"/>.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="element"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the element name.</returns>
        public static TEnum AsEnum<TEnum>(this XElement element, string prefix, TEnum defaultValue)
            where TEnum : struct
        {
            return element.AsEnum<TEnum>(prefix, StringComparison.OrdinalIgnoreCase, defaultValue);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="element"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="element"/>.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the element name should be parsed.</param>
        /// <returns>An Enum value parsed from the element name.</returns>
        public static TEnum AsEnum<TEnum>(this XElement element, string prefix, StringComparison comparisonType)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.Parse<TEnum>(element.Name.LocalName.Replace(prefix, string.Empty, comparisonType), comparisonType);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="element"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="element"/>.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the element name should be parsed.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="element"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the element name.</returns>
        public static TEnum AsEnum<TEnum>(this XElement element, string prefix, StringComparison comparisonType, TEnum defaultValue)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.Parse<TEnum>(element.Name.LocalName.Replace(prefix, string.Empty, comparisonType), comparisonType, defaultValue);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="element"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="element"/>.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="element"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XElement element, string prefix, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.TryParse<TEnum>(element.Name.LocalName.Replace(prefix, string.Empty, StringComparison.OrdinalIgnoreCase), out result);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="element"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="element">The element whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="element"/>.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the element name should be parsed.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="element"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XElement element, string prefix, StringComparison comparisonType, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (element == null)
                throw new ArgumentNullException("element");

            return EnumHelper.TryParse<TEnum>(element.Name.LocalName.Replace(prefix, string.Empty, comparisonType), comparisonType, out result);
        }
        #endregion

        #region XAttribute Methods & Functions
        public static bool HasAttribute(this XElement element, string name)
        {
            return element.HasAttribute(name, true);
        }

        public static bool HasAttribute(this XElement element, string name, bool checkForEmptyValue)
        {
            return (element.Attribute(name) != null
                && (!checkForEmptyValue || (checkForEmptyValue && !string.IsNullOrWhiteSpace(element.Attribute(name).Value))));
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="attribute"/> to an Enum type.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <returns>An Enum value parsed from the attribute name.</returns>
        public static TEnum AsEnum<TEnum>(this XAttribute attribute)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.Parse<TEnum>(attribute.Name.LocalName);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="attribute"/> to an Enum type, if the value can not be
        /// converted then the <paramref name="defaultValue"/> is returned.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="attribute"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the attribute name.</returns>
        public static TEnum AsEnum<TEnum>(this XAttribute attribute, TEnum defaultValue)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return attribute.AsEnum<TEnum>(StringComparison.OrdinalIgnoreCase, defaultValue);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="attribute"/> to an Enum type, if the value can not be
        /// converted then the <paramref name="defaultValue"/> is returned.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the attribute name should be parsed.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="attribute"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the attribute name.</returns>
        public static TEnum AsEnum<TEnum>(this XAttribute attribute, StringComparison comparisonType, TEnum defaultValue)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.Parse<TEnum>(attribute.Name.LocalName, comparisonType, defaultValue);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="attribute"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="attribute"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XAttribute attribute, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.TryParse<TEnum>(attribute.Name.LocalName, out result);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="attribute"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the attribute name should be parsed.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="attribute"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XAttribute attribute, StringComparison comparisonType, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.TryParse<TEnum>(attribute.Name.LocalName, comparisonType, out result);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="attribute"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="attribute"/>.</param>
        /// <returns>An Enum value parsed from the attribute name.</returns>
        public static TEnum AsEnum<TEnum>(this XAttribute attribute, string prefix)
            where TEnum : struct
        {
            return attribute.AsEnum<TEnum>(prefix, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="attribute"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="attribute"/>.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="attribute"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the attribute name.</returns>
        public static TEnum AsEnum<TEnum>(this XAttribute attribute, string prefix, TEnum defaultValue)
            where TEnum : struct
        {
            return attribute.AsEnum<TEnum>(prefix, StringComparison.OrdinalIgnoreCase, defaultValue);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="attribute"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="attribute"/>.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the attribute name should be parsed.</param>
        /// <returns>An Enum value parsed from the attribute name.</returns>
        public static TEnum AsEnum<TEnum>(this XAttribute attribute, string prefix, StringComparison comparisonType)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.Parse<TEnum>(attribute.Name.LocalName.Replace(prefix, string.Empty, comparisonType), comparisonType);
        }

        /// <summary>
        /// Converts the name of the specified <paramref name="attribute"/> to an Enum type, removing the specified <paramref name="prefix"/> from the conversion.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="attribute"/>.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the attribute name should be parsed.</param>
        /// <param name="defaultValue">The value to use if the <paramref name="attribute"/> name can not be converted.</param>
        /// <returns>An Enum value parsed from the attribute name.</returns>
        public static TEnum AsEnum<TEnum>(this XAttribute attribute, string prefix, StringComparison comparisonType, TEnum defaultValue)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.Parse<TEnum>(attribute.Name.LocalName.Replace(prefix, string.Empty, comparisonType), comparisonType, defaultValue);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="attribute"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="attribute"/>.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="attribute"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XAttribute attribute, string prefix, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.TryParse<TEnum>(attribute.Name.LocalName.Replace(prefix, string.Empty, StringComparison.OrdinalIgnoreCase), out result);
        }

        /// <summary>
        /// Converts the string representation of the <paramref name="attribute"/> name to an enum to its <typeparam ref="TEnum"/> equivalent.
        /// A return value indicates whether the conversion succeeded.
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> of Enum to convert to.</typeparam>
        /// <param name="attribute">The attribute whose name to convert.</param>
        /// <param name="prefix"><see cref="String"/> instance to remove when converting the name <paramref name="attribute"/>.</param>
        /// <param name="comparisonType"><see cref="StringComparison"/> value indicating how the attribute name should be parsed.</param>
        /// <param name="result">When this method returns, contains the <typeparam ref="TEnum"/> value equivalent contained in name of the <param ref="attribute"/>, 
        /// if the conversion succeeded, or the default if the conversion failed.</param>
        /// <returns><c>True</c> if s was converted successfully; otherwise, <c>False</c>.</returns>
        public static bool TryAsEnum<TEnum>(this XAttribute attribute, string prefix, StringComparison comparisonType, out TEnum result)
            where TEnum : struct
        {
            if (!typeof(TEnum).IsEnum)
                throw new ArgumentException("The type specified is not an Enum type.");

            if (attribute == null)
                throw new ArgumentNullException("attribute");

            return EnumHelper.TryParse<TEnum>(attribute.Name.LocalName.Replace(prefix, string.Empty, comparisonType), comparisonType, out result);
        }
        #endregion
    }
}
