using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
    internal class ConnectionInformation
    {
        internal ConnectionInformation(string connectionString, string schemaName)
        {
            this.SchemaName = schemaName;
            this.ConnectionString = connectionString;
        }

        internal ConnectionInformation(System.Configuration.ConnectionStringSettings connectionConfiguration, string schemaName)
            : this(connectionConfiguration.ConnectionString, schemaName)
        {
            this.ConfigurationName = connectionConfiguration.Name;
            this.SchemaName = schemaName;
        }

        #region Public Properties
        public string SchemaName { get; private set; }

        public string ConnectionString { get; private set; }

        public string ConfigurationName { get; private set; }
        #endregion

        #region Private Static Declarations
        private readonly static Dictionary<Type, ConnectionInformation> registeredConnections = new Dictionary<Type, ConnectionInformation>();
        private readonly static object syncObject = new object();
        #endregion

        #region Private Static Read-Only Properties
        private static Dictionary<Type, ConnectionInformation> RegisteredConnections
        {
            get
            {
                lock (ConnectionInformation.syncObject)
                    return ConnectionInformation.registeredConnections;
            }
        }
        #endregion

        #region Public Static Methods & Functions
        public static ConnectionInformation GetConnectionInformation(Type registeredType)
        {
            if (ConnectionInformation.RegisteredConnections.ContainsKey(registeredType))
                return ConnectionInformation.RegisteredConnections[registeredType];

            return null;
        }

        public static void RegisterConnectionInformation(Type registeredType, string connectionString)
        {
            ConnectionInformation.RegisterConnectionInformation(registeredType, connectionString, "dbo");
        }

        public static void RegisterConnectionInformation(Type registeredType, string connectionString, string schema)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            ConnectionInformation ci;
            if (ConnectionInformation.IsConfigurationName(connectionString))
            {
                ci = new ConnectionInformation(System.Configuration.ConfigurationManager.ConnectionStrings[connectionString], schema);
            }
            else if (ConnectionInformation.IsConnectionString(connectionString))
            {
                ci = new ConnectionInformation(connectionString, schema);
            }
            else
            {
                throw new ArgumentException("connectionString");
            }
            ConnectionInformation.RegisterConnectionInformation(registeredType, ci);
        }

        public static void RegisterConnectionInformation(Type registeredType, ConnectionInformation connectionInformation)
        {
            if (!ConnectionInformation.RegisteredConnections.ContainsKey(registeredType))
                ConnectionInformation.RegisteredConnections.Add(registeredType, connectionInformation);
        }
        #endregion

        #region Private Static Methods & Functions
        private static bool IsConnectionString(string connectionString)
        {
            try
            {
                SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder(connectionString);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private static bool IsConfigurationName(string connectionName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionName] != null;
        }
        #endregion
    }
}
