using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Configuration.Stores
{
    public interface IConfigurationSection
    {
        /// <summary>
        /// Gets the name identifying the current configuration section.
        /// </summary>
        string Name
        {
            get;
        }

        IConfigurationSectionHandler GetHandler();
    }
}
