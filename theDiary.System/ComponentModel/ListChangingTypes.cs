using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public enum ListChangingTypes
    {
        ItemAdding = 1,
        ItemsAdding = 2,

        ItemDeleting = 3,
        ItemsDeleting = 4,

        ItemMoving = 5,
        ItemsMoving = 6,

        ItemChanging = 7,
        ItemsChanging = 8
    }
}
