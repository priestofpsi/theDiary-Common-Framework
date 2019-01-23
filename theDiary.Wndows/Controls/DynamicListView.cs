using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Controls
{
    public class DynamicListView
        : ListView, 
        INotifyPropertyChanging, INotifyPropertyChanged
    {
        public DynamicListView()
            : base()
        {
        }

        private IEnumerable dataSource;
        private event PropertyChangingEventHandler propertyChanging;
        private event PropertyChangedEventHandler propertyChanged;

        public IEnumerable DataSource
        {
            get
            {
                return this.dataSource;
            }
            set
            {
                if (Object.Equals(this.dataSource, value))
                    return;
                
                if (this.propertyChanging != null)
                    this.propertyChanging(this, new PropertyChangingEventArgs("DataSource"));

                this.dataSource = value;

                if (this.propertyChanged != null)
                    this.propertyChanged(this, new PropertyChangedEventArgs("DataSource"));
            }
        }

        #region INofityProperty Implementation
        event PropertyChangingEventHandler INotifyPropertyChanging.PropertyChanging
        {
            add 
            {
                this.propertyChanging += value;
            }
            remove
            {
                this.propertyChanging -= value;
            }
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
        #endregion

        private void PopulateFromDataSource()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(this.PopulateFromDataSource));
                return;
            }

            try
            {
                this.BeginUpdate();
                var iterator = this.DataSource.GetEnumerator();
                UniqueList<Type> types = new UniqueList<Type>();
                while(iterator.MoveNext())
                {
                    if (iterator.Current.IsNull())
                        continue;

                    types.TryAdd(iterator.Current.GetType());
                }
                this.PopulateColumns(this.GetValidProperties(types));
            }
            catch
            {
            }
            finally
            {
                this.EndUpdate();
            }
        }


        private void PopulateColumns(IEnumerable<PropertyInfoMetaData> properties)
        {            
            this.Columns.Clear();
            properties.ForEachAsParallel(property => this.Columns.Add(new DynamicColumnHeader(property)));
        }
        
    }


}
