using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Controls
{
    public class WatermarkTextBox
        : TextBox
    {
        public WatermarkTextBox()
            : base()
        {
            this.WatermarkFont = new Drawing.Font(base.Font, Drawing.FontStyle.Italic);
            this.WatermarkColor = System.Drawing.SystemColors.InactiveCaptionText;

            this.GotFocus += this.TextBoxFocus;

            this.LostFocus += this.TextBoxLostFocus;
        }

        #region Private Declarations
        private string text = string.Empty;
        private System.Drawing.Font font;
        private System.Drawing.Font watermarkFont;
        private System.Drawing.Color foreColor;
        private System.Drawing.Color watermarkColor;
        private string watermarkText;
        private char? passwordChar;
        #endregion

        #region Private Read-Only Properties
        private bool WatermarkShown
        {
            get
            {
                return this.Text.Equals(this.WatermarkText);
            }
        }
        #endregion

        #region Public Properties
        [Description("The text to display as a Watermark.")]
        [Category("Appearance")]
        [DefaultValue("")]
        public string WatermarkText
        {
            get
            {
                return this.watermarkText ?? string.Empty;
            }
            set
            {
                this.watermarkText = value ?? string.Empty;
            }
        }

        [Category("Appearance")]
        [DefaultValue("InactiveCaptionText")]
        public System.Drawing.Color WatermarkColor
        {
            get
            {
                return this.watermarkColor;
            }
            set
            {
                this.watermarkColor = value;
            }
        }

        [DefaultValue(false)]
        public bool HasText
        {
            get
            {
                return this.Text != null
                    && !this.WatermarkShown;
            }
        }

        [Category("Appearance")]
        public System.Drawing.Font WatermarkFont
        {
            get
            {
                return this.watermarkFont;
            }
            set
            {
                this.watermarkFont = value;
            }
        }

        public override System.Drawing.Color ForeColor
        {
            get
            {
                if (this.foreColor != base.ForeColor)
                    return this.foreColor;

                return base.ForeColor;
            }
            set
            {
                this.foreColor = value;
            }
        }

        public override Drawing.Font Font
        {
            get
            {
                if (this.font == null)
                    this.font = base.Font;
                return this.font;
            }
            set
            {
                this.font = value;
            }
        }
        #endregion

        #region Private Methods & Functions
        private void TextBoxFocus(object sender, EventArgs e)
        {
            if (this.WatermarkShown)
                this.Text = string.Empty;
            base.Font = this.font;
            base.ForeColor = this.ForeColor;
            this.Invalidate();
        }

        private void TextBoxLostFocus(object sender, EventArgs e)
        {
            if (!this.WatermarkShown
                    && string.IsNullOrEmpty(this.Text))
            {
                base.Font = this.WatermarkFont;
                base.ForeColor = this.WatermarkColor;
                this.Text = this.WatermarkText;
            }
            else if (!this.WatermarkShown)
            {
                base.Font = this.font;
                base.ForeColor = this.ForeColor;
            }
            else if (this.WatermarkShown)
            {
                base.Font = this.WatermarkFont;
                base.ForeColor = this.WatermarkColor;

            }
            this.Invalidate();
        }
        #endregion
    }
}
