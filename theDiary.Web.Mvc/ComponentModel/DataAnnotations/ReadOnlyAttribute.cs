using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies that a Field or Property is Read-Only, and should not editable.
    /// </summary>
    public sealed class ReadOnlyAttribute
        : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyAttribute"/>.
        /// </summary>
        public ReadOnlyAttribute()
            : base()
        {
            this.ControllerNames = new string[] { };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadOnlyAttribute"/> class, with the names of the controllers
        /// where the Field or Property is Read-Only.
        /// </summary>
        /// <param name="controllerNames">The names of the controllers.</param>
        public ReadOnlyAttribute(params string[] controllerNames)
            : base()
        {
            this.ControllerNames = controllerNames;
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Gets the names of the Controllers where the field is Read-Only,
        /// </summary>
        public string[] ControllerNames { get; private set; }
        #endregion
    }
}
