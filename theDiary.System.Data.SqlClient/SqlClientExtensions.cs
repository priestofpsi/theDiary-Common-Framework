using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.SqlClient
{
    public static class SqlClientExtensions
    {
        private static string GetConnectionstring()
        {
            return GetConnectionstring("DefaultConnection");
        }

        private static string GetConnectionstring(string connectionStringName)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
        }

        /*  
        public static void Execute<T>(this T instance, string commandText, CommandType commandType)
            where T : ISerializable
        {
            using (SqlClient.SqlConnection connection = new SqlConnection(GetConnectionstring()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;
                typeof(T).GetProperties().Where(property => property.CanRead && property.CanWrite).ForEach(property => command.Parameters.AddWithValue(property.Name, property.GetValue(instance)));
            }  
        }

        public static void Execute<T>(this T instance, string commandText, CommandType commandType, params string[] properties)
            where T : ISerializable
        {
            using (SqlClient.SqlConnection connection = new SqlConnection(GetConnectionstring()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;
                typeof(T).GetProperties().Where(property => property.CanRead 
                    && property.CanWrite 
                    && properties.Contains(property.Name)).ForEach(property => command.Parameters.AddWithValue(property.Name, property.GetValue(instance)));
                
                command.ExecuteNonQuery();
            }
        }

        public static void ExecuteRead<T>(this T instance, string commandText, CommandType commandType, params string[] properties)
            where T : ISerializable
        {
            using (SqlClient.SqlConnection connection = new SqlConnection(GetConnectionstring()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;
                typeof(T).GetProperties().Where(property => property.CanRead
                    && property.CanWrite
                    && properties.Contains(property.Name)).ForEach(property => command.Parameters.AddWithValue(property.Name, property.GetValue(instance)));
                SqlDataReader reader = command.ExecuteReader();
                if (!reader.Read())
                    return;

                typeof(T).GetProperties().Where(property => property.CanRead
                        && property.CanWrite).ForEach(property => property.SetValue(instance, reader[property.Name]));
            }
        }

        public static IEnumerable<T> ExecuteReader<T>(this T instance, string commandText, CommandType commandType, params string[] properties)
        {
            using (SqlClient.SqlConnection connection = new SqlConnection(GetConnectionstring()))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;
                typeof(T).GetProperties().Where(property => property.CanRead
                    && property.CanWrite
                    && properties.Contains(property.Name)).ForEach(property => command.Parameters.AddWithValue(property.Name, property.GetValue(instance)));
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    T returnValue = System.Activator.CreateInstance<T>();
                    typeof(T).GetProperties().Where(property => property.CanRead
                        && property.CanWrite).ForEach(property => property.SetValue(returnValue, reader[property.Name]));
                    yield return returnValue;
                }
            }
        }
        */

        private static IEnumerable<T> ProcessReaderEnum<T>(this IDataReader reader)
        {
            while (reader.Read())
                yield return reader.ProcessReaderRow<T>();
        }
        public static T[] ProcessReader<T>(this IDataReader reader)
        {
            return reader.ProcessReaderEnum<T>().ToArray();
        }

        public static T ProcessReaderRow<T>(this IDataReader reader)
        {
            dynamic instance = System.Activator.CreateInstance<T>();
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.FlattenHierarchy);

            properties.Where(property => property.CanWrite).ForEach(property =>
                {
                    if (reader.ContainsField(property.Name))
                        property.SetValue(instance, reader[property.Name].FromSqlValue());
                });

            return instance;
        }

        public static IEnumerable<SqlParameter> AsEnumerable(this SqlParameterCollection parameters)
        {
            if (parameters.IsNotNull())
                for (int index = 0; index < parameters.Count; index++)
                    yield return parameters[index];
        }

        public static SqlCommand CreateCommand(this SqlConnection connection, string commandText)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            SqlCommand returnValue = connection.CreateCommand();
            returnValue.CommandText = commandText;

            return returnValue;
        }

        public static SqlParameter SetOutputParameter(this SqlCommand command, string parameterName, SqlDbType dbType)
        {
            SqlParameter returnValue = null;
            if (!parameterName.StartsWith("@"))
                parameterName = string.Format("@{0}", parameterName);
            switch (command.Parameters.Contains(parameterName))
            {
                case true:
                    returnValue = command.Parameters[parameterName];
                    break;
                case false:
                    returnValue = new SqlParameter();
                    returnValue.ParameterName = parameterName;
                    command.Parameters.Add(returnValue);
                    break;
            }
            returnValue.SqlDbType = dbType;
            returnValue.Direction = ParameterDirection.InputOutput;
            return returnValue;
        }
        public static SqlParameter SetParameter(this SqlCommand command, string parameterName, object value)
        {
            return command.SetParameter(parameterName, value.IsDefault() ? null : (object)value, ParameterDirection.Input, null);
        }
        public static SqlParameter SetParameter(this SqlCommand command, string parameterName, object value, ParameterDirection direction)
        {
            return command.SetParameter(parameterName, value, direction, null);
        }
        public static SqlParameter SetParameter(this SqlCommand command, string parameterName, object value, ParameterDirection direction, SqlDbType? valueType)
        {
            SqlParameter returnValue = null;
            value = value.ToSqlValue();
            if (!parameterName.StartsWith("@"))
                parameterName = string.Format("@{0}", parameterName);
            switch (command.Parameters.Contains(parameterName))
            {
                case true:
                    returnValue = command.Parameters[parameterName];
                    if (returnValue.Value != value)
                        returnValue.Value = value;
                    break;
                case false:
                    returnValue = new SqlParameter();
                    returnValue.ParameterName = parameterName;
                    returnValue.Value = value;
                    command.Parameters.Add(returnValue);
                    break;
            }
            if (valueType != null)
                returnValue.SqlDbType = valueType.Value;
            returnValue.Direction = direction;
            return returnValue;
        }

        public static void SetParameters(this SqlCommand command, dynamic values)
        {
            if (values == null)
                throw new ArgumentNullException("values");
            IEnumerable<PropertyInfo> properties = ((Type) values.GetType()).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.FlattenHierarchy).Where(property => property.CanUsePropertyToSave());
            properties.ForEach(property =>
            {
                object value = property.GetValue(values, null);
                SqlDbType? valType = null;
                ParameterDirection direction = ParameterDirection.Input;
                if (property.Name.StartsWith("_"))
                    direction = ParameterDirection.InputOutput;

                command.SetParameter(property.Name, value, direction, valType);
            });
        }

        public static void SetParameters<T>(this SqlCommand command, T instance)
        {
            IEnumerable<PropertyInfo> properties = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.FlattenHierarchy).Where(property => property.CanUsePropertyToSave());
            properties.ForEach(property =>
                {   
                    object value = property.GetValue(instance, null);
                    SqlDbType? valType = null;
                    ParameterDirection direction = ParameterDirection.Input;
                    if ((property.PropertyType == typeof(Guid) 
                        || property.PropertyType == typeof(Guid?)) 
                            && (value == null 
                            || value.Equals(Guid.Empty)))
                    {
                        valType = SqlDbType.UniqueIdentifier;
                        direction = ParameterDirection.InputOutput;
                    }
                    command.SetParameter(property.Name, value, direction, valType);
                });
        }

        public static void SetParameterDirection(this SqlCommand command, string parameterName, ParameterDirection direction)
        {
            SqlParameter parameter = command.SetParameter(parameterName, null);
            parameter.Direction = direction;
        }

        public static object GetParameter(this SqlCommand command, string parameterName)
        {
            return command.GetParameter<object>(parameterName);
        }

        public static T GetParameter<T>(this SqlCommand command, string parameterName)
        {
            if (!parameterName.StartsWith("@"))
                parameterName = string.Format("@{0}", parameterName);
            T returnValue = default(T);
            object value = command.Parameters[parameterName].Value;
            if (value != DBNull.Value)
                returnValue = (T)value;

            return returnValue;
        }

        public static void SetReturnParameter(this SqlCommand command)
        {
            command.Parameters.Add(new SqlParameter()
            {
                ParameterName = "RETURN_VALUE",
                Direction = ParameterDirection.ReturnValue,
                SqlDbType = SqlDbType.Int
            });
        }

        public static int GetReturnParameter(this SqlCommand command)
        {
            return (int)command.Parameters["RETURN_VALUE"].Value;
        }

        public static void SetIncludeDeletedParameter(this SqlCommand command, bool includeDeleted)
        {
            if (includeDeleted)
                command.Parameters.Add(new SqlParameter("IncludeDeleted", System.Data.SqlDbType.Bit)
                {
                    Value = true
                });
        }

        #region IDatabaseAccessor Methods & Functions
        public static void RegisterConnectionString<T>(this T instance, string connectionString)
            where T : IDatabaseAccessor
        {
            instance.RegisterConnectionString<T>(connectionString, "dbo");
        }

        public static void RegisterConnectionString<T>(this T instance, string connectionString, string schemaName)
            where T : IDatabaseAccessor
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                throw new ArgumentNullException("connectionString");

            ConnectionInformation.RegisterConnectionInformation(typeof(T), connectionString, schemaName);
        }
        public static void ExecuteNonQuery(this IDatabaseAccessor instance, string commandText, dynamic parameterValues)
        {
            using (SqlConnection connection = instance.CreateConnection())
            {
                string outputValue = null;
                connection.Open();
                SqlCommand command = instance.CreateCommand(connection, commandText, CommandType.StoredProcedure);
                IEnumerable<PropertyInfo> properties = ((Type)parameterValues.GetType()).GetProperties().Where(property => property.CanRead);
                properties.ForEach(property =>
                {
                    object value = property.GetValue(parameterValues, null);
                    SqlDbType? valType = null;
                    ParameterDirection direction = ParameterDirection.Input;
                    string parameterName = property.Name;
                    if (parameterName.StartsWith("_"))
                    {
                        direction = ParameterDirection.InputOutput;
                        parameterName = parameterName.Substring(1);
                        outputValue = parameterName;
                    }
                    if (!parameterName.Equals("IncludeDeleted", StringComparison.OrdinalIgnoreCase))
                    {
                        command.SetParameter(parameterName, value, direction, valType);
                    }
                    else
                    {
                        command.SetIncludeDeletedParameter((bool)value);
                    }
                });

                var reader = command.ExecuteNonQuery();
            }
        }
        public static T ExecuteNonQuery<T>(this IDatabaseAccessor instance, string commandText, dynamic parameterValues)
        {
            T returnValue = default(T);
            using (SqlConnection connection = instance.CreateConnection())
            {
                string outputValue = null;
                connection.Open();
                SqlCommand command = instance.CreateCommand(connection, commandText, CommandType.StoredProcedure);
                IEnumerable<PropertyInfo> properties = ((Type)parameterValues.GetType()).GetProperties().Where(property => property.CanRead);
                properties.ForEach(property =>
                {
                    object value = property.GetValue(parameterValues, null);
                    SqlDbType? valType = null;
                    ParameterDirection direction = ParameterDirection.Input;
                    string parameterName = property.Name;
                    if (parameterName.StartsWith("_"))
                    {
                        direction = ParameterDirection.InputOutput;
                        parameterName = parameterName.Substring(1);
                        outputValue = parameterName;
                    }
                    if (!parameterName.Equals("IncludeDeleted", StringComparison.OrdinalIgnoreCase))
                    {
                        command.SetParameter(parameterName, value, direction, valType);
                    }
                    else
                    {
                        command.SetIncludeDeletedParameter((bool)value);
                    }
                });

                var reader = command.ExecuteNonQuery();
                if (outputValue != null)
                    returnValue = (T)command.GetParameter(outputValue);
            }
            return returnValue;
        }

        public static IEnumerable<T> ExecuteCommand<T>(this IDatabaseAccessor instance, string commandText, dynamic parameterValues)
        {
            IEnumerable<T> results = new T[] { };
            using (SqlConnection connection = instance.CreateConnection())
            {
                connection.Open();
                using (SqlCommand command = instance.CreateCommand(connection, commandText, CommandType.StoredProcedure))
                {
                    IEnumerable<PropertyInfo> properties = ((Type)parameterValues.GetType()).GetProperties().Where(property => property.CanRead);
                    properties.ForEach(property =>
                    {
                        object value = property.GetValue(parameterValues, null);
                        SqlDbType? valType = null;
                        ParameterDirection direction = ParameterDirection.Input;
                        string parameterName = property.Name;
                        if (parameterName.StartsWith("_"))
                        {
                            direction = ParameterDirection.InputOutput;
                            parameterName = parameterName.Substring(1);
                        }
                        if (!parameterName.Equals("IncludeDeleted", StringComparison.OrdinalIgnoreCase))
                        {
                            command.SetParameter(parameterName, value, direction, valType);
                        }
                        else
                        {
                            command.SetIncludeDeletedParameter((bool)value);
                        }
                    });

                    var reader = command.ExecuteReader();
                    results = reader.ProcessReader<T>();
                }
            }
            return results;
        }

        public static SqlCommand CreateCommand<T>(this T instance, string commandText)
            where T : IDatabaseAccessor
        {
            return instance.CreateCommand(commandText, CommandType.StoredProcedure);
        }

        public static SqlConnection CreateConnection<T>(this T instance)
        {
            var ci = ConnectionInformation.GetConnectionInformation(instance.GetType());
            var connection = new SqlConnection(ci.ConnectionString);
            

            return connection;
        }

        public static SqlCommand CreateCommand<T>(this T instance, SqlConnection connection, string commandText, CommandType commandType)
            where T : IDatabaseAccessor
        {
            var ci = ConnectionInformation.GetConnectionInformation(instance.GetType());
            if (connection == null)
                connection = instance.CreateConnection();
            if(connection.State != ConnectionState.Open)
                connection.Open();
            SqlCommand command = null;
            if (commandType == CommandType.StoredProcedure)
                command = connection.CreateCommand(commandText.AppendConnectionSchema(ci));
            if (command == null)
                command = connection.CreateCommand(commandText);
            command.CommandType = commandType;
            return command;
        }

        public static SqlCommand CreateCommand<T>(this T instance, string commandText, CommandType commandType)
            where T : IDatabaseAccessor
        {
            var ci = ConnectionInformation.GetConnectionInformation(instance.GetType());
            var connection = instance.CreateConnection();
            connection.Open();
            SqlCommand command = null;
            if (commandType == CommandType.StoredProcedure)
                command = connection.CreateCommand(commandText.AppendConnectionSchema(ci));
            if (command == null)
                command = connection.CreateCommand(commandText);
            command.CommandType = commandType;
            return command;
        }

        #endregion

        private static bool CanUsePropertyToSave(this PropertyInfo property)
        {
            return property.CanRead
                && property.CanWrite
                && !property.HasAttribute<System.ComponentModel.IgnoreAttribute>()
                && (property.PropertyType.IsSubclassOf(typeof(ValueType))
                    || property.PropertyType.IsSubclassOf(typeof(Nullable<>))
                    || property.PropertyType == typeof(string));
        }
        private static string AppendConnectionSchema(this string commandText, ConnectionInformation instance)
        {
            if (!commandText.StartsWith(string.Format("[{0}].[", instance.SchemaName), StringComparison.OrdinalIgnoreCase)
                && !commandText.StartsWith(string.Format("{0}.", instance.SchemaName), StringComparison.OrdinalIgnoreCase))
                return string.Format("[{0}].{1}", instance.SchemaName, commandText.StartsWith("[") ? commandText : string.Format("[{0}]", commandText));

            return commandText;
        }

        private static string AppendConnectionSchema(this string commandText, string schemaName)
        {
            if (!commandText.StartsWith(string.Format("[{0}].[", schemaName), StringComparison.OrdinalIgnoreCase)
                && !commandText.StartsWith(string.Format("{0}.", schemaName), StringComparison.OrdinalIgnoreCase))
                return string.Format("[{0}].{1}", schemaName, commandText.StartsWith("[") ? commandText : string.Format("[{0}]", commandText));

            return commandText;
        }

        private static object ToSqlValue(this object value)
        {
            if (value == null
                || (value.GetType() == typeof(Guid) && Guid.Empty.Equals(((Guid)value)))
                || (value.GetType() == typeof(string) && ((string)value).IsNullEmptyOrWhiteSpace()))
                return DBNull.Value;

            if (value.GetType().IsEnum)
                value = EnumHelper.ToUnderlyingType(value);

            return value;
        }

        private static object FromSqlValue(this object value)
        {
            if (value == DBNull.Value)
                return null;

            return value;
        }

        private static T FromSqlValue<T>(this object value)
        {
            if (value == DBNull.Value)
                return default(T);

            return (T)value;
        }

        private static bool ContainsField(this IDataReader reader, string fieldName)
        {
            for (int i = 0; i < reader.FieldCount; i++)
                if (reader.GetName(i).Equals(fieldName, StringComparison.OrdinalIgnoreCase))
                    return true;

            return false;
        }
    }
}
