using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    /// <summary>
    /// The delegate that is called after an Asyncronous Translation has been completed.
    /// </summary>
    /// <param name="translationDetails">The details of the translation.</param>
    public delegate void TranslateTextCallBack(TranslateDetails translationDetails);

    /// <summary>
    /// The delegate that is called when a translation is required.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="TranslateEventArgs"/> instance containing the details of the translation.</param>
    /// <returns>The translated text.</returns>
    public delegate string TranslateHandler(object sender, TranslateEventArgs e);
}
