using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Resources
{
    public class DefaultResourceValue
        : ResourceValue
    {
        public DefaultResourceValue(string resourceValue)
            : base(resourceValue, ResourceController.Instance.DefaultCulture)
        {
        }
    }

    public class ResourceValue
        : ISerializable, IXmlSerializable, IResourceValue
    {
        #region Constructors
        public ResourceValue(string resourceValue)
            : this(resourceValue, ResourceController.Instance.DefaultCulture)
        {
        }

        public ResourceValue(string resourceValue, CultureInfo culture)
            : base()
        {
            this.Culture = culture;
            this.Value = resourceValue ?? string.Empty;
        }

        protected ResourceValue(SerializationInfo info, StreamingContext context)
        {
            this.Value = info.GetString("data");
            this.Culture = (CultureInfo)info.GetValue("culture", typeof(CultureInfo));
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Gets the <see cref="CultureInfo"/> for the <see cref="ResourceValue"/>.
        /// </summary>
        public CultureInfo Culture { get; private set; }

        /// <summary>
        /// Gets the ISO Code for the culture associated to the <see cref="ResourceValue"/>.
        /// </summary>
        public string CultureISOCode
        {
            get
            {
                return this.Culture.TwoLetterISOLanguageName;
            }
        }

        /// <summary>
        /// Gets the <see cref="String"/> value of the Resource.
        /// </summary>
        public string Value { get; private set; }
        #endregion

        /// <summary>
        /// Returns the hash code for this <see cref="ResourceValue"/>.
        /// </summary>
        /// <returns>The hash code for this instance.</returns>
        public override int GetHashCode()
        {
            return this.Culture.GetHashCode()
                | (this.Value ?? string.Empty).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.IsNullOrNotType<IResourceInfo>())
                return false;

            return ((IResourceInfo)obj).CultureISOCode.Equals(this.CultureISOCode)
                && ((IResourceInfo)obj).Value.Equals(this.Value);
        }

        #region Public Operators
        public static implicit operator string(ResourceValue resourceValue)
        {
            return resourceValue.Value;
        }

        public static implicit operator CultureInfo(ResourceValue resourceValue)
        {
            return resourceValue.Culture;
        }

        public static bool operator ==(ResourceValue resourceValue, CultureInfo culture)
        {
            return resourceValue.CultureISOCode.Equals(culture.TwoLetterISOLanguageName);
        }

        public static bool operator !=(ResourceValue resourceValue, CultureInfo culture)
        {
            return !resourceValue.CultureISOCode.Equals(culture.TwoLetterISOLanguageName);
        }

        public static bool operator ==(CultureInfo culture, ResourceValue resourceValue)
        {
            return resourceValue.CultureISOCode.Equals(culture.TwoLetterISOLanguageName);
        }

        public static bool operator !=(CultureInfo culture, ResourceValue resourceValue)
        {
            return !resourceValue.CultureISOCode.Equals(culture.TwoLetterISOLanguageName);
        }
        #endregion

        Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(Xml.XmlReader reader)
        {
            this.Value = reader.ReadContentAsString();
        }

        void IXmlSerializable.WriteXml(Xml.XmlWriter writer)
        {
            writer.WriteRaw(string.Format("<value>{0}</value>", this.Value));
        }

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("data", this.Value);
            info.AddValue("culture", this.Culture);
        }
    }
}
