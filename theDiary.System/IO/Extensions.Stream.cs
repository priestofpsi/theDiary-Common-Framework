using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace System.IO
{
    /// <summary>
    /// Provides extensions methods used for <see cref="Stream"/> and <see cref="Byte"/> <see cref="Array"/> implementations.
    /// </summary>
    public static class StreamExtensions
    {
        #region MimeType Methods & Functions
        public static string GetMimeType(this byte[] data)
        {
            if (data.IsNull())
                throw new ArgumentNullException("data");

            return MimeTypeHelper.GetMimeType(data);
        }

        public static string GetMimeType(this System.IO.Stream stream)
        {
            if (stream.IsNull())
                throw new ArgumentNullException("stream");

            return MimeTypeHelper.GetMimeType(stream.ToByteArray());
        }

        public static string GetMimeType(this System.IO.FileInfo file)
        {
            if (file.IsNull())
                throw new ArgumentNullException("file");

            if (!file.Exists)
                throw new FileNotFoundException(string.Format("{0} not found", file.FullName));

            return MimeTypeHelper.GetMimeType(file);
        }
        #endregion

        #region Byte[] Methods & Functions
/// <summary>
/// Converts a <see cref="Stream"/> to a <see cref="Byte"/> <see cref="Array"/>.
/// </summary>
/// <param name="stream">The <see cref="Stream"/> to convert.</param>
/// <returns>A <see cref="Byte"/> <see cref="Array"/> of the data contained in the <paramref name="stream"/>.</returns>
/// <exception cref="ArgumentNullException"> thrown if the <paramref name="stream"/> is <c>Null</c>.</exception>
public static byte[] ToByteArray(this Stream stream)
{
    if (stream.IsNull())
        throw new ArgumentNullException("stream");

    MemoryStream mStream = stream as MemoryStream;
    if (mStream.IsNotNull())
        return mStream.ToArray();

    stream.Position = 0;
    byte[] buffer = new byte[stream.Length];
    for (int totalBytesCopied = 0; totalBytesCopied < stream.Length; )
        totalBytesCopied += stream.Read(buffer, totalBytesCopied, Convert.ToInt32(stream.Length) - totalBytesCopied);
    return buffer;
}
        #endregion

        #region MemoryStream Methods & Functions
        /// <summary>
        /// Copies a <see cref="Stream"/> instance to a new instance of a <see cref="MemoryStream"/>.
        /// </summary>
        /// <param name="source">The <see cref="Stream"/> to copy from.</param>
        /// <returns>A new instance of a <see cref="MemoryStream"/> containing the data in the <paramref name="source"/>.</returns>
        public static MemoryStream ToMemoryStream(this Stream source)
        {
            if (source.IsNull())
                throw new ArgumentNullException("stream");

            MemoryStream mStream = source as MemoryStream;
            if (mStream.IsNotNull())
                return mStream;

            return new MemoryStream(source.ToByteArray());
        }
        #endregion
    }
}
