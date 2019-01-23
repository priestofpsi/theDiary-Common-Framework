using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Controls
{
    public class DynamicColumnHeader
        : ColumnHeader, INotifyPropertyChanged, IOrdinalMetaData
    {
        #region Constructors
        public DynamicColumnHeader()
            : base()
        {
            this.AddPropertyChangedHandler();
        }
        
        public DynamicColumnHeader(PropertyInfoMetaData property)
            : this()
        {
            this.metaData = property;
            this.AssociatedProperty = property;
        }

        public DynamicColumnHeader(PropertyInfoMetaData property, int imageIndex)
            : this(imageIndex)
        {
            this.metaData = property;
            this.AssociatedProperty = property;
        }

        public DynamicColumnHeader(PropertyInfoMetaData property, string imageKey)
            : this(imageKey)
        {
            this.metaData = property;
            this.AssociatedProperty = property;
        }

        private DynamicColumnHeader(int imageIndex)
            : base(imageIndex)
        {
            this.AddPropertyChangedHandler();
        }

        private  DynamicColumnHeader(string imageKey)
            : base(imageKey)
        {
            this.AddPropertyChangedHandler();
        }
        #endregion

        private event PropertyChangedEventHandler propertyChanged;
        private IOrdinalMetaData metaData;

        #region Public Properties
        
        public PropertyInfo AssociatedProperty
        {
            get
            {
                return base.Tag as PropertyInfo;
            }
            set
            {
                if (base.Tag == value)
                    return;

                base.Tag = value;
                if (this.propertyChanged != null)
                    this.propertyChanged(this, new PropertyChangedEventArgs("AssociatedProperty"));
            }
        }

        public new object Tag
        {
            get
            {
                return base.Tag;
            }
            set
            {
                this.AssociatedProperty = value as PropertyInfo;

                if (this.propertyChanged != null)
                    this.propertyChanged(this, new PropertyChangedEventArgs("Tag"));
            }
        }
        #endregion
        private void AddPropertyChangedHandler()
        {
            this.propertyChanged += (s, e) =>
            {
                if (e.PropertyName.Equals("AssociatedProperty")
                    || e.PropertyName.Equals("Tag"))
                {
                    if (this.AssociatedProperty.IsNull())
                    {
                        this.Text = "Column";
                    }
                    else
                    {
                        ColumnNameAttribute columnName = this.AssociatedProperty.GetAttribute<ColumnNameAttribute>();
                        this.Text = (columnName.IsNotNull()) ? columnName.ColumnName : this.AssociatedProperty != null ? this.AssociatedProperty.Name.AsReadable() : "Column";
                        this.Name = string.Format("dch{0}", this.AssociatedProperty.Name);
                    }
                }
            };
        }
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add 
            { 
                this.propertyChanged += value; 
            }
            remove 
            {
                this.propertyChanged -= value;
            }
        }

        int IOrdinalMetaData.ActualOrdinal
        {
            get
            {
                return this.metaData.ActualOrdinal;
            }
        }

        int IOrdinalMetaData.NamedOrdinal
        {
            get
            {
                return this.metaData.NamedOrdinal;
            }
        }

        string IOrdinalMetaData.SafeName
        {
            get
            {
                return this.metaData.SafeName;
            }
        }
    }
}
