using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    /// <summary>
    /// Defines the requirements for implementing a service used to translate words asyncronously.
    /// </summary>
    public interface ITranslationProviderAsync
       : ITranslationProvider
    {
        #region Method & Function Definitions
        void TranslateAsync(string input, System.Globalization.CultureInfo sourceCulture, System.Globalization.CultureInfo targetCulture, TranslateTextCallBack callBack);
        #endregion
    }
}
