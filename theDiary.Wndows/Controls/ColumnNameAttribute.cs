using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnNameAttribute
        : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ColumnNameAttribute"/> class.
        /// </summary>
        /// <param name="columnName">The name of the column.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="columnName"/> is <c>Null</c> or <c>Empty</c>.</exception>
        public ColumnNameAttribute(string columnName)
            : base()
        {
            if (string.IsNullOrWhiteSpace(columnName))
                throw new ArgumentNullException("columnName");

            this.ColumnName = columnName.Trim();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the name of the column.
        /// </summary>
        public string ColumnName { get; private set; }
        #endregion
    }
}
