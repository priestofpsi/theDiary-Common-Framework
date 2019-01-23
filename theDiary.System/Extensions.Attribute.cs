using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    public static partial class SystemExtensions
    {
        public static bool ImplementsInterface<T>(this Type type)
        {
            Type interfaceType = typeof(T);

            if (!interfaceType.IsInterface)
                throw new InvalidOperationException("The type specified is not an interface type.");

            Type[] interfaces = type.FindInterfaces(new TypeFilter((a, b) => a == interfaceType), null);

            return (interfaces != null && interfaces.Length != 0);
        }

        /// <summary>
        /// Determines if the specified <paramref name="attributeProvider"/> has an attribute of <typeparamref name="T"/> in its full implementation hierarchy.
        /// </summary>
        /// <typeparam name="T">The <see cref="Type"/> of the attribute to locate.</typeparam>
        /// <param name="attributeProvider">The <see cref="ICustomAttributeProvider"/> instance to check.</param>
        /// <returns><c>True</c> if the <paramref name="attributeProvider"/> has an attribute of <typeparamref name="T"/>; otherwisew <c>False</c>.</returns>
        public static bool HasAttribute<T>(this ICustomAttributeProvider attributeProvider)
                where T : Attribute
        {
            return attributeProvider.HasAttribute<T>(true);
        }

        public static bool HasAttribute<T>(this ICustomAttributeProvider attributeProvider, bool inherited)
            where T : Attribute
        {
            return !attributeProvider.GetCustomAttributes(typeof(T), inherited).IsNullOrEmpty();
        }

        public static T GetAttribute<T>(this ICustomAttributeProvider attributeProvider)
            where T : Attribute
        {
            return attributeProvider.GetAttribute<T>(false);
        }

        public static T GetAttribute<T>(this ICustomAttributeProvider attributeProvider, bool inherited)
            where T : Attribute
        {
            return attributeProvider.GetAttributes<T>(inherited).FirstOrDefault();
        }
        
        public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider attributeProvider)
            where T : Attribute
        {
            return attributeProvider.GetAttributes<T>(true);
        }

        public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider attributeProvider, bool inherited)
            where T : Attribute
        {
            return attributeProvider.GetCustomAttributes(typeof(T), inherited).Cast<T>();
        }

        public static IEnumerable<T> GetAttributes<T>(this ICustomAttributeProvider attributeProvider, Predicate<T> predicate)
            where T: Attribute
        {
            return attributeProvider.GetAttributes<T>().Where(attribute=>predicate(attribute));
        }
    }
}
