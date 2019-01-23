using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Text.RegularExpressions
{
    /// <summary>
    /// Represents a <see cref="String"/> pattern used to match Regular Expression.
    /// </summary>
    public struct RegExPattern
    {
        #region Constructors
        /// <summary>
        /// Initializes a new <see cref="RegExPattern"/> structure.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to use.</param>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="pattern"/> is <c>Null</c> or <c>Empty</c>.</exception>
        private RegExPattern(string pattern)
            : this(pattern, System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.Singleline)
        {
        }

        /// <summary>
        /// Initializes a new <see cref="RegExPattern"/> structure.
        /// </summary>
        /// <param name="pattern">The regular expression pattern to use.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        public RegExPattern(string pattern, RegexOptions options)
        {
            if (string.IsNullOrWhiteSpace(pattern))
                throw new ArgumentNullException("pattern");

            this.pattern = pattern;
            this.options = options;
            this.regEx = new Regex(pattern, options);
        }
        #endregion

        #region Private Declarations
        private string pattern;
        private RegexOptions options;
        private System.Text.RegularExpressions.Regex regEx; 
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the pattern used to test a Regular Expression.
        /// </summary>
        public string Pattern
        {
            get
            {
                return this.pattern;
            }
        }

        /// <summary>
        /// Gets a value indicating if the specified <see cref="String"/> matches the <see cref="RegExPattern"/>.
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <returns><c>True</c> if the <see cref="RegExPattern"/> matched; otherwise <c>False</c>.</returns>
        public bool this[string input]
        {
            get
            {
                return this.IsMatch(input, this.options);
            }
        }

        /// <summary>
        /// Gets a value indicating if the specified <see cref="String"/> matches the <see cref="RegExPattern"/>.
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <returns><c>True</c> if the <see cref="RegExPattern"/> matched; otherwise <c>False</c>.</returns>
        public bool this[string input, RegexOptions options]
        {
            get
            {
                return this.IsMatch(input, options);
            }
        }

        /// <summary>
        /// Searches the specified input string for the occurrence of the regular expression specified in the <see cref="RegExPattern"/>.
        /// </summary>
        /// <param name="input">The value to match.</param>
        /// <param name="groupnum">The zero-based index of the collection member to be retrieved.</param>
        /// <returns>The value mached by the <see cref="RegExPattern"/>.</returns>
        public string this[string input, int groupnum]
        {
            get
            {
                if (!this.IsMatch(input))
                    return null;

                return this.Match(input).Groups[groupnum].Value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <param name="groupnum"></param>
        /// <returns></returns>
        public Match this[string input, RegexOptions options, int groupnum]
        {
            get
            {
                if (!this.IsMatch(input, options))
                    return null;

                return this.Match(input, options);
            }
        }
        #endregion

        #region Private Constant Declarations
        private static readonly RegExPattern emailAddress = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
        private static readonly RegExPattern ipv4Address = @"^([01]?\d\d?|2[0-4]\d|25[0-5])\.(\*|[01]?\d\d?|2[0-4]\d|25[0-5])\.(\*|[01]?\d\d?|2[0-4]\d|25[0-5])\.(\*|[01]?\d\d?|2[0-4]\d|25[0-5])$";
        #endregion

        #region Public Static Read-Only Properties
        /// <summary>
        /// Gets the <see cref="RegExPattern"/> used to validate an RFC certified email address.
        /// </summary>
        public static RegExPattern EmailAddress
        {
            get
            {
                return RegExPattern.emailAddress;
            }
        }

        /// <summary>
        /// Gets the <see cref="RegExPattern"/> used to validate an IPv4 Address.
        /// </summary>
        public static RegExPattern IPv4Address
        {
            get
            {
                return RegExPattern.ipv4Address;
            }
        }
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Determines if the specified <see cref="String"/> matches the <see cref="RegExPattern"/>.
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <returns><c>True</c> if the <see cref="RegExPattern"/> matched; otherwise <c>False</c>.</returns>
        public bool IsMatch(string input)
        {
            return this.IsMatch(input, this.options);
        }

        /// <summary>
        /// Determines if the specified <see cref="String"/> matches the <see cref="RegExPattern"/>.
        /// </summary>
        /// <param name="input">The regular expression pattern to match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <returns><c>True</c> if the <see cref="RegExPattern"/> matched; otherwise <c>False</c>.</returns>
        public bool IsMatch(string input, RegexOptions options)
        {
            if (input == null)
                return false;

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
            return regEx.IsMatch(input);
        }

        /// <summary>
        /// Searches the <paramref name="input"/> <see cref="string"/> for the first occurrence of a regular expression.
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <returns></returns>
        public Match Match(string input)
        {
            return this.Match(input, this.options);
        }

        /// <summary>
        /// Searches the <paramref name="input"/> <see cref="string"/> for the first occurrence of a regular expression.
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="input"/> is <c>Null</c>.</exception>
        public Match Match(string input, RegexOptions options)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
            return regEx.Match(input);
        }

        /// <summary>
        /// Searches the <paramref name="input"/> <see cref="string"/> for the first occurrence of a regular expression.
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <param name="startAt">The zero-based character position at which to start the search.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="input"/> is <c>Null</c>.</exception>
        public Match Match(string input, int startAt)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx();
            return regEx.Match(input, startAt);
        }

        /// <summary>
        /// Searches the <paramref name="input"/> <see cref="string"/> for the first occurrence of a regular expression, beginning at the specified starting position in the string.
        /// </summary>
        /// <param name="input">The input string to search for a match.</param>
        /// <param name="options">A bitwise combination of the enumeration values that modify the regular expression.</param>
        /// <param name="startAt">The zero-based character position at which to start the search.</param>
        /// <returns>An object that contains information about the match.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="input"/> is <c>Null</c>.</exception>
        public Match Match(string input, RegexOptions options, int startAt)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
            return regEx.Match(input, startAt);
        }

        public bool TryMatch(string input, out Match result)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
            result = regEx.Match(input);
            return result.Success;
        }

        public bool TryMatch(string input, Match previousMatch, out Match result)
        {
            if (input == null && previousMatch == null)
                throw new InvalidOperationException("Both input and previousMatch can not be null.");

            if (previousMatch == null)
            {
                System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
                result = regEx.Match(input);
            }
            else
            {
                result = previousMatch.NextMatch();
            }
            return result.Success;
        }

        public bool TryMatch(string input, RegexOptions options, out Match result)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
            result = regEx.Match(input);
            return result.Success;
        }

        public bool TryMatch(string input, int startAt, out Match result)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
            result = regEx.Match(input);
            return result.Success;
        }

        public bool TryMatch(string input, RegexOptions options, int startAt, out Match result)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            System.Text.RegularExpressions.Regex regEx = this.GetRegEx(options);
            result = regEx.Match(input);
            return result.Success;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            
            Type objType = obj.GetType();
            if (objType == typeof(RegExPattern))
            
            {
                return this.Pattern.Equals(((RegExPattern) obj).Pattern);
            }
            else if (objType == typeof(string))
            {
                return this.Pattern.Equals((string)obj);
            }
            return false;
        }

        /// <summary>
        /// Returns the hash code for this <see cref="RegExPattern"/>.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.Pattern.GetHashCode();
        }

        /// <summary>
        /// Returns the <see cref="String"/> representation of the <see cref="RegExPattern"/>.
        /// </summary>
        /// <returns>The <see cref="String"/> representation of this instance.</returns>
        public override string ToString()
        {
            return this.Pattern;
        }
        #endregion

        #region Private Methods & Functions
        private Regex GetRegEx()
        {
            return this.regEx;
        }

        private Regex GetRegEx(RegexOptions options)
        {
            if (this.options == options)
                return this.GetRegEx();

            return new Regex(this.pattern, options);
        }
        #endregion

        #region Public Operators
        public static implicit operator string(RegExPattern instance)
        {
            return instance.Pattern;
        }

        public static implicit operator RegExPattern(string pattern)
        {
            return new RegExPattern(pattern);
        }

        public static explicit operator Regex(RegExPattern instance)
        {
            return new Regex(instance.Pattern);
        }

        public static bool operator ==(RegExPattern instance, string input)
        {
            return instance.IsMatch(input);
        }

        public static bool operator !=(RegExPattern instance, string input)
        {
            return !instance.IsMatch(input);
        }
        #endregion
    }
}
