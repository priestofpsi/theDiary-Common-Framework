using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web
{
    /// <summary>
    /// Represents a Html element attribute.
    /// </summary>
    public sealed class HtmlAttribute
        : IEquatable<string>,
        IEquatable<HtmlAttribute>,
        IEquatable<KeyValuePair<string,string>>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttribute"/> class.
        /// </summary>
        public HtmlAttribute()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttribute"/> class, with the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        public HtmlAttribute(string name)
            : this()
        {
            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttribute"/> class, with the specified <paramref name="name"/> and indicating if
        /// the value should be hidden or not when rendered.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="hideValue"><c>True</c> if the value should not be rendered if empty, otherwise <c>False</c>.</param>
        public HtmlAttribute(string name, bool hideValue)
            : this()
        {
            this.Name = name;
            this.HideValueIfEmpty = hideValue;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttribute"/> class, with the specified <paramref name="name"/> and <paramref name="value"/>.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value contained in the attribute.</param>
        public HtmlAttribute(string name, string value)
            : this(name)
        {
            this.Value = value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HtmlAttribute"/> class, with the specified <paramref name="name"/>, <paramref name="value"/> and indicating if
        /// the value should be hidden or not when rendered.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value contained in the attribute.</param>
        /// <param name="hideValueIfEmpty"><c>True</c> if the value should not be rendered if empty, otherwise <c>False</c>.</param>
        public HtmlAttribute(string name, string value, bool hideValueIfEmpty)
            : this(name, hideValueIfEmpty)
        {
            this.Value = value;
        }
        #endregion

        #region Private Declarations
        private string name;
        private string value;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the attribute.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("value");

                this.name = value;
            }
        }

        /// <summary>
        /// Gets or sets the value of the attribute.
        /// </summary>
        public string Value
        {
            get
            {
                return this.value ?? string.Empty;
            }
            set
            {
                this.value = value ?? string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets whether the value is hidden or not if <value>Value</value> is empty.
        /// </summary>
        public bool HideValueIfEmpty { get; set; }
        
        #endregion

        #region Private Properties
        /// <summary>
        /// Gets a value indicating if the <value>Value</value> is empty or not.
        /// </summary>
        private bool IsEmpty
        {
            get
            {
                return string.IsNullOrWhiteSpace(this.Value);
            }
        }
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Returns a <see cref="String"/> representation of a <see cref="HtmlAttribute"/>.
        /// </summary>
        /// <returns>The <see cref="String"/> representation of the instance.</returns>
        public override string ToString()
        {
            if (this.IsEmpty && this.HideValueIfEmpty)
                return this.Name;

            return string.Format("{0}='{1}'", this.Name, this.Value);
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            return base.Equals(obj);
        }

        public bool Equals(string val)
        {
            if (val == null)
                return false;

            return this.Equals(HtmlAttribute.Parse(val));
        }

        public bool Equals(HtmlAttribute val)
        {
            if (val == null)
                return false;

            return this.Name.Equals(val.Name, StringComparison.OrdinalIgnoreCase)
                && this.Value.Equals(val.Value);
        }

        public bool Equals(KeyValuePair<string, string> val)
        {
            if (string.IsNullOrWhiteSpace(val.Key))
                return false;

            return this.Equals(new HtmlAttribute(val.Key, val.Value));
        }

        /// <summary>
        /// Returns the hash code for this <see cref="HtmlAttribute"/>.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.Name.ToLower().GetHashCode()
                | this.Value.GetHashCode()
                | this.HideValueIfEmpty.GetHashCode();
        }
        #endregion

        #region Public Static Methods & Functions
        public static HtmlAttribute Parse(string attribute)
        {
            if (string.IsNullOrWhiteSpace(attribute))
                throw new ArgumentNullException("attribute");

            string[] vals = attribute.Split('=');
            if (vals.Length == 1)
                return new HtmlAttribute(vals[0]);

            return new HtmlAttribute(vals[0], vals[1]);
        }
        #endregion

        #region Public Operators
        public static implicit operator string(HtmlAttribute value)
        {
            if (value == null)
                throw new ArgumentNullException("value");

            return value.ToString();
        }

        public static explicit operator HtmlAttribute(string value)
        {
            return HtmlAttribute.Parse(value);
        }
        #endregion
    }
}
