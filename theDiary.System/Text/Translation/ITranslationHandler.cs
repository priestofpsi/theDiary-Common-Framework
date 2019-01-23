using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    /// <summary>
    /// Defines the events and methods used for interatction with a <see cref="ITranslationProvider"/>.
    /// </summary>
    public interface ITranslationHandler
    {
        #region Event Definitions
        /// <summary>
        /// The event that is raised when a translation is required.
        /// </summary>
        event TranslateHandler Translate;
        #endregion
    }
}
