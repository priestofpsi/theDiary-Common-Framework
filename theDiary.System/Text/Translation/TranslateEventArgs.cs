using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    public class TranslateEventArgs
        : EventArgs, ITranslateDetails
    {
        #region Constructors
        public TranslateEventArgs(ITranslateDetails details)
            : base()
        {
            this.InputText = details.InputText;
            this.SourceCulture = details.SourceCulture;
            this.TargetCulture = details.TargetCulture;
        }

        public TranslateEventArgs(string inputText)
            : this(inputText, System.Globalization.CultureInfo.InstalledUICulture, System.Globalization.CultureInfo.CurrentUICulture)
        {
        }

        public TranslateEventArgs(string inputText, System.Globalization.CultureInfo targetCulture)
            : this(inputText, System.Globalization.CultureInfo.InstalledUICulture, targetCulture)
        {
        }

        public TranslateEventArgs(string inputText, System.Globalization.CultureInfo sourceCulture, System.Globalization.CultureInfo targetCulture)
            : base()
        {
            if (inputText == null)
                throw new ArgumentNullException("inputText");

            if (sourceCulture == null)
                throw new ArgumentNullException("sourceCulture");

            if (targetCulture == null)
                throw new ArgumentNullException("targetCulture");
            
            this.InputText = inputText;
            this.SourceCulture = sourceCulture;
            this.TargetCulture = targetCulture;
        }
        #endregion

        #region Public Read-Only Properties
        public TranslateDetails Details { get; private set; }

        public string InputText { get; private set; }

        public Globalization.CultureInfo SourceCulture { get; private set; }

        public Globalization.CultureInfo TargetCulture { get; private set; }
        #endregion
    }
}
