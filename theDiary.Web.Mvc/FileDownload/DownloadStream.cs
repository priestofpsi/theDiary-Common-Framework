using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Web.Mvc.FileDownload
{
    public class DownloadStream
        : MemoryStream
    {
        #region Constructors
        public DownloadStream()
            : base()
        {
        }

        public DownloadStream(byte[] buffer)
            : base(buffer)
        {

        }

        public DownloadStream(byte[] buffer, bool writable)
            : base(buffer, writable)
        {
        }

        public DownloadStream(byte[] buffer, int index, int count)
            : base(buffer, index, count)
        {
        }

        public DownloadStream(byte[] buffer, int index, int count, bool writable)
            : base(buffer, index, count, writable)
        {
        }

        public DownloadStream(byte[] buffer, int index, int count, bool writable, bool publiclyVisible)
            : base(buffer, index, count, writable, publiclyVisible)
        {
        }
        #endregion
    }

    public interface IDownloadStreamProvider
    {
        string ProviderName { get; }

        string ProviderDisplayName { get; }

        string ProviderDefaultExtension { get; }

        string ContentMIMEType { get; }

        string DownloadFileName { get; set; }

        byte[] CreateDownloadStream(object data);
    }

    public interface IDownloadStreamProviderAsync
		: IDownloadStreamProvider
    {
        void CreateDownloadStreamAsync(object data, DownloadStreamCreatedHandler callBack);
    }

    public interface IDownloadStreamProviderAsync<T>
        : IDownloadStreamProviderAsync
    {
        new void CreateDownloadStreamAsync<T>(T data, DownloadStreamCreatedHandler<T> callBack);

    }

    public delegate void DownloadStreamCreatedHandler(IDownloadStreamProvider source, DownloadStreamProviderEventArgs e);
    
    public delegate void DownloadStreamCreatedHandler<T>(IDownloadStreamProvider source, DownloadStreamProviderEventArgs<T> e);
    
    public class DownloadStreamProviderEventArgs<T>
		: DownloadStreamProviderEventArgs
    {
        public DownloadStreamProviderEventArgs()
        {
        }

        internal protected DownloadStreamProviderEventArgs(T contentSource, string contentMIMEType, string downloadFileName, byte[] contentData)
			: base(contentSource, contentMIMEType, downloadFileName, contentData)
        {
        }

        public new T ContentSource
        {
            get
            {
                return (T)base.ContentSource;
            }
			set
            {
				base.ContentSource = value;
            }
        }
    }

    public class DownloadStreamProviderEventArgs
		: EventArgs
    {
        public DownloadStreamProviderEventArgs()
			: base()
        {
        }

        internal protected DownloadStreamProviderEventArgs(object contentSource, string contentMIMEType, string downloadFileName, byte[] contentData)
			: this()
        {
            this.ContentMIMEType = contentMIMEType;
            this.DownloadFileName = downloadFileName;
            this.ContentSource = contentSource;
            this.ContentData = contentData;
        }

        public string ContentMIMEType { get; protected set; }

        public string DownloadFileName { get; protected set; }

        public object ContentSource { get; protected set; }

        public byte[] ContentData { get; protected set; }
    }

    public interface IExportStreamProvider
		: IDownloadStreamProvider
    {

    }

    public interface IExportStreamProvider<T>
        : IDownloadStreamProviderAsync<T>
        where T : IEnumerable<T>
    {
        
    }

    
}
