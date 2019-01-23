using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Web.Mvc.FileDownload
{
    public class PrimitiveExcelExportProvider<T>
        : IExportStreamProvider<T>
        where T : IEnumerable<T>
    {
        public string ProviderName
        {
            get { return "Html as Excel"; }
        }

        public string ProviderDisplayName
        {
            get { return "Html Excel File"; }
        }

        public string ProviderDefaultExtension
        {
            get { return "xls"; }
        }

        public string ContentMIMEType
        {
            get { return @"application/vnd.ms-excel"; }
        }

        public string DownloadFileName { get; set; }

        public void CreateDownloadStreamAsync<T>(T data, DownloadStreamCreatedHandler<T> callBack)
        {
            DownloadStreamCreatedHandler cb = (s, e) =>
            {
                var e1 = new DownloadStreamProviderEventArgs<T>((T)e.ContentSource, e.ContentMIMEType, e.DownloadFileName, e.ContentData);
                callBack(this, e1);
            };
            ((IDownloadStreamProviderAsync)this).CreateDownloadStreamAsync(data, cb);
        }

        byte[] IDownloadStreamProvider.CreateDownloadStream(object data)
        {
            byte[] returnValue;
            string inputContent = this.CreateExcelContent((IEnumerable<T>)data);
            returnValue = Encoding.UTF8.GetBytes(inputContent);

            return returnValue;
        }

        void IDownloadStreamProviderAsync.CreateDownloadStreamAsync(object data, DownloadStreamCreatedHandler callBack)
        {
            DownloadStreamProviderEventArgs e;
            ManualResetEventSlim ev = new ManualResetEventSlim();
            byte[] contentData = null;
            System.Threading.ThreadPool.QueueUserWorkItem((cb) =>
            {
                contentData = ((IDownloadStreamProvider)this).CreateDownloadStream(data);
                ev.Set();
            });
            ev.Wait();
            callBack(this, new DownloadStreamProviderEventArgs(data, this.ContentMIMEType, this.DownloadFileName, contentData));
        }

        private string CreateExcelContent(IEnumerable<T> items, params string[] fieldNames)
        {
            System.Text.StringBuilder content = new System.Text.StringBuilder();
            content.AppendLine("<table>");

            IEnumerable<PropertyInfo> properties;
            if (fieldNames.IsNullOrEmpty())
            {
                properties = typeof(T).GetProperties().Where(property => property.CanRead);
            }
            else
            {
                properties = typeof(T).GetProperties().Where(property => fieldNames.Contains(property.Name) && property.CanRead);
            }


            this.WriteHeaderRow(default(T), content, properties, (a, b) => b.Name as string);
            foreach (T item in items)
                this.WriteRow(item, content, properties, (a, b) => b.GetValue(a, null) as string);

            content.AppendLine("</table>");
            return content.ToString();
        }

        private void WriteHeaderRow(T instance, System.Text.StringBuilder content, IEnumerable<PropertyInfo> properties, Func<T, PropertyInfo, string> func)
        {
            this.WriteRow(default(T), content, properties, func, "th");
        }

        private void WriteRow(T instance, System.Text.StringBuilder content, IEnumerable<PropertyInfo> properties, Func<T, PropertyInfo, string> func, string cellElement = "td")
        {
            content.AppendLine("<tr>");
            foreach (PropertyInfo pf in properties)
                content.AppendFormat("<{1}>{0}</{1}>", func(instance, pf), cellElement);

            content.AppendLine("</tr>");
        }
    }
}
