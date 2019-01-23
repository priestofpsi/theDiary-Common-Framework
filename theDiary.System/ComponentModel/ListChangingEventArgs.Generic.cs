using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public class ListChangingEventArgs<T>
        : ListChangingEventArgs
    {
        #region Constructors
        public ListChangingEventArgs(ListChangingTypes listChangingType, T item)
            : base()
        {
            this.Item = item;
            this.ListChangingType = listChangingType;
        }

        public ListChangingEventArgs(ListChangingTypes listChangingType, IEnumerable<T> items)
            : base()
        {
            this.Items = items;
            this.ListChangingType = listChangingType;
        }
        #endregion

        #region Public Read-Only Properties
        public new T Item
        {
            get
            {
                return this.Items.FirstOrDefault();
            }
            private set
            {
                this.Items = value.MakeArray();
            }
        }

        public new IEnumerable<T> Items { get; protected set; }
        #endregion
    }
}
