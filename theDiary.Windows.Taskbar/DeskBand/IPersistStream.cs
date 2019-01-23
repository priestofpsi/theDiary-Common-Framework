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
    [Guid("00000109-0000-0000-C000-000000000046")]
    public interface IPersistStream
    {
        void GetClassID(out Guid pClassID);

        void IsDirty();

        void Load([In, MarshalAs(UnmanagedType.Interface)] Object pStm);

        void Save([In, MarshalAs(UnmanagedType.Interface)] Object pStm,
           [In] bool fClearDirty);

        void GetSizeMax([Out] out UInt64 pcbSize);
    }
}
