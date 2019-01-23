using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public class EnumMetadataProvider
        : CachedDataAnnotationsModelMetadataProvider
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instances of the <see cref="EnumMetadataProvider"/>.
        /// </summary>
        public EnumMetadataProvider()
            : base()
        {
        }
        #endregion

        #region Protected Overriden Methods & Functions
        protected override CachedDataAnnotationsModelMetadata CreateMetadataFromPrototype(
            CachedDataAnnotationsModelMetadata prototype, Func<object> modelAccessor)
        {
            var metadata = base.CreateMetadataFromPrototype(prototype, modelAccessor);
            var type = metadata.ModelType;
            if (type.IsEnum ||
                (type.IsGenericType(typeof(Nullable<>)) && 
                type.GetGenericArguments().Length == 1 && type.GetGenericArguments()[0].IsEnum))
                metadata.TemplateHint = "Enum";

            return metadata;
        }
        #endregion
    }
}