using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ColumnHiddenAttribute
        : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnHiddenAttribute"/>.
        /// </summary>
        public ColumnHiddenAttribute()
            : base()
        {
        }
        #endregion
    }
}