using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Resources
{
    internal interface IResourceValue
    {
        string Value { get; }
    }

    internal interface IResourceInfo
        : IResourceValue
    {
        CultureInfo Culture { get; }

        string CultureISOCode { get; }

        
    }
}
