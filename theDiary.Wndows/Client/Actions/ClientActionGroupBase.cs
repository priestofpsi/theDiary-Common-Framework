using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions
{
    public abstract class ClientActionGroupBase<T>
    : ClientActionGroupBase, 
        IClientActionGroup<T>
    {
        #region Constructors
        protected ClientActionGroupBase(string actionName)
            : base(actionName)
        {
            this.GroupName = string.Empty;
        }

        protected ClientActionGroupBase(string actionName, ActionEventHandler executeHandler)
            : base(actionName, executeHandler)
        {
            this.GroupName = string.Empty;
        }

        protected ClientActionGroupBase(string groupName, string actionName)
            : base(actionName)
        {
            this.GroupName = string.IsNullOrWhiteSpace(groupName) ? string.Empty : groupName;
        }

        protected ClientActionGroupBase(string groupName, string actionName, ActionEventHandler executeHandler)
            : base(actionName, executeHandler)
        {
            this.GroupName = string.IsNullOrWhiteSpace(groupName) ? string.Empty : groupName;
        }
        #endregion
    }

    public abstract class ClientActionGroupBase
        : ClientActionBase,
        IClientActionGroup
    {
        #region Constructors
        protected ClientActionGroupBase(string actionName)
            : base(actionName)
        {
            this.GroupName = string.Empty;
        }

        protected ClientActionGroupBase(string actionName, ActionEventHandler executeHandler)
            : base(actionName, executeHandler)
        {
            this.GroupName = string.Empty;
        }

        protected ClientActionGroupBase(string groupName, string actionName)
            : base(actionName)
        {
            this.GroupName = string.IsNullOrWhiteSpace(groupName) ? string.Empty : groupName;
        }

        protected ClientActionGroupBase(string groupName, string actionName, ActionEventHandler executeHandler)
            : base(actionName, executeHandler)
        {
            this.GroupName = string.IsNullOrWhiteSpace(groupName) ? string.Empty : groupName;
        }
        #endregion

        #region Public Properties
        public string GroupName { get; protected set; }
        #endregion
    }
}
