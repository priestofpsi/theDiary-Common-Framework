using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration.Stores
{
    public interface IConfigurationSection<T>
        : IConfigurationSection 
    {
        new IConfigurationSectionHandler<T> GetHandler();
    }
}
