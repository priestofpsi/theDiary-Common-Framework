using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public interface IClientControlAction<TControl>
        where TControl : Component
    {
        void SetControl(TControl control);
    }
}
