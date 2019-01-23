using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace System.Resources
{
    public sealed class ResourceKeyValue
        : IResourceKey, 
        IResourceInfo, 
        IXmlSerializable,
        IEquatable<ResourceKeyValue>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourveKeyValue"/> class containing the <paramref name="key"/> and <paramref name="value"/>
        /// for the default system <see cref="CultureInfo"/>.
        /// </summary>
        /// <param name="key"><see cref="String"/> identifing the resource key.</param>
        /// <param name="value"><see cref="String"/> value of the resource key.</param>
        public ResourceKeyValue(string key, string value)
        {
            this.ResourceKey = key;
            this.Value = new ResourceValue(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ResourveKeyValue"/> class containing the <paramref name="key"/> and <paramref name="value"/>
        /// for the specified <paramref name="culture"/>.
        /// </summary>
        /// <param name="key"><see cref="String"/> identifing the resource key.</param>
        /// <param name="value"><see cref="String"/> value of the resource key.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> for the resource key.</param>
        public ResourceKeyValue(string key, string value, CultureInfo culture)
        {
            this.ResourceKey = key;
            this.Value = new ResourceValue(value, culture);
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Gets the key identifing the resource.
        /// </summary>
        public string ResourceKey { get; private set; }

        /// <summary>
        /// Gets the <see cref="CultureInfo"/> for the <see cref="ResourceValue"/>.
        /// </summary>
        public CultureInfo Culture
        {
            get
            {
                return this.Value.Culture;
            }
        }

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

        public ResourceValue Value { get; private set; }

        /// <summary>
        /// Gets the <see cref="String"/> value of the Resource.
        /// </summary>
        string IResourceValue.Value
        {
            get
            {
                return this.Value.Value;
            }
        }
        #endregion

        #region Public Methods & Functions
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ResourceKeyValue rkv = obj as ResourceKeyValue;
            if (rkv != null)
                return this.Equals(rkv);

            return this.Value.Equals(obj as string);
        }

        public bool Equals(ResourceKeyValue other)
        {
            if (other == null)
                return false;

            return this.Culture.Equals(other.Culture)
                && this.ResourceKey.Equals(other.ResourceKey)
                && this.Value.Equals(other.Value);
        }

        public override string ToString()
        {
            StringBuilder returnValue = new StringBuilder("{");
            returnValue.Append("{0}:{1}", this.ResourceKey, this.Value);
            returnValue.Append("}");

            return returnValue;
        }

        public override int GetHashCode()
        {
            return this.ResourceKey.GetHashCode()
                | this.Value.GetHashCode()
                | this.CultureISOCode.GetHashCode();
        }
        #endregion

        #region IXmlSerializable Methods & Functions
        Xml.Schema.XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        void IXmlSerializable.ReadXml(Xml.XmlReader reader)
        {
            this.ResourceKey = reader.GetAttribute("name");
            this.Value = (ResourceValue)reader.ReadContentAs(typeof(ResourceValue), null);
        }

        void IXmlSerializable.WriteXml(Xml.XmlWriter writer)
        {
            writer.WriteStartElement("data");
            writer.WriteAttributeString("name", this.ResourceKey);
            writer.WriteAttributeString("xml:space", "preserve");
            ((IXmlSerializable)this.Value).WriteXml(writer);
            writer.WriteEndElement();
        }
        #endregion
    }
}
