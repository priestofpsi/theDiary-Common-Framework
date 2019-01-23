using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Client.Configuration
{
    public interface IConfigurationWrapper
    {
        TOut ReadConfiguration<TOut, TIn>(string configurationName, TIn defaultValue = default(TIn));

        T ReadConfiguration<T>(string configurationName, T defaultValue = default(T));

        void SaveConfiguration<T>(string configurationName, T value, bool setConfigurationInstance = true);
    }
}
