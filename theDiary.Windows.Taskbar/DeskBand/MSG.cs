using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seth.Windows.Taskbar.DeskBand
{
    public struct MSG
    {
        public IntPtr hwnd;
        public UInt32 message;
        public UInt32 wParam;
        public Int32 lParam;
        public UInt32 time;
        public POINT pt;
    }
}
