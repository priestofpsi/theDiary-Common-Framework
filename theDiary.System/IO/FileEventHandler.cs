using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    /// <summary>
    /// Represents the method that will handle events relating to a files.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The details of the file that raised the event.</param>
    public delegate void FileEventHandler(object sender, FileEventArgs e);
}
