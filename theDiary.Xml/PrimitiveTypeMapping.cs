using System;
using System.Collections.Generic;
using System.Linq;

namespace theDiary.Xml
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PrimitiveTypeMapping" /> structure.
    /// </summary>
    internal struct PrimitiveTypeMapping : IEquatable<Type>, IEquatable<string>, IEquatable<PrimitiveTypeMapping>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PrimitiveTypeMapping" /> structure.
        /// </summary>
        /// <param name="primitiveType">The <see cref="Type" /> used to initialize the instance.</param>
        /// <exception cref="ArgumentException">thrown if the <paramref name="type" /> is not primitive.</exception>
        /// <exception cref="ArgumentNullException">thrown if the <paramref name="type" /> is <c>Null</c>.</exception>
        public PrimitiveTypeMapping(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));
            if (!PrimitiveTypeMapping.IsPrimitiveType(type))
                throw new ArgumentException(nameof(type));

            this.PrimitiveType = type;
        }

        private static readonly Type StringType = typeof (string);

        private static bool IsPrimitiveType(Type type)
        {
            return type.IsPrimitive || type == PrimitiveTypeMapping.StringType;
        }

        /// <summary>
        ///     Gets the <see cref="Type" /> that was used to obtain this instance of <see cref="PrimitiveTypeMapping" />.
        /// </summary>
        public Type PrimitiveType
        {
            get;
        }

        /// <summary>
        ///     Gets the fully qualified name of the underlying primitive <see cref="Type" />, including its namespace but not its
        ///     assembly.
        /// </summary>
        public string FullName
        {
            get
            {
                return this.PrimitiveType.FullName;
            }
        }

        /// <summary>
        ///     Gets the name of the underlying primitive <see cref="Type" />.
        /// </summary>
        public string Name
        {
            get
            {
                return this.PrimitiveType.Name;
            }
        }

        public override int GetHashCode()
        {
            return this.PrimitiveType.GetHashCode();
        }

        public bool Equals(Type obj)
        {
            return this.PrimitiveType.Equals(obj);
        }

        public bool Equals(PrimitiveTypeMapping obj)
        {
            return this.PrimitiveType.Equals(obj.PrimitiveType);
        }

        public bool Equals(string name)
        {
            return this.Equals(name, FindNameBy.Both, StringComparison.Ordinal);
        }

        public bool Equals(string name, StringComparison comparisonType)
        {
            return this.Equals(name, FindNameBy.Both, comparisonType);
        }

        public bool Equals(string name, FindNameBy findOn)
        {
            return this.Equals(name, findOn, StringComparison.Ordinal);
        }

        public bool Equals(string name, FindNameBy findOn, StringComparison comparisonType)
        {
            switch (findOn)
            {
                case FindNameBy.FullName:
                    return this.FullName.Equals(name, comparisonType);
                case FindNameBy.Name:
                    return this.Name.Equals(name, comparisonType);
                case FindNameBy.Both:
                default:
                    return this.FullName.Equals(name, comparisonType) || this.Name.Equals(name, comparisonType);
            }
        }
    }
}
