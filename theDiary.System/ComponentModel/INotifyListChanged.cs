using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    /// <summary>
    /// Notifies clients that a list has changed.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public interface INotifyListChanged<T>
        : IList<T>
    {
        /// <summary>
        /// Occurs when a the list has changed.
        /// </summary>
        event ListChangedEventHandler<T> ListChanged;
    }
}
