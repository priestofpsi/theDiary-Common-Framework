using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration.Stores
{
    public interface IConfigurationSectionHandler
    {
        object Create(string relativePath);

        object Create();

        bool Exists(string relativePath);

        bool Exists();

        object Open(string relativePath);

        object Open();

        void Close(string relativePath);

        void Close();
    }
}
