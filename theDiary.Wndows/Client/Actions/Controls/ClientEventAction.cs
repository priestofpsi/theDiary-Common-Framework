using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions.Controls
{
    public abstract class ClientEventAction<TControl>
        : IClientEventAction<TControl>
    {
        protected ClientEventAction(string eventName, Delegate execute)
            : this(string.Format("{1}_{0}", eventName, typeof(TControl).Name), eventName, execute)
        {

        }

        protected ClientEventAction(string actionName, string eventName, Delegate execute)
            : base()
        {
            this.ActionName = actionName;
            this.EventName = eventName;
            this.Execute = execute;
        }

        public string ActionName { get; private set; }

        public string EventName { get; protected set; }

        protected Delegate Execute { get; set; }

        public void AttachAction(TControl target)
        {
            EventInfo @event = typeof(TControl).GetEvent(this.EventName);
            @event.AddEventHandler(target, (Delegate)this.Execute);
        }
    }
}
