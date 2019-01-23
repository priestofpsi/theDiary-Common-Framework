using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public abstract class ClientActionBase
    : IClientExecutionAction
    {
        #region Constructors
        protected ClientActionBase(string actionName)
        {
            if (string.IsNullOrWhiteSpace(actionName))
                throw new ArgumentNullException("actionName");

            this.ActionName = actionName;
        }

        protected ClientActionBase(string actionName, ActionEventHandler executeHandler)
        {
            if (string.IsNullOrWhiteSpace(actionName))
                throw new ArgumentNullException("actionName");

            if (executeHandler == null)
                throw new ArgumentNullException("executeHandler");

            this.ActionName = actionName;
            this.Execute = executeHandler;
        }
        #endregion

        #region Public Read-Only Properties
        public ActionEventHandler Execute { get; protected set; }

        public string ActionName { get; protected set; }
        #endregion
    }
}
