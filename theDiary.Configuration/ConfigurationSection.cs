using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration
{
    public abstract class ConfigurationSection<TElement>
        : System.Configuration.ConfigurationSection
        where TElement : ConfigurationElementCollection<TElement>
    {
    }
}
