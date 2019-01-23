using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compression;
using System.IO;

namespace System.Web
{
    public static partial class HttpExtensions
    {
        #region Private Constants
        private const string DeflateIdentifier1 = "deflate";
        private const string DeflateIdentifier2 = "*";
        private const string GZipIdentifier1 = "gzip";
        private const string GZipIdentifier2 = "q=0";
        private const string RequestCompressionHeaderKey = "Accept-Encoding";
        private const string ResponseCompressionHeaderKey = "Accept-Encoding";
        private static readonly string[] UncompressableMimeTypes = new string[]{"image/jpeg", "application/zip", "application/x-gzip", "video/mpeg", "video/quicktime", "video/x-la-asf", "video/x-ms-asf", "video/x-msvideo", "", "video/x-sgi-movie", "application/x-gzip", "application/x-gtar", "application/x-compressed", "application/x-compress"};
        #endregion

        #region HttpRequest Compression Extension Methods & Functions
        public static CompressionSupport GetCompressionSupport(this HttpContext context)
        {
            return context.Response.GetCompressionSupport();
        }

        public static CompressionSupport GetCompressionSupport(this HttpRequest request)
        {
            string acceptEncoding = request.Headers[HttpExtensions.RequestCompressionHeaderKey];
            if (acceptEncoding.Contains(HttpExtensions.DeflateIdentifier1) || acceptEncoding == HttpExtensions.DeflateIdentifier2)
            {
                return CompressionSupport.Deflate;
            }
            else if (acceptEncoding.Contains(HttpExtensions.GZipIdentifier1))
            {
                return CompressionSupport.GZip;
            }
            else
            {
                return CompressionSupport.None;
            }
        }

        public static CompressionSupport GetCompressionSupport(this HttpResponse response)
        {
            string acceptEncoding = response.Headers[HttpExtensions.RequestCompressionHeaderKey];
            if (response.MimeTypeSupportsCompression())
            {
                if (acceptEncoding.Contains(HttpExtensions.DeflateIdentifier1) || acceptEncoding == HttpExtensions.DeflateIdentifier2)
                {
                    return CompressionSupport.Deflate;
                }
                else if (acceptEncoding.Contains(HttpExtensions.GZipIdentifier1))
                {
                    return CompressionSupport.GZip;
                }
            }
            
            return CompressionSupport.None;
        }

        /// <summary>
        /// Indicates if the MIME Type specified in the <paramref name="response"/> supports compression.
        /// </summary>
        /// <param name="response">The <see cref="HttpResponse"/> instance to check.</param>
        /// <returns><c>True</c> if the MIME Type supports compression; otherwise <c>False</c>.</returns>
        public static bool MimeTypeSupportsCompression(this HttpResponse response)
        {
            return !HttpExtensions.UncompressableMimeTypes.Contains(response.ContentType, StringComparison.OrdinalIgnoreCase);
        }

        public static bool SupportsCompression(this HttpContext context)
        {
            return context.Request.SupportsCompression();
        }

        public static bool SupportsCompression(this HttpRequest request)
        {
            string acceptEncoding = request.Headers[HttpExtensions.RequestCompressionHeaderKey];
            return !string.IsNullOrWhiteSpace(acceptEncoding) 
                && acceptEncoding.Contains("deflate") 
                && !acceptEncoding.Contains("q=0");
        }
        #endregion

        #region HttpResponse Compression Extension Methods & Functions
        public static void AddCompressionHeaders(this HttpContext context, string contentType)
        {
            context.Response.ContentType = contentType;
            context.AddCompressionHeaders();
        }

        public static void AddCompressionHeaders(this HttpContext context)
        {
            CompressionSupport compressionSupport = context.GetCompressionSupport();
            context.AddCompressionHeaders(compressionSupport);
        }

        public static void AddCompressionHeaders(this HttpContext context, CompressionSupport compressionSupport)
        {
            context.Response.AddCompressionHeaders(compressionSupport);
        }

        public static void AddCompressionHeaders(this HttpResponse response, string contentType)
        {
            response.ContentType = contentType;
            response.AddCompressionHeaders();
        }
        public static void AddCompressionHeaders(this HttpResponse response)
        {
            CompressionSupport compressionSupport = response.GetCompressionSupport();
            response.AddCompressionHeaders(compressionSupport);
        }

        public static void AddCompressionHeaders(this HttpResponse response, CompressionSupport compressionSupport)
        {
            switch (compressionSupport)
            {
                case CompressionSupport.Deflate:
                    response.AppendHeader(HttpExtensions.ResponseCompressionHeaderKey, HttpExtensions.DeflateIdentifier1);
                    break;
                case CompressionSupport.GZip:
                    response.AppendHeader(HttpExtensions.ResponseCompressionHeaderKey, HttpExtensions.GZipIdentifier1);
                    break;
                default:
                    break;
            }
        }

        public static void SendHttpResponse(HttpContext context, string contentToSend)
        {
            CompressionSupport compressionSupport = context.GetCompressionSupport();
            string acceptEncoding = context.Request.Headers["Accept-Encoding"];
            if (compressionSupport != CompressionSupport.None)
            {
                if (context.Response.Headers.AllKeys.FirstOrDefault(key=>key.Equals(HttpExtensions.ResponseCompressionHeaderKey)) == null)
                    context.AddCompressionHeaders(compressionSupport);

                System.IO.MemoryStream stream = null;
                using (stream = new System.IO.MemoryStream())
                {
                    byte[] content = System.Text.Encoding.UTF8.GetBytes(contentToSend);
                    if (compressionSupport == CompressionSupport.Deflate)
                    {
                        using (System.IO.Compression.DeflateStream zip = new System.IO.Compression.DeflateStream(stream, System.IO.Compression.CompressionMode.Compress, false))
                        {
                            zip.Write(content, 0, content.Length);
                        }

                    }
                    else if (compressionSupport == CompressionSupport.GZip)
                    {
                        using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Compress, false))
                        {
                            zip.Write(content, 0, content.Length);
                        }
                    }
                }
                context.Response.BinaryWrite(stream.ToByteArray());
            }
            else
            {
                context.Response.Write(contentToSend);
            }
            context.Response.Flush();
        }    
        #endregion
    }
}
