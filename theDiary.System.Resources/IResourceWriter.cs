using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Resources
{
    public interface IResourceSourceController
        : IDisposable
    {
        #region Method & Function Declarations
        Stream OpenResourceStream(Type modelType, CultureInfo culture);

        void CloseResourceStream();

        void WriteResourceValue(string resourceKey, string resourceValue);

        void RemoveResourceValue(string resourceKey);

        string ReadResourceValue(string resourceKey);

        bool ResourceExists(string resourceKey);

        IEnumerable<string> GetResourceKeys();
        #endregion
    }
}