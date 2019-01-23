using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    /// <summary>
    /// Represents a mutable string of characters.
    /// <remarks>This class is based on the <see cref="System.Text.StringBuilder"/> class, that implements additional
    /// functionality for ease of use.</remarks>
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{ToString()}")]
    public class StringBuilder
        : IEquatable<string>,
        IEquatable<System.StringBuilder>,
        IEquatable<System.Text.StringBuilder>
    {
        #region Public Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="StringBuilder"/> class.
        /// </summary>
        public StringBuilder()
            : base()
        {
            this.result = new System.Text.StringBuilder();
        }

        /// <summary>
        /// /Initializes a new instance of the <see cref="StringBuilder"/> class, using the specified string.
        /// </summary>
        /// <param name="value">The string used to initialize the value of the instance. If value is null, the new <see cref="StringBuilder"/> will contain the empty string (that is,
        ///  it contains System.String.Empty).</param>
        public StringBuilder(string value)
            : base()
        {
            this.result = new System.Text.StringBuilder(value);
        }
        #endregion

        #region Private Declarations
        private volatile System.Text.StringBuilder result;
        private readonly object syncObject = new object();
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the length of the current <see cref="StringBuilder"/> object.
        /// </summary>
        public int Length
        {
            get
            {
                //lock (this.syncObject)
                return this.result.Length;
            }
            set
            {
                //lock (this.syncObject)
                this.result.Length = value;
            }
        }

        /// <summary>
        /// Gets or sets the character at the specified character position in this instance.
        /// </summary>
        /// <param name="index">The position of the character.</param>
        /// <returns>The Unicode character at position index.</returns>
        public char this[int index]
        {
            get
            {
                //lock (this.syncObject)
                return this.result[index];
            }
            set
            {
                //lock (this.syncObject)
                this.result[index] = value;
            }
        }
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Removes all characters from the current <see cref="StringBuilder"/> instance.
        /// </summary>
        public void Clear()
        {
            this.result.Clear();
        }

        /// <summary>
        /// Determines if the <see cref="StringBuilder"/> instance contains the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> to locate.</param>
        /// <returns><c>True</c> if the <see cref="StringBuilder"/> contains the <paramref name="value"/>; otherwise <c>False</c>.</returns>
        public bool Contains(string value)
        {
            return this.Contains(value, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Determines if the <see cref="StringBuilder"/> instance contains the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The <see cref="String"/> to locate.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns><c>True</c> if the <see cref="StringBuilder"/> contains the <paramref name="value"/>; otherwise <c>False</c>.</returns>
        public bool Contains(string value, StringComparison comparisonType)
        {
            return this.result.ToString().IndexOf(value, comparisonType) != -1;
        }

        #region Append Methods & Functions
        /// <summary>
        /// Appends the string representation of a specified object to this instance.
        /// </summary>
        /// <param name="value">The <see cref="object"/> to append.</param>
        public void Append(object value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Boolean"/> value to this instance.
        /// </summary>.
        /// <param name="value">The <see cref="Boolean"/> value to append.</param>
        public void Append(bool value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Byte"/> to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(byte value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Char"/> to this instance.
        /// </summary>
        /// <param name="value">The Unicode character to append.</param>
        public void Append(char value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of the Unicode characters in a specified array to this instance.
        /// </summary>
        /// <param name="value">The array of characters to append.</param>
        public void Append(char[] value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Decimal"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(decimal value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Double"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(double value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Single"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(float value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Int32"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(int value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Int64"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(long value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="SByte"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(sbyte value)
        {
            lock (this.syncObject)
                this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="Int16"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(short value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="UInt32"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(uint value)
        {
            // lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="UInt64"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(ulong value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        /// <summary>
        /// Appends the string representation of a specified <see cref="UInt16"/> number to this instance.
        /// </summary>
        /// <param name="value">The value to append.</param>
        public void Append(ushort value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        public void Append(string value)
        {
            //lock (this.syncObject)
            this.result.Append(value);
        }

        public void Append(Func<bool> condition, string trueValue, string falseValue)
        {
            if (condition.IsNull())
                throw new ArgumentNullException("condition");

            this.Append(condition(), trueValue, falseValue);
        }

        public void Append(bool condition, string trueValue, string falseValue)
        {
            this.Append((condition) ? trueValue : falseValue);
        }
        /// <summary>
        /// Appends the string returned by processing a composite format string, which contains zero or more format items, to this instance. 
        /// Each format item is replaced by the string representation of a corresponding argument in a parameter array.
        /// </summary>
        /// <param name="format">A composite format string (see Remarks).</param>
        /// <param name="args">An array of objects to format.</param>
        public void Append(string format, params object[] args)
        {
            this.result.AppendFormat(format, args);
        }

        public void AppendLine()
        {
            this.result.AppendLine();
        }

        public void AppendLine(string value)
        {
            this.result.AppendLine(value);
        }

        public void AppendLine(string format, params object[] args)
        {
            this.result.AppendFormat(format, args);
            this.result.AppendLine();
        }

        public void AppendFormat(string format, params object[] args)
        {
            this.result.AppendFormat(format, args);
        }
        #endregion

        #region Insert Methods & Functions
        public void Insert(int index, bool value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, byte value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, char value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, char[] value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, decimal value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, double value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, float value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, int value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, long value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, object value)
        {
            this.result.Insert(0, value);
        }
        public void Insert(int index, sbyte value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, short value)
        {
            this.result.Insert(0, value);
        }
        public void Insert(int index, uint value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, ulong value)
        {
            this.result.Insert(0, value);
        }
        public void Insert(int index, ushort value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, string value)
        {
            this.result.Insert(0, value);
        }

        public void Insert(int index, string value, int count)
        {
            this.result.Insert(0, value, count);
        }

        public void Insert(int index, char[] value, int startIndex, int charCount)
        {
            this.result.Insert(0, value, startIndex, charCount);
        }

        #endregion

        #region Remove Methods & Functions
        public void Remove(int startIndex)
        {
            //lock (this.syncObject)
            //{
            if (startIndex < 0 || startIndex > this.result.Length - 1)
                throw new ArgumentOutOfRangeException("startIndex");

            this.result.Remove(startIndex, this.result.Length - startIndex);
            //}
        }

        public void Remove(int startIndex, int length)
        {
            //lock (this.syncObject)
            //{
            if (startIndex < 0 || startIndex > this.result.Length - 1)
                throw new ArgumentOutOfRangeException("startIndex");

            this.result.Remove(startIndex, length);
            //}
        }

        public void Remove(string oldValue)
        {
            //lock (this.syncObject)
            this.result.Replace(oldValue, string.Empty);
        }

        public void Remove(string oldValue, StringComparison comparisonType)
        {
            //lock (this.syncObject)
            //{
            int index = this.result.ToString().IndexOf(oldValue, comparisonType);
            this.result.Remove(index, oldValue.Length);
            //}
        }
        #endregion

        #region Replace Methods & Functions
        public void Replace(char oldChar, char newChar)
        {
            //lock (this.syncObject)
            this.result.Replace(oldChar, newChar);
        }
        public void Replace(string oldValue, string newValue)
        {
            //lock (this.syncObject)
            this.result.Replace(oldValue, newValue);
        }

        public void Replace(string oldValue, string newValue, StringComparison comparisonType)
        {
            //lock (this.syncObject)
            //{
            int index = this.result.ToString().IndexOf(oldValue, comparisonType);
            if (index == -1)
                return;

            this.result.Remove(index, oldValue.Length);
            this.result.Insert(index, newValue);
            //}
        }
        #endregion

        #region Equals Methods & Functions
        /// <summary>
        /// Determines if the <see cref="String"/> representation of a <see cref="StringBuilder"/> is equal to another <see cref="String"/> value.
        /// </summary>
        /// <param name="other">The <see cref="String"/> to compare to this instance.</param>
        /// <returns><c>True</c> if the values are equal; otherwisde <c>False</c>.</returns>
        public bool Equals(string other)
        {
            return this.Equals(other, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Determines if the <see cref="String"/> representation of a <see cref="StringBuilder"/> is equal to another <see cref="String"/> value.
        /// </summary>
        /// <param name="other">The <see cref="String"/> to compare to this instance.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <returns><c>True</c> if the values are equal; otherwisde <c>False</c>.</returns>
        public bool Equals(string other, StringComparison comparisonType)
        {
            return this.ToString().Equals(other, comparisonType);
        }

        /// <summary>
        /// Determines if the <see cref="String"/> representation of a <see cref="StringBuilder"/> is equal to another <see cref="StringBuilder"/> value.
        /// </summary>
        /// <param name="other">The <see cref="StringBuilder"/> to compare to this instance.</param>
        /// <returns><c>True</c> if the values are equal; otherwisde <c>False</c>.</returns>
        public bool Equals(StringBuilder other)
        {
            return this.Equals(other, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Determines if the <see cref="String"/> representation of a <see cref="StringBuilder"/> is equal to another <see cref="StringBuilder"/> value.
        /// </summary>
        /// <param name="other">The <see cref="StringBuilder"/> to compare to this instance.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <returns><c>True</c> if the values are equal; otherwisde <c>False</c>.</returns>
        public bool Equals(StringBuilder other, StringComparison comparisonType)
        {
            if (other == null)
                return false;

            return this.ToString().Equals(other.ToString(), comparisonType);
        }

        /// <summary>
        /// Determines if the <see cref="String"/> representation of a <see cref="System.Text.StringBuilder"/> is equal to another <see cref="System.Text.StringBuilder"/> value.
        /// </summary>
        /// <param name="other">The <see cref="System.Text.StringBuilder"/> to compare to this instance.</param>
        /// <returns><c>True</c> if the values are equal; otherwisde <c>False</c>.</returns>
        public bool Equals(Text.StringBuilder other)
        {
            return this.Equals(other, StringComparison.CurrentCulture);
        }

        /// <summary>
        /// Determines if the <see cref="String"/> representation of a <see cref="System.Text.StringBuilder"/> is equal to another <see cref="System.Text.StringBuilder"/> value.
        /// </summary>
        /// <param name="other">The <see cref="System.Text.StringBuilder"/> to compare to this instance.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies how the strings will be compared.</param>
        /// <returns><c>True</c> if the values are equal; otherwisde <c>False</c>.</returns>
        public bool Equals(Text.StringBuilder other, StringComparison comparisonType)
        {
            if (other == null)
                return false;

            return this.ToString().Equals(other.ToString());
        }
        #endregion

        #region Override Methods & Functions
        /// <summary>
        /// Determines if a <see cref="Object"/> is equal to a <see cref="StringBuilder"/> instance.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare to this instance.</param>
        /// <returns><c>True</c> if the values are equal; otherwisde <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Type objType = obj.GetType();
            if (objType == typeof(System.StringBuilder))
                return this.Equals((System.StringBuilder)obj);
            if (objType == typeof(string))
                return this.Equals((string)obj);
            if (objType == typeof(System.Text.StringBuilder))
                return this.Equals((System.Text.StringBuilder)obj);

            return Object.Equals(this, obj);
        }

        /// <summary>
        /// Returns the <see cref="String"/> representation of the <see cref="StringBuilder"/>.
        /// </summary>
        /// <returns>The <see cref="String"/> representation of this instance.</returns>
        public override string ToString()
        {
            //lock (this.syncObject)
            return this.result.ToString();
        }

        /// <summary>
        /// Converts the value of a substring of this instance to a <see cref="String"/>.
        /// </summary>
        /// <param name="startIndex">The starting position of the substring in this instance.</param>
        /// <returns>A <see cref="String"/> whose value is the same as the specified substring of this instance.</returns>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the <paramref name="startIndex"/> is less than zero or is greater than the length of the current instance.</exception>
        public string ToString(int startIndex)
        {
            if (startIndex < 0 || startIndex > this.Length - 1)
                throw new ArgumentOutOfRangeException("startIndex");
            //lock (this.syncObject)
            return this.result.ToString().Substring(startIndex);
        }

        /// <summary>
        /// Converts the value of a substring of this instance to a <see cref="String"/>.
        /// </summary>
        /// <param name="startIndex">The starting position of the substring in this instance.</param>
        /// <param name="length">The length of the substring.</param>
        /// <returns>A <see cref="String"/> whose value is the same as the specified substring of this instance.</returns>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="startIndex"/> or <paramref name="length"/> is less than zero.-or- The sum of <paramref name="startIndex"/> and <paramref name="length"/>
        /// is greater than the length of the current instance</exception>
        public string ToString(int startIndex, int length)
        {
            if (startIndex <= 0 || startIndex > this.Length - 1)
                throw new ArgumentOutOfRangeException("startIndex");
            if (length > (startIndex + (this.Length - startIndex)))
                throw new ArgumentOutOfRangeException("length");

            //lock (this.syncObject)
            return this.result.ToString().Substring(startIndex, length);
        }

        /// <summary>
        /// Gets the hash code for a <see cref="StringBuilder"/>.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            //lock (this.syncObject)
            return this.result.GetHashCode();
        }
        #endregion
        #endregion

        #region Public Static Implicit Operators
        public static implicit operator string(StringBuilder instance)
        {
            return instance.result.ToString();
        }

        public static implicit operator StringBuilder(string instance)
        {
            StringBuilder returnValue = new StringBuilder();
            returnValue.Append(instance);

            return returnValue;
        }

        public static StringBuilder operator +(StringBuilder instance, string value)
        {
            instance.Append(value);
            return instance;
        }

        public static bool operator ==(StringBuilder instance, string value)
        {
            return instance.ToString().Equals(value);
        }

        public static bool operator !=(StringBuilder instance, string value)
        {
            return !instance.ToString().Equals(value);
        }
        #endregion


    }
}
