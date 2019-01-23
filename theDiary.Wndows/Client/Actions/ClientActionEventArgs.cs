using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public class ClientActionEventArgs
        : EventArgs
    {
        public ClientActionEventArgs(IClientAction action)
            : base()
        {
            this.Action = action;
        }

        public IClientAction Action { get; protected set; }

    }
    public class ClientActionEventArgs<T>
        : ClientActionEventArgs
    {
        public ClientActionEventArgs(IClientAction action)
            : base(action)
        {
        }

        public ClientActionEventArgs(IClientAction action, T state)
            : base(action)
        {
            this.State = state;
        }

        public T State { get; private set; }
    }
}
