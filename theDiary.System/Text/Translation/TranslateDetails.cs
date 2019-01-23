using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    public sealed class TranslateDetails
        : ITranslateDetails
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TranslateDetails"/> class.
        /// </summary>
        public TranslateDetails()
            : base()
        {
        }

        internal TranslateDetails(Type translationServiceType, string input, System.Globalization.CultureInfo sourceCulture, System.Globalization.CultureInfo targetCulture)
        {
            this.TranslationServiceType = translationServiceType;
            this.InputText = input;
            this.SourceCulture = sourceCulture;
            this.TargetCulture = targetCulture;
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Gets the <see cref="Type"/> of <see cref="ITranslationProvider"/> that made the translation.
        /// </summary>
        public Type TranslationServiceType { get; private set; }

        /// <summary>
        /// Gets the original text requested for translation.
        /// </summary>
        public string InputText { get; private set; }

        /// <summary>
        /// Gets the translated text based on the <value>InputText</value>.
        /// </summary>
        public string TranslatedText { get; internal set; }

        /// <summary>
        /// Gets the <see cref="System.Globalization.CultureInfo"/> for the <value>InputText</value>.
        /// </summary>
        public System.Globalization.CultureInfo SourceCulture { get; private set; }

        /// <summary>
        /// Gets the <see cref="System.Globalization.CultureInfo"/> for the <value>TranslatedText</value>.
        /// </summary>
        public System.Globalization.CultureInfo TargetCulture { get; private set; }

        /// <summary>
        /// Gets the error that occured during the translation of the <see cref="ITranslationProviderAsync"/>.
        /// </summary>
        public Exception TranslationError { get; internal set; }

        /// <summary>
        /// Gets a value indicating if the translation was successfull or not.
        /// </summary>
        public bool Success
        {
            get
            {
                return (this.TranslationError == null);
            }
        }
        #endregion
    }
}
