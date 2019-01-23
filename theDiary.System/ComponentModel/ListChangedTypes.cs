using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public enum ListChangedTypes
    {
        ItemAdded = 1,
        ItemsAdded = 2,

        ItemDeleted = 3,
        ItemsDeleted = 4,

        ItemMoved = 5,
        ItemsMoved = 6,

        ItemChanged = 7,
        ItemsChanged = 8
    }
}
