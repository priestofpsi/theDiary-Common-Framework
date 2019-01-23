using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Seth.Windows.Taskbar.DeskBand
{
    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("6d5140c1-7436-11ce-8034-00aa006009fa")]
    public interface _IServiceProvider
    {
        void QueryService(
            ref Guid guid,
            ref Guid riid,
            [MarshalAs(UnmanagedType.Interface)] out Object Obj);
    }
}
