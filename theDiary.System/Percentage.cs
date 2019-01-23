using System.Globalization;

namespace System
{
    public struct Percentage
        : IComparable<Percentage>,
        IEquatable<Percentage>
    {
        private Percentage(double lowerLimit, double upperLimit, byte accuracy)
        {
            if (lowerLimit >= upperLimit)
                throw new ArithmeticException("lowerLimit is greater than the upperLimit");

            this.LowerLimit = lowerLimit;
            this.UpperLimit = upperLimit;
            this.Accuracy = Math.Min((byte)accuracy, Percentage.AccuracyLimit);
            this.value = lowerLimit;
            this.progressNotification = null;
        }

        private Percentage(double lowerLimit, double upperLimit, byte accuracy, Action<Percentage> changeHandler)
            : this(lowerLimit, upperLimit, accuracy)
        {
            if (changeHandler == null)
                throw new ArgumentNullException("changeHandler");

            this.progressNotification = new Progress<Percentage>(changeHandler);
        }

        #region Public Fields

        public static readonly Percentage MaxValue = 100;
        public static readonly Percentage MinValue = 0;
        public static readonly Percentage Default = Percentage.FromRange(0, 100, 2);
        public static int DefaultAccuracy
        {
            get
            {
                return CultureInfo.CurrentCulture.NumberFormat.PercentDecimalDigits; 
            }
        }

        #endregion Public Fields

        #region Private Fields
        private const byte AccuracyLimit = 50;
        private const byte DefaultLowerLimit = 0;
        private const byte DefaultUpperLimit = 100;
        private readonly byte Accuracy;
        private readonly double LowerLimit;
        private IProgress<Percentage> progressNotification;
        private readonly double UpperLimit;
        private double value;

        #endregion Private Fields

        #region Private Properties

        private double Value
        {
            get
            {
                return this.value;
            }
            set
            {
                if (this.value.Equals(Math.Round(value, this.Accuracy)))
                    return;

                this.value = Math.Round(value, this.Accuracy);
                if (this.progressNotification != null)
                    this.progressNotification.Report(this);
            }
        }

        #endregion Private Properties

        #region Public Methods

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj"/> and this instance are the same type and represent the same value; otherwise, false. 
        /// </returns>
        /// <param name="obj">The object to compare with the current instance. </param>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public static Percentage Calculate(long currentValue, long maximumValue)
        {
            return new Percentage()
            {
                Value = (byte)(((float)currentValue / (float)maximumValue) * 100.00)
            };
        }

        #region Byte

        public static Percentage FromRange(byte lowerLimit, byte upperLimit, byte accuracy)
        {
            return new Percentage((double)lowerLimit, (double)upperLimit, (byte)Math.Max(0, (int)Math.Min(accuracy, Percentage.AccuracyLimit)));
        }
        #endregion

        #region Int
        public static Percentage FromRange(int upperLimit)
        {
            return Percentage.FromRange(Percentage.DefaultLowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }

        public static Percentage FromRange(int lowerLimit, int upperLimit)
        {
            return Percentage.FromRange(lowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }

        public static Percentage FromRange(int lowerLimit, int upperLimit, int accuracy)
        {
            return new Percentage((double)lowerLimit, (double)upperLimit, (byte)Math.Max(0, Math.Min(accuracy, (int)Percentage.AccuracyLimit)));
        }
        #endregion

        #region Long
        public static Percentage FromRange(long upperLimit)
        {
            return Percentage.FromRange(Percentage.DefaultLowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }

        public static Percentage FromRange(long lowerLimit, long upperLimit)
        {

            return Percentage.FromRange(lowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }

        public static Percentage FromRange(long lowerLimit, long upperLimit, int accuracy)
        {
            return new Percentage(lowerLimit, upperLimit, (byte)Math.Max(0, Math.Min(accuracy, (int)Percentage.AccuracyLimit)));
        }

        public static Percentage FromRange(long upperLimit, int accuracy)
        {
            return Percentage.FromRange(Percentage.DefaultLowerLimit, upperLimit, (byte)Math.Max(0, Math.Min(accuracy, (int)Percentage.AccuracyLimit)));
        }
        #endregion

        #region Float
        public static Percentage FromRange(float lowerLimit, float upperLimit, int accuracy)
        {

            return new Percentage(lowerLimit, upperLimit, (byte) Math.Max(0, Math.Min(accuracy, (int) Percentage.AccuracyLimit)));
        }

        public static Percentage FromRange(float lowerLimit, float upperLimit)
        {
            return Percentage.FromRange(lowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }

        public static Percentage FromRange(float upperLimit, int accuracy)
        {
            return Percentage.FromRange(Percentage.DefaultLowerLimit, upperLimit, accuracy);
        }

        public static Percentage FromRange(float upperLimit)
        {
            return Percentage.FromRange(Percentage.DefaultLowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }
        #endregion

        #region Double
        public static Percentage FromRange(double lowerLimit, double upperLimit, int accuracy)
        {
            return new Percentage(lowerLimit, upperLimit, (byte)Math.Max(0, Math.Min(accuracy, (int)Percentage.AccuracyLimit)));
        }

        public static Percentage FromRange(double lowerLimit, double upperLimit)
        {
            return Percentage.FromRange(lowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }

        public static Percentage FromRange(double upperLimit, int accuracy)
        {
            return Percentage.FromRange(Percentage.DefaultLowerLimit, upperLimit, (byte)Math.Max(0, Math.Min(accuracy, (int)Percentage.AccuracyLimit)));
        }

        public static Percentage FromRange(double upperLimit)
        {
            return Percentage.FromRange(Percentage.DefaultLowerLimit, upperLimit, Percentage.DefaultAccuracy);
        }
        #endregion

        #region Operators
        public static implicit operator int(Percentage value)
        {
            return Convert.ToInt32(value.Value);
        }

        public static implicit operator byte(Percentage value)
        {
            return Convert.ToByte(value.Value);
        }

        public static implicit operator long(Percentage value)
        {
            return Convert.ToInt64(value.Value);
        }

        public static implicit operator float(Percentage value)
        {
            return Convert.ToSingle(value.Value);
        }

        public static implicit operator double(Percentage value)
        {
            return Convert.ToDouble(value.Value);
        }

        public static implicit operator Percentage(int value)
        {
            return new Percentage()
            {
                Value = Convert.ToByte(Math.Max(Math.Min(byte.MaxValue, value), value))
            };
        }

        public static bool operator !=(Percentage a, Percentage b)
        {
            return a.Value != b.Value;
        }

        public static Percentage operator %(Percentage a, long value)
        {
            a.Calculate(value);
            return a;
        }

        public static Percentage operator %(Percentage a, int value)
        {
            a.Calculate(value);
            return a;
        }

        public static Percentage operator %(Percentage a, byte value)
        {
            a.Calculate(value);
            return a;
        }

        public static Percentage operator %(Percentage a, float value)
        {
            a.Calculate(value);
            return a;
        }

        public static Percentage operator %(Percentage a, double value)
        {
            a.Calculate(value);
            return a;
        }

        public static bool operator <(Percentage a, Percentage b)
        {
            return a.Value < b.Value;
        }

        public static bool operator ==(Percentage a, Percentage b)
        {
            return a.Value == b.Value;
        }

        public static bool operator >(Percentage a, Percentage b)
        {
            return a.Value > b.Value;
        }
        
        public int CompareTo(Percentage other)
        {
            return this.Value.CompareTo(other.Value);
        }

        public bool Equals(Percentage other)
        {
            return other.Value.Equals(this.Value);
        }

        public override int GetHashCode()
        {
            return this.Value.GetHashCode()
                | this.Accuracy.GetHashCode()
                | this.LowerLimit.GetHashCode()
                | this.UpperLimit.GetHashCode();
        }

        public override string ToString()
        {
            string valueFormat = "0";
            if (this.Accuracy > 0)
                valueFormat += ".".PadRight(this.Accuracy, '0');
            ;

            NumberFormatInfo numberFormat = CultureInfo.CurrentCulture.NumberFormat;
            numberFormat.PercentDecimalDigits = this.Accuracy;

            return this.Value.ToString("{0:p0}", numberFormat);
        }
        #endregion

        #endregion Public Methods

        #region Private Methods

        private void Calculate(byte currentValue)
        {
            this.Value = Math.Round((((double) this.GetSafeValue(currentValue) / this.GetSafeValue(this.UpperLimit)) * 100.00), this.Accuracy);
        }

        private void Calculate(int currentValue)
        {
            this.Value = Math.Round((((double) this.GetSafeValue(currentValue) / this.GetSafeValue(this.UpperLimit)) * 100.00), this.Accuracy);
        }

        private void Calculate(long currentValue)
        {
            this.Value = Math.Round((((double) this.GetSafeValue(currentValue) / this.GetSafeValue(this.UpperLimit)) * 100.00), this.Accuracy);
        }

        private void Calculate(float currentValue)
        {
            this.Value = Math.Round((((double) this.GetSafeValue(currentValue) / this.GetSafeValue(this.UpperLimit)) * 100.00), this.Accuracy);
        }

        private void Calculate(double currentValue)
        {
            this.Value = Math.Round(((this.GetSafeValue(currentValue) / this.GetSafeValue(this.UpperLimit)) * 100.00), this.Accuracy);
        }

        private double GetSafeValue(double value)
        {
            if (!this.UpperLimit.Equals(0))
                return value;

            return value + this.LowerLimit;
        }
        #endregion Private Methods
    }
}