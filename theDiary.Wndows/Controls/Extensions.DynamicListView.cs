using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Controls
{
    internal static class DynamicListViewExtensions
    {
        public static string GetDisplayName(this PropertyInfo property)
        {
            ColumnNameAttribute columnName = property.GetAttribute<ColumnNameAttribute>();
            DisplayNameAttribute displayName = property.GetAttribute<DisplayNameAttribute>();

            if (displayName.IsNotNull())
                return displayName.DisplayName;
            if (columnName.IsNotNull())
                return columnName.ColumnName;

            return property.Name.AsReadable();
        }

        public static bool IsPropertyValid(this PropertyInfo property)
        {
            return property.CanWrite
                    && property.CanRead
                    && !property.HasAttribute<ColumnHiddenAttribute>();
        }

        public static PropertyInfoMetaDataCollection GetValidProperties(this DynamicListView instance, IEnumerable<Type> types)
        {
            PropertyInfoMetaDataCollection returnValue = new PropertyInfoMetaDataCollection();
            foreach (var type in types)
            {
                int ordinal = 0;
                foreach (var property in instance.GetValidProperties(type))
                {
                    returnValue.Add(property.Name, new PropertyInfoMetaData(property, ordinal));
                    ordinal++;
                }
            }

            return returnValue;
        }

        public static IEnumerable<PropertyInfo> GetValidProperties(this DynamicListView instance, Type type)
        {
            return type.GetProperties().AsParallel().Where(property => IsPropertyValid(property));
        }
    }

    internal class PropertyInfoMetaDataCollection
        : GroupedList<string, PropertyInfoMetaData>,
        IEnumerable<PropertyInfoMetaData>
    {
        public override void Add(string key, PropertyInfoMetaData value)
        {
            base.Add(key, value);
            value.NamedOrdinal = base[key].Count();
        }

        public new IEnumerator<PropertyInfoMetaData> GetEnumerator()
        {
            return base.Values.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return base.GetEnumerator();
        }
    }
    public interface IOrdinalMetaData
    {
        int ActualOrdinal { get; }

        int NamedOrdinal { get; }

        string SafeName { get; }
    }

    public class PropertyInfoMetaData
        : IOrdinalMetaData
    {
        public PropertyInfoMetaData(PropertyInfo property, int ordinal)
        {
            this.Property = property;
            this.ActualOrdinal = ordinal;
        }
        public PropertyInfo Property { get; set; }

        public int ActualOrdinal { get; private set; }

        public int NamedOrdinal { get; internal set; }

        public string SafeName
        {
            get
            {
                return string.Format("{0}{1}", this.Property.Name, this.NamedOrdinal);
            }
        }
        public static implicit operator PropertyInfo(PropertyInfoMetaData metaData)
        {
            return metaData.Property;
        }
    }
}
