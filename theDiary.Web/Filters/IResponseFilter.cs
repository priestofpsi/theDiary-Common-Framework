using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System.Web
{
    /// <summary>
    /// Defines the functionality required for implementation of a <see cref="HttpResponseBase"/>.<value>Filter</value>.
    /// </summary>
    public interface IResponseFilter
    {
        #region Property Definitions
        /// <summary>
        /// Gets the <see cref="HttpApplication"/> instance associated to the <see cref="IResponseFilter"/>.
        /// </summary>
        HttpApplication Application { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="IResponseFilter"/> supports reading.
        /// </summary>
        bool CanRead { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="IResponseFilter"/> supports seeking.
        /// </summary>
        bool CanSeek { get; }

        /// <summary>
        /// Gets a value indicating whether the current <see cref="IResponseFilter"/> supports writing.
        /// </summary>
        bool CanWrite { get; }

        /// <summary>
        /// Gets the length in bytes of the <see cref="IResponseFilter"/>.
        /// </summary>
        long Length { get; }

        /// <summary>
        /// Gets or sets the position within the current <see cref="IResponseFilter"/>.
        /// </summary>
        long Position { get; set; }
        #endregion

        #region Method & Function Definitions
        /// <summary>
        /// reads a sequence of bytes from the current <see cref="IResponseFilter"/> and advances the position within the <see cref="IResponseFilter"/> by the number of bytes read.
        /// </summary>
        /// <param name="buffer">An array of bytes. When this method returns, the buffer contains the specified byte array with the values between offset and (offset + count - 1) replaced by the bytes read from the current source.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin storing the data read from the current stream.</param>
        /// <param name="count">The maximum number of bytes to be read from the current <see cref="IResponseFilter"/>.</param>
        /// <returns>The total number of bytes read into the buffer. This can be less than the number of bytes requested if that many bytes are not currently available, or zero (0) if the end of the <see cref="IResponseFilter"/> has been reached.</returns>
        int Read(byte[] buffer, int offset, int count);

        /// <summary>
        /// Writes a sequence of bytes to the current <see cref="IResponseFilter"/> and advances the current position within this <see cref="IResponseFilter"/> by the number of bytes written.
        /// </summary>
        /// <param name="buffer">An array of bytes. This method copies count bytes from buffer to the current <see cref="IResponseFilter"/>.</param>
        /// <param name="offset">The zero-based byte offset in buffer at which to begin copying bytes to the current <see cref="IResponseFilter"/>.</param>
        /// <param name="count">The number of bytes to be written to the current <see cref="IResponseFilter"/>.</param>
        void Write(byte[] buffer, int offset, int count);

        /// <summary>
        /// Sets the position within the current <see cref="IResponseFilter"/>.
        /// </summary>
        /// <param name="offset">A <see cref="byte"/> offset relative to the origin parameter.</param>
        /// <param name="origin">A value of type <see cref="System.IO.SeekOrigin"/> indicating the reference point used to obtain the new position.</param>
        /// <returns>The new position within the current <see cref="IResponseFilter"/>.</returns>
        long Seek(long offset, SeekOrigin origin);

        /// <summary>
        /// Sets the length of the current <see cref="IResponseFilter"/>.
        /// </summary>
        /// <param name="value">The desired length of the current <see cref="IResponseFilter"/> in bytes.</param>
        void SetLength(long value);

        /// <summary>
        /// Closes the current <see cref="IResponseFilter"/> and releases any resources (such as sockets and file handles) associated with the current <see cref="IResponseFilter"/>. 
        /// Instead of calling this method, ensure that the <see cref="IResponseFilter"/> is properly disposed.
        /// </summary>
        void Close();

        /// <summary>
        /// Clears all buffers for this <see cref="IResponseFilter"/> and causes any buffered data to be written to the underlying <see cref="HttpResponse"/>.
        /// </summary>
        void Flush();

        /// <summary>
        /// Notifies the <see cref="IResponseFilter"/> that no more data will be received and it can perform any additional processes required.
        /// </summary>
        void ProcessFilter();
        #endregion
    }
}
