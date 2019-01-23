using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace System.IO
{
    /// <summary>
    /// Helper class used to retrieve or locate Mime Types.
    /// </summary>
    public static class MimeTypeHelper
    {
        #region Public Constant Declarations
        /// <summary>
        /// The <see cref="String"/> value representing an Unknown Mime Type.
        /// </summary>
        public static readonly string UnknownMimeType = "unknown/unknown";
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Gets the MimeType for the specified file, based on it's extension if possible, otherwise on its content.
        /// </summary>
        /// <param name="fileName">The path to the file to investigate.</param>
        /// <returns>The Mime Type for the file if successfull; otherwise return 'unknown/unknown'.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="fileName"/> is <c>Null</c> or Empty.</exception>
        /// <exception cref="FileNotFoundException">thrown if the file specified by the parameter <paramref name="fileName"/> does not exist.</exception>
        public static string GetMimeType(string fileName)
        {
            if (fileName.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
                throw new FileNotFoundException(string.Format("{0} not found", fileName));
            
            string mimeType;
            if (!MimeTypeHelper.TryGetMimeTypeByExtension(fileName, out mimeType))
                MimeTypeHelper.TryGetMimeTypeByData(new System.IO.FileInfo(fileName).OpenRead().ToByteArray(), out mimeType);

            return mimeType;
        }

        /// <summary>
        /// Gets the MimeType for the specified file, based on it's extension if possible, otherwise on its content.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> instance to investigate.</param>
        /// <returns>The Mime Type for the file if successfull; otherwise return 'unknown/unknown'.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="file"/> is <c>Null</c> or Empty.</exception>
        /// <exception cref="FileNotFoundException">thrown if the file specified by the parameter <paramref name="file"/> does not exist.</exception>
        public static string GetMimeType(FileInfo file)
        {
            if (file.IsNull())
                throw new ArgumentNullException("file");

            return MimeTypeHelper.GetMimeType(file.FullName);
        }

        /// <summary>
        /// Gets the MimeType for the specified data.
        /// </summary>
        /// <param name="data"><see cref="Byte"/> <see cref="Array"/> containing the data to investigate.</param>
        /// <returns>The Mime Type for the <paramref name="data"/> if successfull; otherwise return 'unknown/unknown'.</returns>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="data"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public static string GetMimeType(byte[] data)
        {
            if (data.IsNullOrEmpty())
                throw new ArgumentNullException("data");

            return MimeTypeHelper.GetMimeTypeByData(data);
        }
        #endregion

        #region Private Methods & Functions
        private static string GetMimeTypeByData(byte[] data)
        {
            string mimeType;
            MimeTypeHelper.TryGetMimeTypeByData(data, out mimeType);
            return mimeType;
        }

        private static bool TryGetMimeTypeByData(byte[] data, out string mimeType)
        {
            byte[] buffer = new byte[256];
            using (MemoryStream stream = new MemoryStream(data))
            {
                if (stream.Length >= 256)
                    stream.Read(buffer, 0, 256);
                else
                    stream.Read(buffer, 0, (int)stream.Length);
            }
            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                mimeType = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return true;
            }
            catch
            {
                mimeType = MimeTypeHelper.UnknownMimeType;
                return false;
            }
        }

        private static string GetMimeTypeByExtension(string fileName)
        {
            string mimeType;
            MimeTypeHelper.TryGetMimeTypeByExtension(fileName, out mimeType);
            return mimeType;
        }

        private static bool TryGetMimeTypeByExtension(string fileName, out string mimeType)
        {
            RegistryKey regKey = Registry.ClassesRoot.OpenSubKey(System.IO.Path.GetExtension(fileName).ToLower());
            mimeType = MimeTypeHelper.UnknownMimeType;
            if (regKey.IsNotNull())
            {
                object contentType = regKey.GetValue("Content Type");

                if (contentType.IsNotNull())
                    mimeType = contentType.ToString();
            }

            return !mimeType.Equals(MimeTypeHelper.UnknownMimeType, StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        #region External Methods & Functions
        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
            System.UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            System.UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
            System.UInt32 dwMimeFlags,
            out System.UInt32 ppwzMimeOut,
            System.UInt32 dwReserverd
        );
        #endregion
    }
}
