using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Runtime.Serialization
{
    /// <summary>
    /// Provides extension methods used to aid in serialization and deserialization of <see cref="Object"/> instances.
    /// </summary>
    public static partial class SerializationExtensions
    {
        #region Private Static Declarations
        private static Dictionary<Type, Tuple<MethodInfo, MethodInfo>> validFormatters = new Dictionary<Type, Tuple<MethodInfo, MethodInfo>>();
        private static readonly object syncObject = new object();
        #endregion

        #region Binary Serializable Methods & Functions
        /// <summary>
        /// Serializes an object to a <see cref="MemoryStream"/> using a <see cref="Formatters.Binary.BinaryFormatter"/>.
        /// </summary>
        /// <param name="instance">The instance to serialize.</param>
        /// <returns>The <see cref="Stream"/> to serialize the <paramref name="instance"/> too.</returns>
        /// <exception cref="ArgumentNullException"> thrown if the <paramref name="instance"/> or <paramref name="stream"/> is <c>Null</c>.</exception>
        public static MemoryStream Serialize(this IBinarySerializable instance)
        {
            MemoryStream stream = new MemoryStream();
            instance.Serialize(stream);

            return stream;
        }

        /// <summary>
        /// Serializes an object to the specified <paramref name="stream"/> using a <see cref="Formatters.Binary.BinaryFormatter"/>.
        /// </summary>
        /// <param name="instance">The instance to serialize.</param>
        /// <param name="stream">The <see cref="Stream"/> to serialize the <paramref name="instance"/> too.</param>
        /// <exception cref="ArgumentNullException"> thrown if the <paramref name="instance"/> or <paramref name="stream"/> is <c>Null</c>.</exception>
        public static void Serialize(this IBinarySerializable instance, Stream stream)
        {
            if (instance.IsNull())
                throw new ArgumentNullException("instance");

            if (stream.IsNull())
                throw new ArgumentNullException("stream");

            Formatters.Binary.BinaryFormatter formatter = new Formatters.Binary.BinaryFormatter();
            formatter.Serialize(stream, instance);
        }

        /// <summary>
        /// Deserializes a <see cref="Stream"/> to the specified <paramref name="instance"/>.
        /// </summary>
        /// <param name="instance">The instance that the <paramref name="stream"/> will be deserialized to.</param>
        /// <param name="stream">The <see cref="Stream"/> containing the data to deserialize.</param>
        /// <exception cref="ArgumentNullException"> thrown if the <paramref name="instance"/> or <paramref name="stream"/> is <c>Null</c>.</exception>
        public static void Deserialize(this IBinarySerializable instance, Stream stream)
        {
            if (instance.IsNull())
                throw new ArgumentNullException("instance");

            if (stream.IsNull())
                throw new ArgumentNullException("stream");

            Formatters.Binary.BinaryFormatter formatter = new Formatters.Binary.BinaryFormatter();
            formatter.Deserialize(stream).CopyTo(instance);
        }
        #endregion

        #region Generic Serializable Methods & Functions
        /// <summary>
        ///  Serializes an object to a new <see cref="MemoryStream"/> using the class specified by <typeparamref name="TFormatter"/>.
        /// </summary>
        /// <typeparam name="TFormatter">The <see cref="Type"/> used to serialize the <paramref name="instance"/>.</typeparam>
        /// <param name="instance">The instance to serialize.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"> thrown if the <paramref name="instance"/> or <paramref name="stream"/> is <c>Null</c>.</exception>
        /// <exception cref="ArgumentException"> thrown if the <typeparamref name="TFormatter"/> is not a valid Serialization formatter <see cref="Type"/>.</exception>
        public static MemoryStream Serialize<TFormatter>(this ISerializable instance)
            where TFormatter : class, new()
        {
            MemoryStream stream = new MemoryStream();
            instance.Serialize<TFormatter>(stream);

            return stream;
        }

        /// <summary>
        /// Serializes an object to the specified <paramref name="stream"/> using the class specified by <typeparamref name="TFormatter"/>.
        /// </summary>
        /// <typeparam name="TFormatter">The <see cref="Type"/> used to serialize the <paramref name="instance"/>.</typeparam>
        /// <param name="instance">The instance to serialize.</param>
        /// <param name="stream">The <see cref="Stream"/> to serialize the <paramref name="instance"/> too.</param>
        /// <exception cref="ArgumentNullException"> thrown if the <paramref name="instance"/> or <paramref name="stream"/> is <c>Null</c>.</exception>
        /// <exception cref="ArgumentException"> thrown if the <typeparamref name="TFormatter"/> is not a valid Serialization formatter <see cref="Type"/>.</exception>
        public static void Serialize<TFormatter>(this ISerializable instance, Stream stream)
            where TFormatter : class, new()
        {
            if (instance.IsNull())
                throw new ArgumentNullException("instance");

            if (stream.IsNull())
                throw new ArgumentNullException("stream");

            if (!SerializationExtensions.IsSerializableFormatter<TFormatter>())
                throw new ArgumentException("The specified TFormatter Type is not a valid Serialization Formatter.");

            TFormatter formatter = new TFormatter();
            formatter.ExecuteMethod(SerializationMethods.Serialize, stream, instance);
        }

        /// <summary>
        /// Deserializes a <see cref="Stream"/> to the specified <paramref name="instance"/>.
        /// </summary>
        /// <typeparam name="TFormatter">The <see cref="Type"/> used to deserialize the <paramref name="instance"/>.</typeparam>
        /// <param name="instance">The instance that the <paramref name="stream"/> will be deserialized to.</param>
        /// <param name="stream">The <see cref="Stream"/> containing the data to deserialize.</param>
        /// <exception cref="ArgumentNullException"> thrown if the <paramref name="instance"/> or <paramref name="stream"/> is <c>Null</c>.</exception>
        /// <exception cref="ArgumentException"> thrown if the <typeparamref name="TFormatter"/> is not a valid Serialization formatter <see cref="Type"/>.</exception>
        public static void Deserialize<TFormatter>(this ISerializable instance, Stream stream)
            where TFormatter : class, new()
        {
            if (instance.IsNull())
                throw new ArgumentNullException("instance");

            if (stream.IsNull())
                throw new ArgumentNullException("stream");

            if (!SerializationExtensions.IsSerializableFormatter<TFormatter>())
                throw new InvalidOperationException("The specified Type is not a valid Serialization Formatter.");

            TFormatter formatter = new TFormatter();
            formatter.ExecuteMethod(SerializationMethods.Deserialize, stream).CopyTo(instance);
        }
        #endregion

        public static T GetValue<T>(this SerializationInfo info, string name)
        {
            return (T)info.GetValue(name, typeof(T));
        }

        public static void AddValue<T>(this SerializationInfo info, string name, T value)
        {
            info.AddValue(name, value, typeof(T));
        }

        #region Private Methods & Functions
        private static bool IsSerializableFormatter<TFormatter>()
        {
            lock (SerializationExtensions.syncObject)
            {
                Type formatterType = typeof(TFormatter);
                if (!SerializationExtensions.validFormatters.ContainsKey(formatterType))
                {
                    Tuple<MethodInfo, MethodInfo> methods = new Tuple<MethodInfo, MethodInfo>(formatterType.GetMethod("Serialize"), formatterType.GetMethod("Deserialize"));
                    if (methods.Item1 == null || methods.Item2 == null)
                        methods = null;
                    SerializationExtensions.validFormatters.Add(formatterType, methods);
                }
                return SerializationExtensions.validFormatters[formatterType] != null;
            }
        }

        private static object ExecuteMethod<T>(this T instance, SerializationMethods method, params object[] args)
        {
            
            lock (SerializationExtensions.syncObject)
            {
                object methodArg = args.FirstOrDefault();
                object[] methodParams = args.Skip(1).ToArray();
                MethodInfo methodInfo = (method == SerializationMethods.Serialize) ? SerializationExtensions.validFormatters[typeof(T)] .Item1 : SerializationExtensions.validFormatters[typeof(T)] .Item2;

                return methodInfo.Invoke(methodArg, methodParams);
            }            
        }

        private static void CopyTo(this object source, object target)
        {
            source.GetType().GetProperties().Where(a => a.CanRead && a.CanWrite).ForEachAsParallel(property => property.SetValue(target, property.GetValue(source, null)));
        }
        #endregion

        private enum SerializationMethods
        {
            Serialize,
            Deserialize,
        }
    }
}
