using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public interface IClientControlStateAction
        : IClientAction
    {
        void SetState(object state);

        void SetControl(object control, object state);
    }

    public interface IClientControlStateAction<TState, TControl>
        : IClientControlStateAction,
        IClientControlAction<TControl>,
        IClientStateAction<TState>
        where TControl : Component
    {
        new void SetControl(TControl control, TState state);
    }
}
