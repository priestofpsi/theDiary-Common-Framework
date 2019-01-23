using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    /// <summary>
    /// Indicates that the specified element should be ignored.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct
        | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Event)]
    public sealed class IgnoreAttribute
        : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IgnoreAttribute"/>.
        /// </summary>
        public IgnoreAttribute()
            : base()
        {
        }
        #endregion

    }
}
