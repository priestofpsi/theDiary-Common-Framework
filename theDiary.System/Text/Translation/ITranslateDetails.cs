using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Text.Translation
{
    public interface ITranslateDetails
    {
        #region Property Definitions
        string InputText { get; }

        System.Globalization.CultureInfo SourceCulture { get; }

        System.Globalization.CultureInfo TargetCulture { get; }
        #endregion
    }
}
