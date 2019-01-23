using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public delegate void ListChangedEventHandler<T>(IList<T> sender, ListChangedEventArgs<T> e);
}
