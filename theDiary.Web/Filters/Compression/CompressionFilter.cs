using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace System.Web.Compression
{
    public class CompressionFilter
        : Stream, IResponseFilter
    {
        #region Constructors
        public CompressionFilter(HttpContext context)
            : this(context, context.GetCompressionSupport())
        {
        }

        public CompressionFilter(HttpContext context, CompressionSupport compressionType)
            : this(context.Response.Filter, context.ApplicationInstance, compressionType)
        {
            
        }

        public CompressionFilter(Stream filterStream, HttpApplication application, CompressionSupport compressionType)
        {
            if (filterStream == null)
                throw new ArgumentNullException("filterStream");

            if (application == null)
                throw new ArgumentNullException("application");

            this.filterStream = filterStream;
            this.Application = application;
            this.SupportedCompression = compressionType;
            this.compressionStream = new MemoryStream();
        }
        #endregion

        #region Private Declarations
        private Stream filterStream;
        private MemoryStream compressionStream;
        private long filterPosition;
        #endregion

        #region Properites
        public HttpApplication Application { get; private set; }

        public CompressionSupport SupportedCompression {get; private set;}

        public override bool CanRead
        {
            get 
            { 
                return true; 
            }
        }

        public override bool CanSeek
        {
            get 
            { 
                return true; 
            }
        }

        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }

        public override void Flush()
        {
            this.filterStream.Flush();
        }

        public override long Length
        {
            get 
            { 
                return 0; 
            }
        }
        
        public override long Position
        {
            get 
            {
                return this.filterPosition; 
            }
            set 
            {
                this.filterPosition = value; 
            }
        }

        #endregion

        #region Methods

        public override int Read(byte[] buffer, int offset, int count)
        {
            return this.filterStream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return this.filterStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            this.filterStream.SetLength(value);
        }

        public override void Close()
        {
            this.filterStream.Close();
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.SupportedCompression == CompressionSupport.None)
            {
                this.filterStream.Write(buffer, offset, count);
            }
            else
            {
                this.compressionStream.Write(buffer, offset, count);
            }
        }

        public void ProcessFilter()
        {
            if (this.SupportedCompression == CompressionSupport.None)
                return;

            this.compressionStream.Position = 0;
            byte[] compressedData = this.compressionStream.ToArray();
            Stream requiredStream;
            switch(this.SupportedCompression)
            {
                
                case CompressionSupport.Deflate:
                    requiredStream = new DeflateStream(this.filterStream, CompressionMode.Compress);
                    break;
                case CompressionSupport.GZip:
                    requiredStream = new GZipStream(this.filterStream, CompressionMode.Compress);
                    break;
                default:
                    requiredStream = this.filterStream;
                    break;
            }
            requiredStream.Write(compressedData, 0, compressedData.Length);
        }
        #endregion
    }
}
