using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration.Stores
{
    public interface IConfigurationSectionHandler<T>
        : IConfigurationSectionHandler
    {
        new T Create(string relativePath);

        new T Create();
        
        new T Open(string relativePath);

        new T Open();
    }
}
