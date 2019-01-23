using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public interface IClientEventAction
     : IClientEventAction<Control>
    {

    }

    public interface IClientEventAction<T>
        : IClientAction
    {
        void AttachAction(T target);
    }
}
