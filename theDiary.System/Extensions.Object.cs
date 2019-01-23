using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static partial class SystemExtensions
    {
        #region IsAny Extension Methods & Functions
        public static bool IsAny<T>(this T value, params T[] contains)
        {
            foreach (T compareValue in contains)
                if (Object.Equals(value, compareValue))
                    return true;

            return false;
        }

        public static bool IsAny<T>(this T value, out T matchedValue, params T[] contains)
        {
            matchedValue = default(T);
            foreach (T val in contains)
            {
                if (object.Equals(value, val))
                {
                    matchedValue = val;
                    return true;
                }
            }
            return false;
        }
        #endregion
        
        #region Instance Validation Extension Methods & Functions
        public static bool IsNullOrNotType<T>(this object instance)
        {
            return instance.IsNullOrNotTypes(typeof(T));
        }

        public static bool IsEmpty(this IntPtr pointer)
        {
            return pointer.Equals(IntPtr.Zero);
        }

        public static bool IsNotEmpty(this IntPtr pointer)
        {
            return !pointer.Equals(IntPtr.Zero);
        }

        public static bool IsNullOrNotTypes(this object instance, params Type[] types)
        {
            if (instance.IsNull())
                return true;

            if (types.IsNullOrEmpty())
                throw new ArgumentNullException("types");
            Type instanceType = instance.GetType();
            foreach (Type type in types)
                if (instanceType == type
                    || ((type.IsGenericTypeDefinition && instanceType.IsGenericType(type))
                    ||  instanceType.IsSubclassOf(type)))
                    return false;

            return true;
        }

        /// <summary>
        /// Determines if the <paramref name="instance"/> is <c>Null</c>.
        /// </summary>
        /// <param name="instance">The <see cref="Object"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="instance"/> is <c>Null</c>; otherwise <c>False</c>.</returns>
        public static bool IsNull(this object instance)
        {
            return Object.Equals(null, instance);
        }

        /// <summary>
        /// Determines if the <paramref name="instance"/> is not <c>Null</c>.
        /// </summary>
        /// <param name="instance">The <see cref="Object"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="instance"/> is not <c>Null</c>; otherwise <c>False</c>.</returns>
        public static bool IsNotNull(this object instance)
        {
            return !Object.Equals(null, instance);
        }

        public static bool IsNullOrDefault<T>(this T instance)
        {
            return Object.Equals(typeof(T).GetDefaultValue(), instance);
        }

        public static bool IsNotNullOrDefault<T>(this T instance)
        {
            return !Object.Equals(typeof(T).GetDefaultValue(), instance);
        }
        #endregion

        #region Default Value Extension Methods & Functions
        public static bool IsDefault(this object instance)
        {
            if (instance == null 
                || (instance.GetType() == typeof(string) 
                    && string.IsNullOrEmpty((string) instance)))                
                return true;

            if (instance.GetType().IsSubclassOf(typeof(ValueType)))
                return ((ValueType)instance).IsDefault();

            return false;
        }

        /// <summary>
        /// Determines if the <paramref name="instance"/> is the default value for the <see cref="ValueType"/>.
        /// </summary>
        /// <param name="instance">The <see cref="ValueType"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="instance"/> is equal to the default value; otherwise <c>False</c>.</returns>
        public static bool IsDefault(this ValueType instance)
        {
            return Object.Equals(instance.GetType().GetDefaultValue(), instance);
        }

        /// <summary>
        /// Determines if the <paramref name="instance"/> is not the default value for the <see cref="ValueType"/>.
        /// </summary>
        /// <param name="instance">The <see cref="ValueType"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="instance"/> is not equal to the default value; otherwise <c>False</c>.</returns>
        public static bool IsNotDefault(this ValueType instance)
        {
            return !Object.Equals(instance.GetType().GetDefaultValue(), instance);
        }

        /// <summary>
        /// Gets the default value for a specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to check.</param>
        /// <returns>The default value for the specified <paramref name="type"/>.</returns>
        public static dynamic GetDefaultValue(this Type type)
        {
            if (type == typeof(string))
                return string.Empty;

            if (type.IsClass)
                return null;

            return System.Activator.CreateInstance(type);
        }

        /// <summary>
        /// Gets the default value for a specified <see cref="Type"/>.
        /// </summary>
        /// <param name="value">The instance to check.</param>
        /// <typeparam name="T">The <see cref="Type"/> to check.</typeparam>
        /// <returns>The default value for the specified <typeparamref name="T"/>.</returns>
        public static T GetDefaultValue<T>(this T value)
        {
            return (T)typeof(T).GetDefaultValue();
        }
        #endregion

        #region Type Extensions Methods & Functions
        public static bool IsTextType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Char:
                case TypeCode.String:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsBooleanType(this Type type)
        {
            return Type.GetTypeCode(type) == TypeCode.Boolean;
        }

        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Determines if a specified <see cref="Type"/> is a derived from a the Generic of <see cref="Type"/>.
        /// </summary>
        /// <typeparam name="TGeneric">The generic <see cref="Type"/> to compare to.</typeparam>
        /// <param name="type">The <see cref="Type"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="type"/> is a generic of <typeparamref name="TGeneric"/>; otherwise <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="type"/> or <paramref name="type"/> is <c>Null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the <paramref name="type"/> is not a generic <see cref="Type"/>.</exception>
        public static bool IsGenericType<TGeneric>(this Type type)
        {
            return type.IsGenericType(typeof(TGeneric));
        }

        /// <summary>
        /// Determines if a specified <see cref="Type"/> iis a derived from a the Generic of <paramref name="genericType"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to check.</param>
        /// <param name="genericType">The generic <see cref="Type"/> to compare to.</param>
        /// <returns><c>True</c> if the <paramref name="type"/> is a generic of <paramref name="genericType"/>; otherwise <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="type"/> or <paramref name="genericType"/> is <c>Null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">thrown if the <paramref name="genericType"/> is not a generic <see cref="Type"/>.</exception>
        public static bool IsGenericType(this Type type, Type genericType)
        {
            if (type.IsNull())
                throw new ArgumentNullException("type");

            if (genericType.IsNull())
                throw new ArgumentNullException("genericType");

            if (!genericType.IsGenericType || !genericType.IsGenericTypeDefinition)
                throw new ArgumentOutOfRangeException("genericType", "The specified type is not a generic type.");

            return (type.IsGenericType
                && type.GetGenericTypeDefinition() == genericType);
        }
        #endregion
        public static void CopyInterface<TInterface, T>(this TInterface source, T destination)
            where T : TInterface
        {
            if (destination.IsNull())
                throw new ArgumentNullException("destination");

            foreach (var property in typeof(TInterface).GetProperties()
                .Where(prop => prop.CanRead && prop.CanWrite))
            {
                object propertyValue = property.GetValue(source, null);
                property.SetValue(destination, propertyValue);
            }
        }

        public static void CopyFromInterface<TInterface>(this TInterface destination, TInterface source)
        {
            if (destination.IsNull())
                throw new ArgumentNullException("destination");

            foreach (var property in typeof(TInterface).GetProperties()
                .Where(prop => prop.CanRead && prop.CanWrite))
            {
                object propertyValue = property.GetValue(source, null);
                property.SetValue(destination, propertyValue);
            }
        }

    }
}
