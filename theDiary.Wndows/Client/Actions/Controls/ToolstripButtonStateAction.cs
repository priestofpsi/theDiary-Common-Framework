using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions.Controls
{
    public class ToolstripButtonStateAction<T>
        : ClientActionGroupBase<T>, 
        IClientControlStateAction<T, System.Windows.Forms.ToolStripButton>
    {
        #region Constructors
        #region Straight Constructors
        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, int imageIndex, string text, string tooltip)
            : base(groupName, actionName, executeHandler)
        {
            this.GetImageIndex = new ActionStateEventHandler<T, int>((s, e) => imageIndex);
            this.GetText = new ActionStateEventHandler<T, string>((s, e) => text);
            this.GetToolTip = new ActionStateEventHandler<T, string>((s, e) => tooltip ?? string.Empty);
            this.EnableDisable = (s, e) => true;
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, int imageIndex, string text, string tooltip)
            : this(null, actionName, executeHandler, imageIndex, text, tooltip)
        {
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, int imageIndex, string text)
            : this(null, actionName, executeHandler, imageIndex, text, string.Empty)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, int imageIndex, string text)
            : this(groupName, actionName, executeHandler, imageIndex, text, string.Empty)
        {
        }
        #endregion

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, int imageIndex, string text, ActionStateEventHandler<T, bool> enableDisableHandler)
            : this(null, actionName, executeHandler, imageIndex, text, enableDisableHandler)
        {

        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, int imageIndex, string text, ActionStateEventHandler<T, bool> enableDisableHandler, ActionStateEventHandler<T, bool> visibilityHandler)
            : this(null, actionName, executeHandler, imageIndex, text, enableDisableHandler, visibilityHandler)
        {

        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executHandler, int imageIndex, string text, ActionStateEventHandler<T, bool> enableDisableHandler)
            : base(groupName, actionName, executHandler)
        {
            this.GetImageIndex = new ActionStateEventHandler<T, int>((s, e) => imageIndex);
            this.GetText = new ActionStateEventHandler<T, string>((s, e) => text);
            this.GetToolTip = new ActionStateEventHandler<T, string>((s, e) => string.Empty);
            this.EnableDisable = enableDisableHandler;
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executHandler, int imageIndex, string text, ActionStateEventHandler<T, bool> enableDisableHandler, ActionStateEventHandler<T, bool> visibilityHandler)
            : base(groupName, actionName, executHandler)
        {
            this.GetImageIndex = new ActionStateEventHandler<T, int>((s, e) => imageIndex);
            this.GetText = new ActionStateEventHandler<T, string>((s, e) => text);
            this.GetToolTip = new ActionStateEventHandler<T, string>((s, e) => string.Empty);
            this.EnableDisable = enableDisableHandler;
            this.GetVisibility = visibilityHandler;
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionStateEventHandler<T> executHandler, int imageIndex, string text, ActionStateEventHandler<T, bool> enableDisableHandler, ActionStateEventHandler<T, bool> visibilityHandler)
            : base(groupName, actionName)
        {
            this.Execute = executHandler;
            this.GetImageIndex = new ActionStateEventHandler<T, int>((s, e) => imageIndex);
            this.GetText = new ActionStateEventHandler<T, string>((s, e) => text);
            this.GetToolTip = new ActionStateEventHandler<T, string>((s, e) => string.Empty);
            this.EnableDisable = enableDisableHandler;
            this.GetVisibility = visibilityHandler;
        }

        #region Func Constructors
        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, Func<T, int> imageFunc, Func<T, string> textFunc, Func<T, bool> enableDisableFunc)
            : this(null, actionName, executeHandler, imageFunc, textFunc, null, enableDisableFunc)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, Func<T, int> imageFunc, Func<T, string> textFunc, Func<T, bool> enableDisableFunc)
            : this(groupName, actionName, executeHandler, imageFunc, textFunc, null, enableDisableFunc)
        {
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, Func<T, int> imageFunc, Func<T, string> textFunc, Func<T, string> tooltipFunc)
            : this(null, actionName, executeHandler, imageFunc, textFunc, tooltipFunc, null)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, Func<T, int> imageFunc, Func<T, string> textFunc, Func<T, string> tooltipFunc)
            : this(groupName, actionName, executeHandler, imageFunc, textFunc, tooltipFunc, null)
        {
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, Func<T, int> imageFunc, Func<T, string> textFunc, Func<T, string> tooltipFunc, Func<T, bool> enableDisableFunc)
            : this(null, actionName, executeHandler, imageFunc, textFunc, tooltipFunc, enableDisableFunc)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, Func<T, int> imageFunc, Func<T, string> textFunc, Func<T, string> tooltipFunc, Func<T, bool> enableDisableFunc)
            : base(groupName, actionName, executeHandler)
        {
            this.GetImageIndex = new ActionStateEventHandler<T, int>((s, e) => imageFunc(e.State));
            this.GetText = new ActionStateEventHandler<T, string>((s, e) => textFunc(e.State));

            if (tooltipFunc == null)
                tooltipFunc = new Func<T, string>(e => string.Empty);
            this.GetToolTip = new ActionStateEventHandler<T, string>((s, e) => tooltipFunc(e.State));

            if (enableDisableFunc == null)
                enableDisableFunc = new Func<T, bool>(e => e != null);
            this.EnableDisable = new ActionStateEventHandler<T, bool>((s, e) => enableDisableFunc(e.State));
        }
        #endregion

        #region Handler Constructors
        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler)
            : this(null, actionName, executeHandler, imageHandler, textHandler, null, null)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler)
            : this(groupName, actionName, executeHandler, imageHandler, textHandler, null, null)
        {
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, bool> enableDisableHandler)
            : this(null, actionName, executeHandler, imageHandler, textHandler, null, enableDisableHandler)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, bool> enableDisableHandler)
            : this(groupName, actionName, executeHandler, imageHandler, textHandler, null, enableDisableHandler)
        {
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, string> tooltipHandler)
            : this(null, actionName, executeHandler, imageHandler, textHandler, tooltipHandler, null)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, string> tooltipHandler)
            : this(groupName, actionName, executeHandler, imageHandler, textHandler, tooltipHandler, null)
        {
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, string> tooltipHandler, ActionStateEventHandler<T, bool> enableDisableHandler)
            : this(actionName, executeHandler, imageHandler, textHandler, tooltipHandler, enableDisableHandler, null)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, string> tooltipHandler, ActionStateEventHandler<T, bool> enableDisableHandler)
            : this(groupName, actionName, executeHandler, imageHandler, textHandler, tooltipHandler, enableDisableHandler, null)
        {
        }

        public ToolstripButtonStateAction(string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, string> tooltipHandler, ActionStateEventHandler<T, bool> enableDisableHandler, ActionStateEventHandler<T, bool> visibilityHandler)
            : base(null, actionName, executeHandler)
        {
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionEventHandler executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, string> tooltipHandler, ActionStateEventHandler<T, bool> enableDisableHandler, ActionStateEventHandler<T, bool> visibilityHandler)
            : base(groupName, actionName, executeHandler)
        {
            this.GetText = textHandler;
            this.GetImageIndex = imageHandler;
            this.GetToolTip = tooltipHandler ?? new ActionStateEventHandler<T, string>((s, e) => string.Empty);
            this.EnableDisable = enableDisableHandler;
            this.GetVisibility = visibilityHandler;
        }

        public ToolstripButtonStateAction(string groupName, string actionName, ActionStateEventHandler<T> executeHandler, ActionStateEventHandler<T, int> imageHandler, ActionStateEventHandler<T, string> textHandler, ActionStateEventHandler<T, string> tooltipHandler, ActionStateEventHandler<T, bool> enableDisableHandler, ActionStateEventHandler<T, bool> visibilityHandler)
            : base(groupName, actionName)
        {
            this.Execute = executeHandler;
            this.GetText = textHandler;
            this.GetImageIndex = imageHandler;
            this.GetToolTip = tooltipHandler ?? new ActionStateEventHandler<T, string>((s, e) => string.Empty);
            this.EnableDisable = enableDisableHandler;
            this.GetVisibility = visibilityHandler;
        }
        #endregion
        #endregion

        #region Private Declarations
        private string groupName;
        private string actionName;
        #endregion

        #region Public Properties

        public new ActionStateEventHandler<T> Execute { get; set; }

        public ActionStateEventHandler<T, bool> EnableDisable { get; set; }

        public ActionStateEventHandler<T, bool> GetVisibility { get; set; }

        public ActionStateEventHandler<T, int> GetImageIndex { get; set; }

        public ActionStateEventHandler<T, string> GetText { get; set; }

        public ActionStateEventHandler<T, string> GetToolTip { get; set; }
        #endregion

        public T State { get; private set; }

        void IClientStateAction<T>.SetState(T state)
        {
            this.State = state;
        }

        void IClientControlStateAction.SetState(object state)
        {
            if (state == null || state is T)
                ((IClientStateAction<T>)this).SetState((T)state);
        }

        void IClientControlStateAction.SetControl(object control, object state)
        {
            if ((state == null || state is T)
                && (control != null && control is System.Windows.Forms.ToolStripButton))
                ((IClientControlStateAction<T, System.Windows.Forms.ToolStripButton>)this).SetControl((System.Windows.Forms.ToolStripButton)control, (T)state);
        }

        void IClientControlAction<System.Windows.Forms.ToolStripButton>.SetControl(System.Windows.Forms.ToolStripButton control)
        {
            if (this.GetImageIndex != null)
                control.ImageIndex = this.GetImageIndex(control, new ClientActionEventArgs<T>(this));

            if (this.GetText != null)
                control.Text = this.GetText(control, new ClientActionEventArgs<T>(this));

            if (this.GetToolTip != null)
                control.ToolTipText = this.GetToolTip(control, new ClientActionEventArgs<T>(this));

            if (this.GetVisibility != null)
                control.Visible = this.GetVisibility(control, new ClientActionEventArgs<T>(this));

            control.Click += (s, e) => this.Execute(s, new ClientActionEventArgs<T>(this, this.State));
        }

        void IClientControlStateAction<T, System.Windows.Forms.ToolStripButton>.SetControl(System.Windows.Forms.ToolStripButton control, T state)
        {
            if (this.GetImageIndex != null)
                control.ImageIndex = this.GetImageIndex(control, new ClientActionEventArgs<T>(this, state));

            if (this.GetText != null)
                control.Text = this.GetText(control, new ClientActionEventArgs<T>(this, state));

            if (this.GetToolTip != null)
                control.ToolTipText = this.GetToolTip(control, new ClientActionEventArgs<T>(this, state));

            if (this.GetVisibility != null)
                control.Visible = this.GetVisibility(control, new ClientActionEventArgs<T>(this, state));
        }
    }
}
