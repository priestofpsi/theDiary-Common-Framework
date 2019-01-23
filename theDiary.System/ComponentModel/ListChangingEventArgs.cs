using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public class ListChangingEventArgs
        : EventArgs
    {
        #region Constructors
        public ListChangingEventArgs(ListChangingTypes listChangingType, object item)
            : base()
        {
            this.ListChangingType = listChangingType;
            this.Item = item;
        }

        public ListChangingEventArgs(ListChangingTypes listChangingType, IEnumerable<object> items)
        {
            this.ListChangingType = listChangingType;
            this.Items = items;
        }

        protected ListChangingEventArgs()
            : base()
        {
        }
        #endregion

        #region Public Read-Only Properties
        public object Item
        {
            get
            {
                return this.Items.FirstOrDefault();
            }
            protected set
            {
                this.Items = value.MakeArray();
            }
        }

        public IEnumerable<object> Items { get; protected set; }

        public ListChangingTypes ListChangingType { get; protected set; }
        #endregion
    }
}
