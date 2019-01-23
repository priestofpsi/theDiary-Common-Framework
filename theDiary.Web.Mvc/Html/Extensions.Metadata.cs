using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc.Html;
using System.ComponentModel.DataAnnotations;

namespace System.Web.Mvc.Html
{
    public static partial class MetaDataExtensions
    {
        public static TAttribute GetMetaDataAttribute<TAttribute>(this ModelMetadata metaData)
            where TAttribute : Attribute
        {
            TAttribute returnValue = (TAttribute)metaData.ContainerType.GetProperty(metaData.PropertyName)
                                      .GetCustomAttributes(typeof(TAttribute), false).FirstOrDefault();

            if (returnValue != null)
                return returnValue;

            var mdt = metaData.GetMetadataType();
            if (mdt != null)
                returnValue = (TAttribute)mdt.MetadataClassType.GetProperty(metaData.PropertyName).GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault();

            return returnValue;
        }

        private static MetadataTypeAttribute GetMetadataType(this ModelMetadata metaData)
        {
            return (MetadataTypeAttribute) metaData.ContainerType.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
        }
    }
}
