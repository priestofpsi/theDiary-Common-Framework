using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public delegate TOut ActionStateEventHandler<T, TOut>(object sender, ClientActionEventArgs<T> e);

    public delegate void ActionStateEventHandler<T>(object sender, ClientActionEventArgs<T> e);

    public delegate void ActionEventHandler(object sender, ClientActionEventArgs e);
}
