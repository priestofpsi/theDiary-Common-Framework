using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    /// <summary>
    /// Represents the method that will handle the <see cref="INotifyListChanging"/>.<value>PropertyChanging</value> event of an <see cref="INotifyListChanging"/> interface.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="ListChangingEventArgs"/> that contains the event data.</param>
    public delegate void ListChangingEventHandler(IList sender, ListChangingEventArgs e);

    /// <summary>
    /// Represents the method that will handle the <see cref="T:INotifyListChanging"/>.<value>PropertyChanging</value> event of an <see cref="T:INotifyListChanging"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">A <see cref="T:ListChangingEventArgs"/> that contains the event data.</param>
    public delegate void ListChangingEventHandler<T>(IList<T> sender, ListChangingEventArgs<T> e);
}
