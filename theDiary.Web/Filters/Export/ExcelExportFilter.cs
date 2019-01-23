using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace System.Web.Export.Excel
{
    public sealed class ExcelExportFilter
        : Stream, IResponseFilter
    {
        #region Constructors
        public ExcelExportFilter(HttpContext context)
            : this(context.Response.Filter, context.ApplicationInstance)
        {
        }

        public ExcelExportFilter(Stream filterStream, HttpApplication application)
        {
            if (filterStream == null)
                throw new ArgumentNullException("filterStream");

            if (application == null)
                throw new ArgumentNullException("application");

            this.filterStream = filterStream;
            this.Application = application;
            this.temporaryStream = new MemoryStream();
        }
        #endregion

        #region Private Declarations
        private RegExPattern stylePattern = @"(\\<link href=\""(*.?)\"" rel=\""stylesheet\"" type=\""text/css\"" \\/>";
        private RegExPattern scriptPattern = new RegExPattern(@"(\\<script(.+?)\\</script\\>)", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.CultureInvariant);
        private Stream filterStream;
        private MemoryStream temporaryStream;
        private long filterPosition;
        #endregion

        #region Public Properties
        public HttpApplication Application { get; private set; }

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

        #region Public Methods & Functions
        public override void Flush()
        {
            this.filterStream.Flush();
        }

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
            this.temporaryStream.Write(buffer, offset, count);
        }

        public void ProcessFilter()
        {
            StringBuilder content = Encoding.UTF8.GetString(this.temporaryStream.ToArray());
            Match match = null;
            while (this.scriptPattern.TryMatch(content, match, out match))
                content.Remove(match.Value);

            match = null;
            bool firstStyle = false;
            while (this.stylePattern.TryMatch(content, match, out match))
            {
                if (!firstStyle)
                {
                    firstStyle = true;
                    content.Replace(match.Value, "<<STYLESHEETREFERENCE>>");
                }
                else
                {
                    // LOAD STYLESHEET CONTENT
                    content.Remove(match.Value);
                }
            }
        }
        #endregion
    }
}
