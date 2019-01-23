using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace System
{
    /// <summary>
    /// Helper class used to parse <see cref="String"/> to <see cref="Enum"/> types.
    /// This class can not be inherited.
    /// </summary>
    public static class EnumHelper
    {
        #region Private Constant Declarations
        private const string InvalidEnumTypeMessage = "The specified Type is not an enum type.";
        private const string InvalidTEnumMessage = "The specified Type ('TEnum') is not an enum type.";
        private const string InvalidValueMessage = "The specified parameter 'value' is Null, empty or contains only whitespace.";
        private const string ValueNotValidMessage = "The specified value is outside the range of the underlying type of enumType.";
        #endregion

        #region Private Static Declarations
        private static readonly Dictionary<Type, EnumRange> enumRanges = new Dictionary<Type, EnumRange>();
        #endregion

        #region Public Static Methods & Functions 
        public static ValueType ToUnderlyingType<TEnum>(TEnum value)
            where TEnum : struct
        {
            return (ValueType) Convert.ChangeType(value, Enum.GetUnderlyingType(typeof(TEnum)));
        }

        public static ValueType ToUnderlyingType(object value)
        {
            return (ValueType) Convert.ChangeType(value, Enum.GetUnderlyingType(value.GetType()));
        }

        #region Parse Methods & Functions
        public static dynamic Parse(Type enumType, string value)
        {
            if (!EnumHelper.IsEnum(enumType))
                throw new ArgumentException(EnumHelper.InvalidEnumTypeMessage);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(EnumHelper.InvalidValueMessage);

            return Enum.Parse(enumType, value);
        }

        public static dynamic Parse(Type enumType, dynamic value)
        {
            if (!EnumHelper.IsEnum(enumType))
                throw new ArgumentException(EnumHelper.InvalidEnumTypeMessage);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(EnumHelper.InvalidValueMessage);

            if (Enum.IsDefined(enumType, value))
                return value;

            Type underlyingEnumType = Enum.GetUnderlyingType(enumType);
            Type valueType = value.GetType();
            object eValue = Convert.ChangeType(value, underlyingEnumType);

            if (Enum.IsDefined(enumType, eValue))
                return eValue;

            throw new OverflowException(EnumHelper.ValueNotValidMessage);
        }

        public static dynamic Parse(Type enumType, string value, StringComparison comparisonType)
        {
            if (!EnumHelper.IsEnum(enumType))
                throw new ArgumentException(EnumHelper.InvalidEnumTypeMessage);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(EnumHelper.InvalidValueMessage);

            foreach (var enumName in Enum.GetNames(enumType))
                if (enumName.Equals(value, comparisonType))
                    return EnumHelper.Parse(enumType, value);

            throw new OverflowException(EnumHelper.ValueNotValidMessage);
        }

        public static TEnum Parse<TEnum>(string value)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(EnumHelper.InvalidValueMessage);

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        public static TEnum Parse<TEnum>(string value, TEnum defaultValue)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            TEnum returnValue;
            if (!Enum.TryParse<TEnum>(value, out returnValue))
                returnValue = defaultValue;

            return (TEnum)Enum.Parse(typeof(TEnum), value);
        }

        public static TEnum Parse<TEnum>(string value, StringComparison comparisonType)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (!string.IsNullOrWhiteSpace(value))
            {
                Type enumType = typeof(TEnum);
                foreach (var enumName in Enum.GetNames(enumType))
                    if (enumName.Equals(value, comparisonType))
                        return EnumHelper.Parse<TEnum>(value);
            }
            throw new OverflowException("value is outside the range of the underlying type of enumType.");
        }

        public static TEnum Parse<TEnum>(string value, StringComparison comparisonType, TEnum defaultValue)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (string.IsNullOrWhiteSpace(value))
                return defaultValue;

            Type enumType = typeof(TEnum);
            foreach (var enumName in Enum.GetNames(enumType))
                if (enumName.Equals(value, comparisonType))
                    return EnumHelper.Parse<TEnum>(value);

            return defaultValue;

        }

        public static bool TryParse<TEnum>(string value, out TEnum returnValue)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            try
            {
                returnValue = EnumHelper.Parse<TEnum>(value);
                return true;
            }
            catch
            {
                returnValue = default(TEnum);
                return false;
            }
        }

        public static bool TryParse<TEnum>(string value, StringComparison comparisonType, out TEnum returnValue)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException("value");

            try
            {
                returnValue = EnumHelper.Parse<TEnum>(value, comparisonType);
                return true;
            }
            catch
            {
                returnValue = default(TEnum);
                return false;
            }
        }
        #endregion

        #region Enum Attribute Methods & Functions
        public static IEnumerable<TOut> GetAttributes<TEnum, TOut>()
            where TEnum : struct
            where TOut : Attribute
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            return typeof(TEnum).GetAttributes<TOut>();
        }

        public static IEnumerable<TOut> GetAttributes<TEnum, TOut>(Predicate<TOut> predicate)
            where TEnum : struct
            where TOut : Attribute
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            return typeof(TEnum).GetAttributes<TOut>(predicate);
        }

        public static TOut GetAttribute<TEnum, TOut>(Predicate<TOut> predicate, out TEnum value)
            where TEnum : struct
            where TOut : Attribute
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            var enumType = typeof(TEnum);
            foreach (var enumName in Enum.GetNames(enumType))
            {
                var memInfo = enumType.GetMember(enumName).FirstOrDefault();
                if (memInfo != null
                    && memInfo.GetAttributes<TOut>(predicate).FirstOrDefault() != null)
                {
                    value = EnumHelper.Parse<TEnum>(enumName);
                    break;
                }
            }

            value = default(TEnum);

            return null;
        }
        #endregion

        public static TEnum ValueOf<TEnum>(object value)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (value.IsNull())
                throw new ArgumentNullException("value");

            TEnum result;

            Type valueType = value.GetType();
            Type enumType = Enum.GetUnderlyingType(typeof(TEnum));
            try
            {
                if (valueType == typeof(string))
                {
                    if (EnumHelper.TryParseAny<TEnum>((string)value, out result))
                        return result;
                }
                else
                {
                    EnumRange range = EnumHelper.enumRanges[enumType];
                    if (range.Converter.CanConvertFrom(valueType))
                        return (TEnum)range.Converter.ConvertFrom(value);
                }
                throw new ArgumentException();
            }
            catch (InvalidCastException cex)
            {
                throw new ArgumentOutOfRangeException(string.Format("The value '{0}' is not a valid value of  '{1}'.", value, typeof(TEnum).FullName), cex);
            }
            catch (InvalidOperationException oex)
            {
                throw new ArgumentOutOfRangeException(string.Format("The value '{0}' is not a valid value of  '{1}'.", value, typeof(TEnum).FullName), oex);
            }
            catch (Exception ex)
            {
                throw new ArgumentOutOfRangeException(string.Format("The value '{0}' is not a valid value of  '{1}'.", value, typeof(TEnum).FullName), ex);
            }
        }

        public static bool TryValueOf<TEnum>(object value, out TEnum result)
            where TEnum : struct
        {
            if (!EnumHelper.IsEnum<TEnum>())
                throw new ArgumentException(EnumHelper.InvalidTEnumMessage);

            if (value.IsNull())
                throw new ArgumentNullException("value");

            result = default(TEnum);
            try
            {
                result = EnumHelper.ValueOf<TEnum>(value);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region Private Static Methods & Functions
        private static bool TryParseAny<TEnum>(string value, out TEnum returnValue)
            where TEnum : struct
        {
            Type enumType = typeof(TEnum);
            double numericValue;
            if (EnumHelper.TryParse<TEnum>((string)value, out returnValue)
                || (double.TryParse((string)value, out numericValue)
                    && EnumHelper.TryChange<TEnum>(EnumHelper.enumRanges[enumType], numericValue, out returnValue)))
                return true;

            return false;
        }

        private static bool TryChange<TEnum>(EnumRange range, double value, out TEnum result)
            where TEnum : struct
        {
            result = default(TEnum);
            bool returnValue = false;
            try
            {
                if (range.IsValid(value))
                {
                    result = (TEnum)range.Converter.ConvertFrom(value);
                    returnValue = true;
                }
            }
            catch (NotSupportedException)
            {
                result = default(TEnum);
            }
            return returnValue;
        }
        
        /// <summary>
        /// Determines if the specified <paramref name="enumType"/> is an Enum
        /// </summary>
        /// <param name="enumType"><see cref="Type"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="enumType"/> is an enum; otherwise <c>False</c>.</returns>
        private static bool IsEnum(Type enumType)
        {
            if (!EnumHelper.enumRanges.ContainsKey(enumType) && enumType.IsEnum)
                EnumHelper.enumRanges.Add(enumType, EnumRange.Create(enumType));

            return EnumHelper.enumRanges.ContainsKey(enumType);
        }

        /// <summary>
        /// Determines if the specified <typeparamref name="TEnum"/> is an Enum
        /// </summary>
        /// <typeparam name="TEnum">The <see cref="Type"/> to check.</typeparam>
        /// <returns><c>True</c> if the <typeparamref name="TEnum"/> is an enum; otherwise <c>False</c>.</returns>
        private static bool IsEnum<TEnum>()
            where TEnum : struct
        {
            return EnumHelper.IsEnum(typeof(TEnum));
        }
        #endregion

        #region Private Classes
        private class EnumRange<T>
            : EnumRange
            where T : struct, IComparable<T>, IComparable
        {
            #region Constructors
            public EnumRange(Type enumType)
                : base(enumType)
            {
                List<T> values = new List<T>();
                foreach (T value in Enum.GetValues(enumType))
                {
                    if (!this.min.HasValue
                        || this.min.Value.CompareTo(value) > 0)
                        this.min = value;

                    if (!this.max.HasValue
                        || this.max.Value.CompareTo(value) < 0)
                        this.max = value;

                    values.Add(value);
                }
                this.Values = values.ToArray();
            }
            #endregion

            #region Private Declarations
            private T? min = null;
            private T? max = null;
            #endregion

            #region Public Properties
            public new T Min
            {
                get
                {
                    return this.min.Value;
                }
            }

            public new T Max
            {
                get
                {
                    return this.max.Value;
                }
            }
            
            public new T[] Values { get; private set; }
            #endregion
        }

        private abstract class EnumRange
            : IComparable<ValueType>
        {
            #region Constructors
            protected EnumRange(Type enumType)
            {
                this.Converter = TypeDescriptor.GetConverter(Enum.GetUnderlyingType(enumType));
                this.EnumType = enumType;
            }
            #endregion

            #region Public Properties
            public virtual IComparable Min { get; protected set; }

            public virtual IComparable Max { get; protected set; }

            public virtual IComparable[] Values { get; protected set; }

            public TypeConverter Converter { get; protected set; }

            public Type UnderlyingType { get; protected set; }

            public Type EnumType { get; protected set; }
            #endregion

            #region Public Methods & Functions
            public bool IsValid(IComparable value)
            {
                return this.Values.Contains(value);
            }

            public int CompareTo(ValueType obj)
            {
                bool min, max;
                min = this.Min.CompareTo(obj) >= 0;
                max = this.Max.CompareTo(obj) <= 0;
                if (!min)
                {
                    return -1;
                }
                else if (!max)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            #endregion

            #region Public Static Methods & Functions
            public static EnumRange Create(Type enumType)
            {
                return (EnumRange)System.Activator.CreateInstance(typeof(EnumRange<>).MakeGenericType(Enum.GetUnderlyingType(enumType)), enumType);
            }

            public static EnumRange Create<TEnum>()
                where TEnum : struct
            {
                return EnumRange.Create(typeof(TEnum));
            }
            #endregion
        }
        #endregion
    }
}
