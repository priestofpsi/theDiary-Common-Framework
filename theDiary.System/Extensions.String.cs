using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace System
{
    /// <summary>
    /// Contains extension methods used for <see cref="String"/> manipulation.
    /// </summary>
    public static partial class StringExtensions
    {
        public static bool IsAny(this string value, StringComparison comparisonType, params string[] comparisonValues)
        {
            return comparisonValues.Any(val => value.Equals(val, comparisonType));
        }

    #region IsNull Methods & Functions
        /// <summary>
        /// Determines if the <paramref name="value"/> is either a <c>Null</c> instance, or is an <c>Empty</c> <see cref="string"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> to compare.</param>
        /// <returns><c>True</c> if the <see cref="string"/> is <c>Null</c> or <c>Empty</c>.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        /// <summary>
        /// Determines if the <paramref name="value"/> is either a <c>Null</c> instance, or is an <c>Empty</c> or <c>Whitepsace</c> <see cref="string"/>..
        /// </summary>
        /// <param name="value">The <see cref="String"/> to compare.</param>
        /// <returns><c>True</c> if the <see cref="string"/> is <c>Null</c>, <c>Empty</c> or <c>Whitespace</c>.</returns>
        public static bool IsNullEmptyOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
        #endregion

        #region StartsIn and EndsIn Methods & Functions
        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified any of the strings when compared using the specified comparison option.
        /// </summary>
        /// <param name="item">The <see cref="string"/> instance to compare too.</param>
        /// <param name="values">The array of <see cref="string"/> instances to compare</param>
        /// <returns><c>True</c> if the string begins with any of the values; otherwise <c>False</c>.</returns>
        public static bool StartsIn(this string item, params string[] values)
        {
            return item.StartsIn(StringComparison.CurrentCulture, values);
        }

        /// <summary>
        /// Determines whether the beginning of this string instance matches the specified any of the strings when compared using the specified comparison option.
        /// </summary>
        /// <param name="item">The <see cref="string"/> instance to compare too.</param>
        /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
        /// <param name="values">The array of <see cref="string"/> instances to compare</param>
        /// <returns><c>True</c> if the string begins with any of the values; otherwise <c>False</c>.</returns>
        public static bool StartsIn(this string item, StringComparison comparisonType, params string[] values)
        {
            if (values.IsNullOrEmpty())
                return false;

            foreach (string value in values)
                if (item.StartsWith(value, comparisonType))
                    return true;

            return false;
        }

        /// <summary>
        /// Determines whether the ending of this string instance matches the specified any of the strings when compared using the specified comparison option.
        /// </summary>
        /// <param name="item">The <see cref="string"/> instance to compare too.</param>
        /// <param name="values">The array of <see cref="string"/> instances to compare</param>
        /// <returns><c>True</c> if the string begins with any of the values; otherwise <c>False</c>.</returns>
        public static bool EndsIn(this string item, params string[] values)
        {
            return item.EndsIn(StringComparison.CurrentCulture, values);
        }

        /// <summary>
        /// Determines whether the ending of this string instance matches the specified any of the strings when compared using the specified comparison option.
        /// </summary>
        /// <param name="item">The <see cref="string"/> instance to compare too.</param>
        /// <param name="comparisonType">One of the enumeration values that determines how this string and value are compared.</param>
        /// <param name="values">The array of <see cref="string"/> instances to compare</param>
        /// <returns><c>True</c> if the string begins with any of the values; otherwise <c>False</c>.</returns>
        public static bool EndsIn(this string item, StringComparison comparisonType, params string[] values)
        {
            if (values.IsNullOrEmpty())
                return false;

            foreach (string value in values)
                if (item.EndsWith(value, comparisonType))
                    return true;

            return false;
        }
        #endregion

        #region Contains Methods & Functions
        public static bool Contains(this string item, string value, StringComparison comparisonType)
        {
            return (item.IndexOf(value, comparisonType) != -1);
        }
                
        public static bool Contains(this IEnumerable<string> values, string value, StringComparison comparisonType)
        {
            foreach (string compareValue in values)
                if (compareValue.Equals(value, comparisonType))
                    return true;

            return false;
        }

        public static bool ContainsAny(this string item, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
                if (item.Contains(value, comparisonType))
                    return true;

            return false;
        }

        public static bool ContainsAny(this string value, params string[] contains)
        {
            return value.ContainsAny(StringComparison.Ordinal, contains);
        }
        
        public static bool ContainsAny(this string value, out string matchedValue, params string[] contains)
        {
            return value.ContainsAny(out matchedValue, StringComparison.Ordinal, contains);
        }

        public static bool ContainsAny(this string value, out string matchedValue, StringComparison comparisonType, params string[] contains)
        {
            if (contains == null)
                throw new ArgumentNullException("contains");
            if (value == null)
                throw new ArgumentNullException("value");

            foreach (string compareValue in contains)
            {
                if (value.Equals(compareValue, comparisonType))
                {
                    matchedValue = compareValue;
                    return true;
                }
            }

            matchedValue = null;
            return false;
        }
        #endregion

        #region SubString Methods & Functions
        public static string Substring(this string value, string startingString, string endingString)
        {
            return value.Substring(startingString, endingString, false);
        }

        public static string Substring(this string value, string startingString, string endingString, StringComparison comparisonType)
        {
            return value.Substring(startingString, endingString, false, StringComparison.CurrentCulture);
        }

        public static string Substring(this string value, string startingString, string endingString, bool includeStartEndValues)
        {
            return value.Substring(startingString, endingString, includeStartEndValues, StringComparison.CurrentCulture);
        }

        public static string Substring(this string value, string startingString, string endingString, bool includeStartEndValues, StringComparison comparisonType)
        {
            if (value == null)
                throw new ArgumentNullException("value");
            if (startingString == null)
                throw new ArgumentNullException("startingString");
            if (endingString == null)
                throw new ArgumentNullException("endingString");

            int startingDifference = (includeStartEndValues) ? 0 : startingString.Length;
            int endingDifference = (includeStartEndValues) ? 0 : endingString.Length;
            int startIndex = value.IndexOf(startingString, comparisonType);
            int endIndex = value.IndexOf(endingString, comparisonType);
            return value.Substring(startIndex + startingDifference, endIndex - startIndex - endingDifference);
        }
        #endregion

        #region Replace Methods & Functions
        /// <summary>
        /// Returns a new string in which all occurrences of a specified string in the current instance are replaced with another specified string.
        /// </summary>
        /// <param name="instance">The string instance to search.</param>
        /// <param name="oldValue">The string to be replaced.</param>
        /// <param name="newValue">The string to replace all occurrences of oldValue.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>A string that is equivalent to the current string except that all instances of oldValue are replaced with newValue.</returns>
        public static string Replace(this string instance, string oldValue, string newValue, StringComparison comparisonType)
        {
            System.StringBuilder returnValue = new StringBuilder();
            int lastIndex = 0;
            int indexOf = instance.IndexOf(oldValue, lastIndex, comparisonType);
            switch (indexOf)
            {
                case -1:
                    return instance;
                case 0:
                    returnValue.Append(newValue);
                    lastIndex = oldValue.Length - 1;
                    break;
            }
            while (indexOf != -1)
            {
                returnValue.Append("{0}{1}", instance.Substring(lastIndex, indexOf - 1), newValue);
                lastIndex = oldValue.Length - 1;
                indexOf = instance.IndexOf(oldValue, lastIndex, comparisonType);
            }
            if (lastIndex < instance.Length - 1)
                returnValue.Append(instance.Substring(lastIndex));

            return returnValue;
        }
        #endregion

        #region Concat Methods & Functions
        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable"/> collection of type System.String, 
        /// using the specified separator between each member.
        /// </summary>
        /// <param name="values">The sequence of <see cref="String"/> elements to join.</param>
        /// <param name="seperator">The value used to seperate each element</param>
        /// <returns>A string that consists of the members of values delimited by the <paramref name="seperator"/> string. 
        /// If <paramref name="values"/> has no members, the method returns System.String.Empty.</returns>
        public static string Concat(this IEnumerable<string> values, string seperator)
        {
            return String.Join(seperator, values);
        }

        /// <summary>
        /// Concatenates the members of a constructed <see cref="T:System.Collections.Generic.IEnumerable"/> collection of type System.String, 
        /// using the specified separator between each member.
        /// </summary>
        /// <param name="values">The sequence of <see cref="String"/> elements to join.</param>
        /// <param name="seperator">The value used to seperate each element</param>
        /// <returns>A string that consists of the members of values delimited by the <paramref name="seperator"/> string. 
        /// If <paramref name="values"/> has no members, the method returns System.String.Empty.</returns>
        public static string Concat(this IEnumerable<string> values, char seperator)
        {
            return values.Concat(seperator.ToString());
        }
        #endregion

        public static string ToMD5(this string value)
        {
            System.Security.Cryptography.MD5 hash = System.Security.Cryptography.MD5.Create();
            return hash.ComputeHash(Encoding.UTF8.GetBytes(value)).GetHashString();
        }

        public static string GetHash(this string input)
        {
            HashAlgorithm hashAlgorithm = new SHA256CryptoServiceProvider();

            byte[] byteValue = System.Text.Encoding.UTF8.GetBytes(input);

            byte[] byteHash = hashAlgorithm.ComputeHash(byteValue);

            return Convert.ToBase64String(byteHash);
        }

        private static string GetHashString(this byte[] value)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in value)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}
