using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    public sealed class TranslationController
        : Singleton<TranslationController>
    {
        #region Constructors
        public TranslationController()
            : base()
        {
            this.dictionary = new TranslationDictionary();
        }
        #endregion

        #region Private Declarations
        private TranslationDictionary dictionary;
        private ITranslationProvider translationService;
        #endregion

        public event EventHandler<TranslationServiceEventArgs> RequestTranslationService;

        #region Public Read-Only Properties
        public string this[string text, CultureInfo culture]
        {
            get
            {
                if (!this.dictionary.ContainsKey(text.Trim()))
                    this.dictionary.Add(text, new TranslationCulture());
                string translatedText = text;
                if (!this.dictionary[text].ContainsKey(culture))
                {                    
                    if (culture.TwoLetterISOLanguageName != System.Globalization.CultureInfo.InstalledUICulture.TwoLetterISOLanguageName)
                    {
                        try
                        {
                            translatedText = this.translationService.Translate(text, System.Globalization.CultureInfo.InstalledUICulture, culture);
                        }
                        catch
                        {
                        }
                    }
                    this.dictionary[text].Add(culture, translatedText);
                }
                return this.dictionary[text][culture];
            }
        }

        public ITranslationProvider Service
        {
            get
            {
                if (this.translationService == null)
                {
                    if (this.RequestTranslationService == null)
                        throw new InvalidOperationException("Unable to initialize TranslationService.");
                    TranslationServiceEventArgs e = new TranslationServiceEventArgs();
                    this.RequestTranslationService(this, e);
                    this.translationService = e.service;

                }
                return this.translationService;
            }
        }
        #endregion
        
        #region Private Classes
        private class TranslationDictionary
            : Dictionary<string, TranslationCulture>
        {
        }
        private class TranslationCulture
            : Dictionary<CultureInfo, string>
        {
        }
        #endregion
       
    }

    
}
