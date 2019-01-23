using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Templates
{
    public sealed class TypeFormatArgument
    {
        #region Constructors
        public TypeFormatArgument(Type type, string formatArgument)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrWhiteSpace(formatArgument))
                throw new ArgumentOutOfRangeException("formatArgument");

            this.Type = type;
            this.FormatArgument = formatArgument;
        }
        #endregion

        #region Public Read-Only Properties
        public Type Type { get; private set; }

        public string FormatArgument { get; private set; }
        #endregion

        public static implicit operator KeyValuePair<Type, string>(TypeFormatArgument typeFormatArg)
        {
            return new KeyValuePair<Type, string>(typeFormatArg.Type, typeFormatArg.FormatArgument);
        }

        public static implicit operator TypeFormatArgument(KeyValuePair<Type, string> typeFormatArg)
        {
            return new TypeFormatArgument(typeFormatArg.Key, typeFormatArg.Value);
        }
    }
}
