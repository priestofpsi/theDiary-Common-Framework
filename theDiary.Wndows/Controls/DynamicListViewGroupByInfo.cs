using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace System.Windows.Forms.Controls
{
    public sealed class DynamicListViewGroupByInfo
    {
        public DynamicListViewGroupByInfo()
        {
        }

        public DynamicListViewGroupByInfo(PropertyInfo property)
            : this()
        {
            this.AssociatedProperty = property;
            this.Text = property.Name.AsReadable(ReadablilityCondition.Default);
        }

        public string Text { get; private set; }

        public PropertyInfo AssociatedProperty { get; private set; }


    }
}
