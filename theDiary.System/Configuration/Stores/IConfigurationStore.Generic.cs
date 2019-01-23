using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration.Stores
{
    public interface IConfigurationStore<THandler>
        where THandler : IConfigurationSectionHandler
    {
        IConfigurationSection<THandler> Root
        {
            get;
        }
        
        THandler GetHandler(string section);

        bool HasValue<TIn>(string name);

        void WriteValue<TIn>(string name, TIn value);

        TIn ReadValue<TIn>(string name);

        TIn ReadValue<TIn>(string name, TIn defaultValue);
    }
}
