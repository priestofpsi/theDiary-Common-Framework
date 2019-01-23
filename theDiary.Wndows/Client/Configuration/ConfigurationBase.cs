using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Configuration
{
    public abstract class ConfigurationBase<TConfigurationWrapper>
        : System.ComponentModel.INotifyPropertyChanged
        where TConfigurationWrapper : class, IConfigurationWrapper, new()
    {
        protected ConfigurationBase()
            : base()
        {
        }

        private static readonly ConfigurationBase<TConfigurationWrapper> instance;
        private static readonly TConfigurationWrapper configurationHelper = System.Activator.CreateInstance<TConfigurationWrapper>();

        protected static TConfigurationWrapper ConfigurationHelper
        {
            get
            {
                return ConfigurationBase<TConfigurationWrapper>.configurationHelper;
            }
        }

        public static ConfigurationBase<TConfigurationWrapper> Instance
        {
            get
            {
                return ConfigurationBase<TConfigurationWrapper>.instance;
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public bool MinimizeAfterRun
        {
            get
            {
                return ConfigurationHelper.ReadConfiguration<bool>("MinimizeAfterRun");
            }
            set
            {
                if (this.MinimizeAfterRun == value)
                    return;

                ConfigurationHelper.SaveConfiguration<bool>("MinimizeAfterRun", value);
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("MinimizeAfterRun"));
            }
        }

        public bool MinimizeToTray
        {
            get
            {
                return ConfigurationHelper.ReadConfiguration<bool>("MinimizeToTray");
            }
            set
            {
                if (this.MinimizeToTray == value)
                    return;

                ConfigurationHelper.SaveConfiguration<bool>("MinimizeToTray", value);
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("MinimizeToTray"));
            }
        }

        public bool AlwaysOnTop
        {
            get
            {
                return ConfigurationHelper.ReadConfiguration<bool>("AlwaysOnTop");
            }
            set
            {
                if (this.AlwaysOnTop == value)
                    return;
                ConfigurationHelper.SaveConfiguration<bool>("AlwaysOnTop", value);
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("AlwaysOnTop"));
            }
        }

        public bool ShowStatusbar
        {
            get
            {
                return ConfigurationHelper.ReadConfiguration<bool>("ShowStatusbar");
            }
            set
            {
                if (this.ShowStatusbar == value)
                    return;
                ConfigurationHelper.SaveConfiguration<bool>("ShowStatusbar", value);
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("ShowStatusbar"));
            }
        }

        public ListItemsView CurrentItemsView
        {
            get
            {
                return (ListItemsView)Enum.Parse(typeof(ListItemsView), ConfigurationHelper.ReadConfiguration("CurrentItemsView", ListItemsView.MediumIcon.ToString()), true);
            }
            set
            {
                if (this.CurrentItemsView == value)
                    return;
                ConfigurationHelper.SaveConfiguration("CurrentItemsView", value.ToString());
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("CurrentItemsView"));
            }
        }

        public ToolstripView CurrentToolstripView
        {
            get
            {
                return ConfigurationHelper.ReadConfiguration<ToolstripView, byte>("CurrentToolstripView", (byte)(ToolstripView.SmallIcons));
            }
            set
            {
                if (this.CurrentToolstripView == value)
                    return;
                ConfigurationHelper.SaveConfiguration("CurrentToolstripView", value.ToString());
                if (this.PropertyChanged != null)
                    this.PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs("CurrentToolstripView"));
            }
        }
    }
}
