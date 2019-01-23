using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace System.Text.Translation
{
    /// <summary>
    /// Defines the requirements for implementing a service used to translate words.
    /// </summary>
    public interface ITranslationProvider
    {
        #region Property Definitions
        /// <summary>
        /// Returns the <see cref="CultureInfo"/> of the Source Language.
        /// </summary>
        CultureInfo SourceCulture { get; }

        /// <summary>
        /// Returns the English name of the Source Language.
        /// </summary>
        string SourceLanguage { get; }
        
        /// <summary>
        /// Returns the ISO code of the Source Language.
        /// </summary>
        string SourceLanguageCode { get; }
        #endregion

        #region Methods & Function Definitions
        string Translate(string input, System.Globalization.CultureInfo sourceCulture, System.Globalization.CultureInfo targetCulture);
        #endregion
    }
}
