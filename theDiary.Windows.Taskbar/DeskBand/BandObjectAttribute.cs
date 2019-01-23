using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seth.Windows.Taskbar.DeskBand
{
    /// <summary>
    /// Specifies Style of the band object, its Name(displayed in explorer menu) and HelpText(displayed in status bar when menu command selected).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BandObjectAttribute : System.Attribute
    {
        public BandObjectAttribute() { }

        public BandObjectAttribute(string name, BandObjectStyle style)
        {
            Name = name;
            Style = style;
        }
        public BandObjectStyle Style;
        public string Name;
        public string HelpText;
    }
}
