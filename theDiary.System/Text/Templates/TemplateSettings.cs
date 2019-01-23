using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Templates
{
    public sealed class TemplateSettings
    {
        #region Constructors
        public TemplateSettings()
            : this(TemplateSettings.DefaultEmptyReferenceValue, false)
        {

        }

        public TemplateSettings(string emptyReferenceValue)
            : this(emptyReferenceValue, false)
        {
            
        }

        public TemplateSettings(bool throwErrorOnEmptyReference)
            : this(TemplateSettings.DefaultEmptyReferenceValue, throwErrorOnEmptyReference)
        {
        }

        public TemplateSettings(string emptyReferenceValue, bool throwErrorOnEmptyReference)
        {
            this.typeFormatArguments = new Dictionary<Type, string>();
            this.EmptyReferenceValue = emptyReferenceValue;
            this.ThrowErrorOnEmptyReference = throwErrorOnEmptyReference;
        }

        public TemplateSettings(string emptyReferenceValue, bool throwErrorOnEmptyReference, params TypeFormatArgument[] formatArgs)
            : this(emptyReferenceValue, throwErrorOnEmptyReference)
        {
            if (!formatArgs.IsNullOrEmpty())
            foreach (var formatArg in formatArgs)
                if (formatArg != null)
                    this.typeFormatArguments.Add(formatArg.Type, formatArg.FormatArgument);
        }
        #endregion

        #region Private Declarations
        private Dictionary<Type, string> typeFormatArguments;
        #endregion

        #region Public Constant Declarations
        public const string DefaultEmptyReferenceValue = "<NULL>";
        #endregion

        #region Public Properties
        public bool ThrowErrorOnEmptyReference { get; set; }

        public string EmptyReferenceValue { get; set; }

        public string this[Type type]
        {
            get
            {
                if (this.typeFormatArguments.ContainsKey(type))
                    return this.typeFormatArguments[type];

                return null;
            }
        }

        public IEnumerable<TypeFormatArgument> TypeFormatArguments
        {
            get
            {
                foreach (KeyValuePair<Type, string> value in this.typeFormatArguments)
                    yield return value;
            }
        }
        #endregion

        #region Public Methods & Functions
        public bool ContainsTypeFormat(Type type)
        {
            return this.typeFormatArguments.ContainsKey(type);
        }

        public string Format(object value)
        {
            if (value == null)
                return this.EmptyReferenceValue;

            Type valueType = value.GetType();
            return ((IFormattable)value).ToString(this[valueType], null);
        }
        #endregion

        #region Public Static Read-Only Properties
        public static TemplateSettings Default
        {
            get
            {
                return new TemplateSettings();
            }
        }
        #endregion
    }

   
}
