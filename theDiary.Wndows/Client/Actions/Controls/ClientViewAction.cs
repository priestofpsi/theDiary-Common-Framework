using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace System.Windows.Forms.Client.Actions.Controls
{
    public class ClientViewAction
        : ClientEventAction<ListView>
    {
        public ClientViewAction(string eventName, EventHandler execute)
            : base(eventName, (Delegate) execute)
        {
            
        }

        public ClientViewAction(string actionName, string eventName, EventHandler execute)
            : base(actionName, eventName, (Delegate) execute)
        {

        }
    }
}
