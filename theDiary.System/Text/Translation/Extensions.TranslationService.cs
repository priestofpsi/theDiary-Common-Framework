using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    public static class TranslationExtensions
    {
        public static TranslateDetails CreateTranslateDetails(this ITranslationProvider translationService, string input, System.Globalization.CultureInfo sourceCulture, System.Globalization.CultureInfo targetCulture)
        {
            return new TranslateDetails(translationService.GetType(), input, sourceCulture, targetCulture);
        }

        public static TranslateDetails SetTranslationDetails(this ITranslationProvider translationService, TranslateDetails translationDetails, string translatedText)
        {
            translationDetails.TranslatedText = translatedText;
            return translationDetails;
        }

        public static TranslateDetails SetTranslationDetails(this ITranslationProvider translationService, TranslateDetails translationDetails, Exception translationError)
        {
            translationDetails.TranslationError = translationError;
            return translationDetails;
        }
    }
}
