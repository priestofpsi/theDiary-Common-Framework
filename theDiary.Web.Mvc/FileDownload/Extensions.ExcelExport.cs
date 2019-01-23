using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.FileDownload;

namespace System.Web.Mvc
{
    public static partial class ExcelExport
    {
        public static IEnumerable<IExportStreamProvider> GetExportProviders(this Controller controller)
        {
            Dictionary<Type, IExportStreamProvider> returnValue = new Dictionary<Type, IExportStreamProvider>();
            AppDomain.CurrentDomain.GetAssemblies().ForEachAsParallel(assembly => 
                {
                    assembly.GetTypes().Where(type => type.ImplementsInterface<IExportStreamProvider>() 
                        && !returnValue.ContainsKey(type)).ForEachAsParallel(type=>
                    {
                        returnValue.Add(type, (IExportStreamProvider) System.Activator.CreateInstance(type));
                    });
                });
            return returnValue.Values;
        }

        public static FileResult ExportAsFileAction<T>(this Controller controller, T items, IExportStreamProvider<T> provider)
            where T : IEnumerable<T>
        {
            FileResult returnValue = new FileContentResult(provider.CreateDownloadStream(items), provider.ContentMIMEType);
            returnValue.FileDownloadName = provider.DownloadFileName;

            return returnValue;
        }

        public static void ActionAsExcel(this Controller controller, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentNullException("fileName");

            if (!fileName.EndsWith(".xls", StringComparison.OrdinalIgnoreCase))
                fileName += ".xls";
            controller.Response.ClearContent();
            controller.Response.Clear();
            controller.Response.ContentType = fileName.GetContentType(".xls");
            controller.Response.AppendHeader("Content-Disposition", string.Format("attachment; filename={0}", fileName));
        }

        public static FileResult CreateExcelFile<T>(this Controller controller, IEnumerable<T> items)
        {
            return controller.CreateExcelFile<T>(items, null);
        }

        public static FileResult CreateExcelFile<T>(this Controller controller, string fileName, IEnumerable<T> items)
        {
            var result = controller.CreateExcelFile<T>(items, null);
            result.FileDownloadName = fileName;

            return result;
        }

        public static FileResult CreateExcelFile<T>(this Controller controller, IEnumerable<T> items, params string[] fieldNames)
        {
            byte[] content;
            string contentType;
            string fileContent = items.ExportToExcel(fieldNames);
            SaveToStream(fileContent, out contentType, out content);
            return new FileContentResult(content, contentType);
        }


        public static void SaveToStream(string inputContent, out string contentType, out byte[] content)
        {
            content = Encoding.UTF8.GetBytes(inputContent);
            contentType = @"application/vnd.ms-excel";
        }

        public static string ExportToExcel<T>(this IEnumerable<T> items, params string[] fieldNames)
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


            WriteRow<T>(default(T), content, properties, (a, b) => b.Name as string, "th");
            foreach (T item in items)
                WriteRow(item, content, properties, (a, b) => b.GetValue(a, null) as string);

            content.AppendLine("</table>");
            return content.ToString();
        }

        private static void WriteHeaderRow<T>(T instance, System.Text.StringBuilder content, IEnumerable<PropertyInfo> properties, Func<T, PropertyInfo, string> func)
        {
            WriteRow<T>(default(T), content, properties, func, "th");
        }

        private static void WriteRow<T>(T instance, System.Text.StringBuilder content, IEnumerable<PropertyInfo> properties, Func<T, PropertyInfo, string> func, string cellElement = "td")
        {
            content.AppendLine("<tr>");
            foreach (PropertyInfo pf in properties)
                content.AppendFormat("<{1}>{0}</{1}>", func(instance, pf), cellElement);

            content.AppendLine("</tr>");
        }

    }
}
