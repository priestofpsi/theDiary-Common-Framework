using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Data.Syncronization
{
    public interface ISyncronizable
    {
        Guid SyncronizationId { get; }

        bool Deleted { get; }
    }

    public interface ISyncronizable<T>
        : ISyncronizable
    {
        DateTime? LastSyncronized { get; }

        T Entity { get; }
    }
}
