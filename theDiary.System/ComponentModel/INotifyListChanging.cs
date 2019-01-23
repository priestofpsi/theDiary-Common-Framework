using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    /// <summary>
    /// Notifies clients that a list is changing.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public interface INotifyListChanging<T>
        : IList<T>
    {    
        /// <summary>
        /// Occurs when a the list is changing.
        /// </summary>
        event ListChangingEventHandler<T> ListChanging;
    }
}
