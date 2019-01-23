using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    public class ListChangedEventArgs<T>
        : EventArgs
    {
        #region Constructors
        public ListChangedEventArgs(ListChangedTypes listChangedType, T item)
            : base()
        {
            this.ListChangedType = listChangedType;
            this.Item = item;
        }

        public ListChangedEventArgs(ListChangedTypes listChangedType, IEnumerable<T> items)
            : base()
        {
            this.ListChangedType = listChangedType;
            this.Items = items;
        }
        #endregion

        #region Public Read-Only Properties
        public T Item
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

        public IEnumerable<T> Items { get; protected set; }

        public ListChangedTypes ListChangedType { get; private set; }
        #endregion
    }
}
