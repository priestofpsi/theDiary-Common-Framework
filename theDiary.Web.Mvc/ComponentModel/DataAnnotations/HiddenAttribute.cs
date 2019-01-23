using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies that a Field or Property is hidden, and should not be shown.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class HiddenAttribute
        : Attribute
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="HiddenAttribute"/> class.
        /// </summary>
        public HiddenAttribute()
            : this(true)
        {
        }

        /// <summary>C:\Development\DotNot\Projects\Iterative\BizAids New\BizAids\Extensions\EnumValue.cs
        /// Initializes a new instance of the <see cref="HiddenAttribute"/> class, with a value indicating if the Field or Property is hidden or not.
        /// </summary>
        /// <param name="hidden">Value indicating if hidden or not.</param>
        public HiddenAttribute(bool hidden)
            : base()
        {
            this.Visible = !hidden;
        }
        #endregion

        #region Public Read-Only Properties
        /// <summary>
        /// Gets a value indicating if the Field or Property is visible or not.
        /// </summary>
        public bool Visible { get; private set; }
        #endregion
    }
}