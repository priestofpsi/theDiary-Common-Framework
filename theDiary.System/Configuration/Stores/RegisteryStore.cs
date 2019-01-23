using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace System.Configuration.Stores
{
    public sealed class RegistryHandler
        : IConfigurationSectionHandler<RegistryKey>
    {
        public string Root
        {
            get;
            private set;
        }

        public RegisteryStoreLocation Registry
        {
            get;
            private set;
        }

        public Microsoft.Win32.RegistryKey RegisteryRoot
        {
            get
            {
                switch (this.Registry)
                {
                    case RegisteryStoreLocation.ClassesRoot:
                        return Microsoft.Win32.Registry.ClassesRoot;
                    case RegisteryStoreLocation.LocalMachine:
                        return Microsoft.Win32.Registry.ClassesRoot;
                    case RegisteryStoreLocation.CurrentConfig:
                        return Microsoft.Win32.Registry.ClassesRoot;
                    default:
                        return Microsoft.Win32.Registry.CurrentUser;
                }
            }
        }
        object IConfigurationSectionHandler.Create()
        {
            return this.As<IConfigurationSectionHandler<RegistryKey>>().Create();
        }

        RegistryKey IConfigurationSectionHandler<RegistryKey>.Create()
        {
            
        }

        public RegistryKey Create(string relativePath)
        {
            return this.RegisteryRoot.CreateSubKey(relativePath);
        }

        bool IConfigurationSectionHandler<RegistryKey>.Exists()
        {
            
        }

        public bool Exists(string relativePath)
        {
            throw new NotImplementedException();
        }

        RegistryKey IConfigurationSectionHandler<RegistryKey>.Open()
        {
            throw new NotImplementedException();
        }

        public RegistryKey Open(string relativePath)
        {
            throw new NotImplementedException();
        }
    }

    public sealed class RegisteryStore
        : IConfigurationStore<RegistryHandler>
    {
        IConfigurationSection<T> IConfigurationStore<RegistryHandler>.Root
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        RegistryHandler IConfigurationStore<RegistryHandler>.GetHandler(string section)
        {
            throw new NotImplementedException();
        }

        bool IConfigurationStore<RegistryHandler>.HasValue<TIn>(string name)
        {
            throw new NotImplementedException();
        }

        TIn IConfigurationStore<RegistryHandler>.ReadValue<TIn>(string name)
        {
            throw new NotImplementedException();
        }

        TIn IConfigurationStore<RegistryHandler>.ReadValue<TIn>(string name, TIn defaultValue)
        {
            throw new NotImplementedException();
        }

        void IConfigurationStore<RegistryHandler>.WriteValue<TIn>(string name, T value)
        {
            throw new NotImplementedException();
        }
    }
}
