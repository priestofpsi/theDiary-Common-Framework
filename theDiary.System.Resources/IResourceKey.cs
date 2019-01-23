using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Resources
{
    internal interface IResourceKey
        : IResourceValue
    {
        string ResourceKey { get; }
    }
}
