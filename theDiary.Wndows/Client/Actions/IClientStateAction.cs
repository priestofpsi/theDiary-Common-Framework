using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public interface IClientPropertySetter
        : IClientAction
    {
    }

    public interface IClientStateAction<T>
        : IClientAction
    {
        T State { get; }

        void SetState(T state);
    }
}
