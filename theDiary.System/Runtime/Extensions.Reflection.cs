using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System.Casting
{
    public static partial class ReflectionCastExtensions
    {
        public static T As<T>(this object instance)
        {
            if (instance.IsNull())
                return default(T);
            return (T) instance;
        }
    }
}

namespace System.Reflection
{
    public static partial class ReflectionExtensions
    {
        #region Private Static Declarations
        private static Dictionary<Type, Dictionary<string, System.Reflection.PropertyInfo>> typeProperties = new Dictionary<Type, Dictionary<string, Reflection.PropertyInfo>>();
        private static Dictionary<Type, Dictionary<string, Dictionary<Type, Exception>>> typePropertyExceptions = new Dictionary<Type, Dictionary<string, Dictionary<Type, Exception>>>();
        #endregion

        #region Public Extension Methods & Functions
        public static T As<T>(this object instance)
        {
            if (instance.IsNull())
                return default(T);
            return (T) instance;
        }
        #region PropertyInfo Methods & Functions
        public static bool HasProperty(this Type instanceType, string propertyName)
        {
            System.Reflection.PropertyInfo property;
            return instanceType.TryGetProperty(propertyName, out property);
        }

        public static bool TryGetProperty(this Type instanceType, string propertyName, out System.Reflection.PropertyInfo property)
        {
            ReflectionExtensions.InitializeTypeProperty(instanceType, propertyName, out property);

            return (property != null);
        }

        public static bool HasProperty(this object instance, string propertyName)
        {
            if (instance == null)
                throw new ArgumentNullException("instance");
            System.Reflection.PropertyInfo property;
            ReflectionExtensions.InitializeTypeProperty(instance.GetType(), propertyName, out property);

            return (property != null);
        }

        public static T GetValue<T>(this object instance, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");

            if (instance == null)
                throw new ArgumentNullException("instance");

            Type instanceType = instance.GetType();
            Type returnType = typeof(T);

            return instance.GetValue<T>(propertyName, instanceType, returnType);
        }

        public static TOut GetValue<TOut, TIn>(this TIn instance, string propertyName)
            where TIn : class
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException("propertyName");

            if (instance == null)
                throw new ArgumentNullException("instance");

            Type instanceType = typeof(TIn);
            Type returnType = typeof(TOut);

            return instance.GetValue<TOut>(propertyName, instanceType, returnType);
        }

        public static void SetValue<T>(this T instance, string propertyName, object value)
        {
            System.Reflection.PropertyInfo property;
            if (!TryGetProperty(typeof(T), propertyName, out property))
                throw new ArgumentException(string.Format("Invalid Property Name : {0}", propertyName), "propertyName");

            Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
            object parsedValue = Convert.ChangeType(value, type);
            property.SetValue(instance, parsedValue, null);
        }

        public static T GetValue<T>(this PropertyInfo property, object value)
        {
            return (T) property.GetValue(value);
        }
        
        public static void CopyValue<T>(this PropertyInfo property, T source, T destination)
        {
            property.SetValue(destination, property.GetValue(source));
        }

        public static dynamic GetDefaultValue(this PropertyInfo property)
        {
            System.ComponentModel.DefaultValueAttribute defaultValue = property.GetAttribute<System.ComponentModel.DefaultValueAttribute>();

            if (defaultValue != null)
                return defaultValue.Value;

            return property.PropertyType.GetDefaultValue();
        }

        public static bool HasDefaultProperty(this PropertyInfo property)
        {
            return property.GetDefaultProperty() != null;
        }

        public static PropertyInfo GetDefaultProperty(this PropertyInfo property)
        {
            if (property.PropertyType.HasAttribute<DefaultPropertyAttribute>())
            {
                DefaultPropertyAttribute attribute = property.PropertyType.GetAttribute<DefaultPropertyAttribute>();
                return property.PropertyType.GetProperty(attribute.Name);
            }

            return null;
        }
        #endregion

        #region InterfaceType Methods & Functions
        public static IEnumerable<Type> GetInterfaceTypes<T>(this Assembly assembly)
        {
            return typeof(T).GetInterfaceTypes(assembly.MakeArray());
        }

        public static IEnumerable<Type> GetInterfaceTypes<T>(this Assembly assembly, Func<Type, bool> typePredicate)
        {
            return typeof(T).GetInterfaceTypes(assembly.MakeArray(), typePredicate);
        }

        public static IEnumerable<Type> GetInterfaceTypes(this Type type)
        {
            return type.GetInterfaceTypes(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static IEnumerable<Type> GetInterfaceTypes(this Type type, Func<Type, bool> typePredicate)
        {
            return type.GetInterfaceTypes(AppDomain.CurrentDomain.GetAssemblies(), typePredicate);
        }

        public static IEnumerable<Type> GetInterfaceTypes(this Type type, IEnumerable<Assembly> assemblies)
        {
            return type.GetInterfaceTypes(assemblies, tp => true);
        }

        public static IEnumerable<Type> GetInterfaceTypes(this Type type, IEnumerable<Assembly> assemblies, Func<Type, bool> typePredicate)
        {
            if (!type.IsInterface)
                throw new ArgumentException("The specified type is not an interface.", "type");
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");
            if (typePredicate == null)
                throw new ArgumentNullException("typePredicate");

            return assemblies.SelectMany(s => s.GetTypes()).Where(p => type.IsAssignableFrom(p)
                && !p.IsInterface
                && !p.IsAbstract
                && (typePredicate != null 
                    && typePredicate(p)));
        }
        #endregion

        public static bool IsDefaultValue<T>(this T value)
        {
            return object.Equals(value, default(T));
        }

        public static bool IsDefaultValue(this object value, Type type)
        {
            return object.Equals(value, type.GetDefaultValue());
        }
        #endregion        

        #region Private Methods & Functions
        private static T GetValue<T>(this object instance, string propertyName, Type instanceType, Type returnType)
        {
            return (T)instanceType.GetProperty(propertyName).GetValue(instance, null);
            /*
            Exception exception = null;
            System.Reflection.PropertyInfo property;
            SystemExtensions.InitializeTypeProperty(instanceType, propertyName, out property);
            SystemExtensions.InitializeTypePropertyExceptions(instanceType, returnType, propertyName, property, out exception);

            //typeProperties[instanceType].Add(propertyName, property);
            if (exception != null)
                throw exception;

            return (T)property.GetValue(instance, null);
            */
        }

        private static bool HasPropertyException(Type instanceType, Type returnType, string propertyName, out Exception exception)
        {
            exception = typePropertyExceptions[instanceType][propertyName][returnType];

            return (exception != null);
        }

        private static void InitializeTypeProperty(Type instanceType, string propertyName, out System.Reflection.PropertyInfo property)
        {
            if (!typeProperties.ContainsKey(instanceType))
                typeProperties.Add(instanceType, new Dictionary<string, Reflection.PropertyInfo>());

            if (!typeProperties[instanceType].ContainsKey(propertyName))
                typeProperties[instanceType].Add(propertyName, instanceType.GetProperty(propertyName));

            property = typeProperties[instanceType][propertyName];
        }

        private static void InitializeTypePropertyExceptions(Type instanceType, Type returnType, string propertyName, System.Reflection.PropertyInfo property, out Exception exception)
        {
            if (!typePropertyExceptions.ContainsKey(instanceType))
                typePropertyExceptions.Add(instanceType, new Dictionary<string, Dictionary<Type, Exception>>());

            if (!typePropertyExceptions[instanceType].ContainsKey(propertyName))
                typePropertyExceptions[instanceType].Add(propertyName, new Dictionary<Type, Exception>());

            if (typePropertyExceptions[instanceType][propertyName].ContainsKey(returnType))
            {
                ReflectionExtensions.HasPropertyException(instanceType, returnType, propertyName, out exception);
            }
            else
            {
                exception = null;
                if (property == null)
                    exception = new MissingFieldException(instanceType.Name, propertyName);

                if (property.PropertyType != returnType
                    || !property.PropertyType.IsSubclassOf(returnType))
                    exception = new InvalidCastException();

                typePropertyExceptions[instanceType][propertyName].Add(returnType, exception);
            }
        }
        #endregion        


    }
}
