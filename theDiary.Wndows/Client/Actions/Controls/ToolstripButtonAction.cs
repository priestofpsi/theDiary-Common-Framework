using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Actions.Controls
{
    public class ToolstripButtonAction<T>
        : ClientActionGroupBase<T>, IClientControlAction<System.Windows.Forms.ToolStripButton>
    {
        #region Constructors
        public ToolstripButtonAction(string groupName, string actionName, ActionEventHandler executeHandler, int imageIndex, string text, string tooltip)
            : base(groupName, actionName, executeHandler)
        {
            this.ImageIndex = imageIndex;
            this.text = text;
            this.Tooltip = tooltip;
        }

        public ToolstripButtonAction(string actionName, ActionEventHandler executeHandler, int imageIndex, string text, string tooltip)
            : this(null, actionName, executeHandler, imageIndex, text, tooltip)
        {
        }

        public ToolstripButtonAction(string actionName, ActionEventHandler executeHandler, int imageIndex, string text)
            : this(null, actionName, executeHandler, imageIndex, text, string.Empty)
        {
        }

        public ToolstripButtonAction(string groupName, string actionName, ActionEventHandler executeHandler, int imageIndex, string text)
            : this(groupName, actionName, executeHandler, imageIndex, text, string.Empty)
        {
        }
        #endregion

        #region Private Declarations
        private string text;
        #endregion

        #region Public Properties
        public string Text
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.text))
                    return this.ActionName;

                return this.text;
            }
        }

        public int ImageIndex { get; private set; }

        public string Tooltip { get; private set; }

        public bool HasTooltip
        {
            get
            {
                return !string.IsNullOrWhiteSpace(this.Tooltip);
            }
        }
        #endregion

        #region Public Methods & Functions
        public void SetControl(System.Windows.Forms.ToolStripButton control)
        {
            control.Text = this.Text;
            control.ImageIndex = this.ImageIndex;
            control.ToolTipText = this.Tooltip;
            control.AutoToolTip = this.HasTooltip;

            control.Click += (s, e) => this.Execute(s, new ClientActionEventArgs(this));
        }
        #endregion
    }
}
