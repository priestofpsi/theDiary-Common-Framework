using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seth.Windows.Taskbar.DeskBand
{
    /// <summary>
    /// Represents different styles of a <see cref="BandObject"/>
    /// </summary>
    [Flags]
    [Serializable]
    public enum BandObjectStyle 
        : uint
    {
        Vertical = 1,
        
        Horizontal = 2,
        
        ExplorerToolbar = 4,
        
        TaskbarToolBar = 8
    }
}
