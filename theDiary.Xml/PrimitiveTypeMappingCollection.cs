using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theDiary.Xml
{
    internal class PrimitiveTypeMappings
        : System.Collections.Generic.UniqueList<PrimitiveTypeMapping>
    {
        public PrimitiveTypeMappings()
            : base()
        {

            this.AddTypes(typeof (int).Assembly.GetTypes().Where(type => type.IsPrimitive || type.Equals(typeof (string))));
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveTypeMappings"/> class.
        /// </summary>
        /// <param name="items">Sequence of <paramref name="items"/> to add.</param>
        private PrimitiveTypeMappings(IEnumerable<Type> items)
            : base()
        {
            this.AddTypes(items);
        }
        private void AddTypes(IEnumerable<Type> items)
        {
            this.AddRange(items.Select<Type, PrimitiveTypeMapping>(item => new PrimitiveTypeMapping(item)));
        }

        public Type this[string typeName]
        {
            get
            {
                if (!this.Contains(typeName))
                    throw new ArgumentOutOfRangeException(nameof(typeName));

                return this.FirstOrDefault(primitiveType => primitiveType.Equals(typeName)).PrimitiveType;
            }
        }

        public bool Contains(Type type)
        {
            return this.Any(primitiveType => primitiveType.Equals(type));
        }
        public bool Contains(string typeName)
        {
            return this.Any(primitiveType => primitiveType.Equals(typeName));
        }

        public bool Contains(string typeName, FindNameBy findOn)
        {
            return this.Any(primitiveType => primitiveType.Equals(typeName, findOn));
        }

        public bool Contains(string typeName, StringComparison comparisonType)
        {
            return this.Any(primitiveType => primitiveType.Equals(typeName, comparisonType));
        }
        public bool Contains(string typeName, FindNameBy findOn, StringComparison comparisonType)
        {
            return this.Any(primitiveType => primitiveType.Equals(typeName, findOn, comparisonType));
        }
    }

    public enum FindNameBy
    {
        FullName = 0,

        Name = 1,

        Both = 2
    }

}
