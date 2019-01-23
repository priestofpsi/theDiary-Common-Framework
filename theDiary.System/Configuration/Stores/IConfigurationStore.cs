using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration.Stores
{
    
    

    public interface IConfigurationStore
    {
        IConfigurationSection Root
        {
            get;
        }

        IConfigurationSectionHandler GetHandler(string section);

        bool HasValue(string name);

        void WriteValue(string name, object value);

        object ReadValue(string name);

        object ReadValue(string name, object defaultValue);
    }
}
