using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace System.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Specifies that a Field or Property is supports AutoCompletion.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class AutoCompleteAttribute 
        : Attribute, IMetadataAware
    {
        #region Constructors
        public AutoCompleteAttribute(string controller, string action)
        {
            if (controller.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException("controller");

            if (action.IsNullEmptyOrWhiteSpace())
                throw new ArgumentNullException("action");

            this.controller = controller;
            this.action = action;
        }
        #endregion

        #region Private Declarations
        private readonly string controller;
        private readonly string action;
        #endregion

        #region Public Methods & Functions
        public void OnMetadataCreated(ModelMetadata metadata)
        {
            metadata.SetAutoComplete(this.controller, this.action);
        }
        #endregion
    }
}
