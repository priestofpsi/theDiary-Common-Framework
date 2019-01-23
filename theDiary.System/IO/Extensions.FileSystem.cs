using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.IO
{
    public static class FileSystemExtensions
    {
        public static Uri ToUri(this FileSystemInfo info)
        {
            return new Uri(info.FullName.FilePathToFileUrl());
        }

        public static string FilePathToFileUrl(this string filePath)
        {
            StringBuilder uri = new StringBuilder();
            filePath.ForEach(v =>
            {
                if ((v >= 'a' && v <= 'z') || (v >= 'A' && v <= 'Z') || (v >= '0' && v <= '9') ||
                        v.IsAny('+', '/', ':', '.', '-', '_', '~', '\xFF'))
                //v == '+' || v == '/' || v == ':' || v == '.' || v == '-' || v == '_' || v == '~' ||
                //v > '\xFF')
                {
                    uri.Append(v);
                }
                else if (v.IsAny(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar))
                {
                    uri.Append('/');
                }
                else
                {
                    uri.Append(String.Format("%{0:X2}", (int) v));
                }
            });
            if (uri.Length >= 2 && uri[0] == '/' && uri[1] == '/') // UNC path
                uri.Insert(0, "file:");
            else
                uri.Insert(0, "file:///");
            return uri.ToString();
        }
    }
}
