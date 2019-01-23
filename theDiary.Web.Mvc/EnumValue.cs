using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel;

namespace System
{
    public sealed class EnumValue
    {
        #region Constructors
        private EnumValue(FieldInfo enumField)
        {
            Type enumType = enumField.DeclaringType;
            DescriptionAttribute descriptionAttrib = (DescriptionAttribute)enumField.GetCustomAttributes(typeof(DescriptionAttribute), true).FirstOrDefault();
            this.Description = (descriptionAttrib == null) ? enumField.Name : descriptionAttrib.Description;
            this.Name = enumField.Name;
            this.Value = Convert.ChangeType(Enum.Parse(enumType, this.Name), enumType);
        }
        #endregion

        #region Public Read-Only Properties
        public dynamic Value { get; private set; }
        
        public string Name { get; private set; }
        
        public string Description { get; private set; }
        #endregion

        public static explicit operator EnumValue(FieldInfo enumField)
        {
            return new EnumValue(enumField);
        }
    }
}