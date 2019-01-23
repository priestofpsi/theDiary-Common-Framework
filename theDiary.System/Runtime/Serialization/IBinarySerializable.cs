using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.Runtime.Serialization
{
    /// <summary>
    /// Allows an object to be serialized and deserialized using a <see cref="Formatters.Binary.BinaryFormatter"/>.
    /// </summary>
    public interface IBinarySerializable
        : ISerializable
    {
    }
}
