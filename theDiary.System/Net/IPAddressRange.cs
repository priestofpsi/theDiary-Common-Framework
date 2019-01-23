using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace System.Net
{
    /// <summary>
    /// Contains a sequence of <see cref="IPAddress"/> elements that define an upper and lower range.
    /// </summary>
    public class IPAddressRange
        : IEnumerable<IPAddress>,
        IEquatable<IPAddress>,
        IComparable<IPAddress>,
        IComparable<IPAddressRange>,
        IEquatable<IPAddressRange>
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="IPAddressRange"/> class, containing a single <see cref="IPAddress"/> as the upper
        /// and lower range limits.
        /// </summary>
        /// <param name="ipAddress"><see cref="IPAddress"/> instance defining the range.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="ipAddress"/> is <c>Null</c>.</exception>
        public IPAddressRange(IPAddress ipAddress)
            : this(ipAddress, ipAddress)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IPAddressRange"/> class, containing the <paramref name="lower"/> and <paramref name="upper"/> range limts.
        /// </summary>
        /// <param name="lower"><see cref="IPAddress"/> instance defining the lower range.</param>
        /// <param name="upper"><see cref="IPAddress"/> instance defining the upper range.</param>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="lower"/>, or <paramref name="upper"/> is <c>Null</c>.</exception>
        public IPAddressRange(IPAddress lower, IPAddress upper)
            : base()
        {
            if (lower.IsNull())
                throw new ArgumentNullException("lower");

            if (upper.IsNull())
                throw new ArgumentNullException("upper");

            this.addressFamily = lower.AddressFamily;
            this.lowerBytes = lower.GetAddressBytes();
            this.upperBytes = upper.GetAddressBytes();
        }
        #endregion

        #region Private Constant Declarations
        private static readonly RegExPattern IPAddressPattern = RegExPattern.IPv4Address;
        private static readonly IPAddress emptyIPAddress = new IPAddress(new byte[] { 0, 0, 0, 0 });
        private static readonly IPAddressRange emptyIPAddressRange = new IPAddressRange(IPAddressRange.emptyIPAddress);
        #endregion

        #region Private Declarations
        private readonly AddressFamily addressFamily;
        private readonly byte[] lowerBytes;
        private readonly byte[] upperBytes;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the lowest permissable <see cref="IPAddress "/>.
        /// </summary>
        public IPAddress Lower
        {
            get
            {
                return new IPAddress(this.lowerBytes);
            }
        }

        /// <summary>
        /// Gets the highest permissable <see cref="IPAddress"/>.
        /// </summary>
        public IPAddress Upper
        {
            get
            {
                return new IPAddress(this.upperBytes);
            }
        }

        /// <summary>
        /// Gets the <see cref="AddressFamily"/> for the <see cref="IPAddressRange"/>.
        /// </summary>
        public AddressFamily AddressFamily
        {
            get
            {
                return this.addressFamily;
            }
        }
        #endregion

        #region Private Read-Only Properties
        /// <summary>
        /// Determines if the current <see cref="IPAddressRange"/> instance is Empty.
        /// </summary>
        private bool IsEmpty
        {
            get
            {
                return (this.Lower.Equals(IPAddressRange.EmptyIPAddress)
                    && this.Upper.Equals(IPAddressRange.EmptyIPAddress));
            }
        }
        #endregion

        #region Public Static Read-Only Properties
        /// <summary>
        /// Returns an Empty instance of a <see cref="IPAddressRange"/>.
        /// </summary>
        public static IPAddressRange Empty
        {
            get
            {
                return IPAddressRange.emptyIPAddressRange;
            }
        }

        /// <summary>
        /// Returns an Empty instance of a <see cref="IPAddress"/>.
        /// </summary>
        public static IPAddress EmptyIPAddress
        {
            get
            {
                return IPAddressRange.emptyIPAddress;
            }
        }
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Determines if a specified <see cref="IPAddress"/> is within the allowed range.
        /// </summary>
        /// <param name="ipAddress">The <see cref="IPAddress"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="ipAddress"/> is within the allowed range; otherwise <c>False</c>.</returns>
        public bool IsInRange(IPAddress ipAddress)
        {
            if (ipAddress.IsNull())
                throw new ArgumentNullException("ipAddress");

            if (this.IsEmpty
                || (this.Lower.Equals(this.Upper) && this.Lower.Equals(ipAddress)))
                return true;

            if (ipAddress.AddressFamily != addressFamily)
                return false;

            byte[] addressBytes = ipAddress.GetAddressBytes();
            bool lowerBoundary = true, upperBoundary = true;

            for (int i = 0; i < this.lowerBytes.Length &&
                (lowerBoundary || upperBoundary); i++)
            {
                if ((lowerBoundary && addressBytes[i] < this.lowerBytes[i]) ||
                    (upperBoundary && addressBytes[i] > this.upperBytes[i]))
                    return false;

                lowerBoundary &= (addressBytes[i] == this.lowerBytes[i]);
                upperBoundary &= (addressBytes[i] == this.upperBytes[i]);
            }

            return true;
        }

        /// <summary>
        /// Gets an enumerator used to iterate through the range of valid <see cref="IPAddress"/> elements.
        /// </summary>
        /// <returns>Sequence of <see cref="IPAddress"/> elements within the defined range.</returns>
        public IEnumerator<IPAddress> GetEnumerator()
        {
            IPAddress ipAddress = this.Lower;
            yield return ipAddress;

            while (this.IsInRange(IPAddressRange.Increment(ipAddress)))
                yield return ipAddress;
        }

        /// <summary>
        /// Gets an enumerator used to iterate through the range of valid <see cref="IPAddress"/> elements.
        /// </summary>
        /// <returns>Sequence of <see cref="IPAddress"/> elements within the defined range.</returns>
        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /// <summary>
        /// Gets the hash code for this instance.
        /// </summary>
        /// <returns>A <see cref="int"/> hash code value.</returns>
        public override int GetHashCode()
        {
            return this.lowerBytes.GetHashCode()
                | this.upperBytes.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="Object"/> is equal to the current <see cref="IPAddressRange"/>.
        /// </summary>
        /// <param name="obj">The <see cref="Object"/> to compare with the current <see cref="Object"/>.</param>
        /// <returns><c>True</c> if the specified <see cref="Object"/> is equal to the current <see cref="IPAddressRange"/>; otherwise, <c>False</c>.</returns>
        public override bool Equals(object obj)
        {
            IPAddressRange ipRange = obj as IPAddressRange;
            if (ipRange.IsNotNull())
                return this.Equals(ipRange);

            IPAddress ipAddress = obj as IPAddress;
            if (ipAddress.IsNotNull())
                return this.Equals(ipAddress);

            return base.Equals(obj);
        }

        /// <summary>
        /// Determines whether the specified <see cref="IPAddress"/> is in the range covered by the current <see cref="IPAddressRange"/>.
        /// </summary>
        /// <param name="other">he <see cref="IPAddress"/> to compare with the current <see cref="IPAddressRange"/>.</param>
        /// <returns><c>True</c> if the <see cref="IPAddress"/> is in range; otherwise <c>False</c>.</returns>
        public bool Equals(IPAddress other)
        {
            if (other.IsNull())
                return false;

            return this.IsInRange(other);
        }

        /// <summary>
        /// Determines whether the specified <see cref="IPAddressRange"/> is equal to the current <see cref="IPAddressRange"/>.
        /// </summary>
        /// <param name="other">he <see cref="IPAddressRange"/> to compare with the current <see cref="IPAddressRange"/>.</param>
        /// <returns><c>True</c> if the <see cref="IPAddressRange"/> is in equal; otherwise <c>False</c>.</returns>
        public bool Equals(IPAddressRange other)
        {
            if (other.IsNull())
                return false;

            return this.Lower.Equals(other.Lower)
                && this.Upper.Equals(other.Upper)
                && this.AddressFamily.Equals(other.AddressFamily);
        }

        /// <summary>
        /// Compares the current <see cref="IPAddressRange"/> with another <see cref="IPAddress"/>.
        /// </summary>
        /// <param name="other">An <see cref="IPAddress"/> to compare with this instance.</param>
        /// <returns> A value that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:<para/>
        /// Value Meaning Less than zero: This instance is less than the <paramref name="other"/> parameter.<para/>
        /// Zero: This instance is equal to <paramref name="other"/>.<para/>
        /// Greater than zero: This instance is greater than <paramref name="other"/>.</returns>
        public int CompareTo(IPAddress other)
        {
            if (other.IsNull())
                throw new ArgumentNullException("other");

            if (this.IsInRange(other))
            {
                return 0;

            }
            else if (this.Compare(other, this.Lower) < 0)
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// Compares the current <see cref="IPAddressRange"/> with another <see cref="IPAddressRange"/>.
        /// </summary>
        /// <param name="other">An <see cref="IPAddressRange"/> to compare with this instance.</param>
        /// <returns> A value that indicates the relative order of the objects being compared.
        /// The return value has the following meanings:<para/>
        /// Value Meaning Less than zero: This instance is less than the <paramref name="other"/> parameter.<para/>
        /// Zero: This instance is equal to <paramref name="other"/>.<para/>
        /// Greater than zero: This instance is greater than <paramref name="other"/>.</returns>
        public int CompareTo(IPAddressRange other)
        {
            if (other.IsNull())
                throw new ArgumentNullException("other");

            if (!this.Equals(other))
            {
                if (this.Compare(other.Lower, this.Lower) < 0)
                {
                    return -1;
                }
                else if (this.Compare(other.Upper, this.Upper) > 0)
                {
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 0;
        }
        #endregion

        #region Private Methods & Functions
        private int Compare(IPAddress x, IPAddress y)
        {
            int retVal = 0;
            byte[] bytesX = x.GetAddressBytes();
            byte[] bytesY = y.GetAddressBytes();

            for (int i = 0; (i < bytesX.Length) && (i < bytesY.Length); i++)
            {
                retVal = bytesX[i].CompareTo(bytesY[i]);
                if (0 != retVal)
                    break;
            }
            return retVal;
        }
        #endregion

        #region Public Static Methods & Functions
        /// <summary>
        ///  Converts the string representation to its <see cref="IPAddressRange"/> equivalent.
        /// </summary>
        /// <param name="ipAddress">A string containing an IPAddress range to convert.</param>
        /// <returns>The <see cref="IPAddressRange"/> value equivalent to the content contained in <paramref name="ipAddress"/>.</returns>
        public static IPAddressRange Parse(string ipAddress)
        {
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ArgumentNullException("ipAddress");

            if (IPAddressRange.IPAddressPattern != ipAddress)
                throw new ArgumentException("Invalid IPAddress specified.", "ipAddress");

            byte[] lower = new byte[4];
            byte[] upper = new byte[4];
            string[] subs = ipAddress.Split('.');

            for (int i = 0; i < subs.Length; i++)
            {
                var sub = subs[i];
                if (sub.Equals("*"))
                {
                    lower[i] = byte.MinValue;
                    upper[i] = byte.MaxValue;
                }
                else
                {
                    lower[i] = byte.Parse(sub);
                    upper[i] = byte.Parse(sub);
                }
            }
            return new IPAddressRange(new IPAddress(lower), new IPAddress(upper));
        }

        /// <summary>
        /// Converts the string representation to its <see cref="IPAddressRange"/> equivalent.
        /// </summary>
        /// <param name="ipAddressLower">A string containing the lower limit of an IPAddress range to convert.</param>
        /// <param name="ipAddressUpper">A string containing the upper limit of an IPAddress range to convert.</param>
        /// <returns>The <see cref="IPAddressRange"/> value equivalent to the content contained in <paramref name="ipAddressLower"/> and <paramref name="ipAddressUpper"/>.</returns>
        public static IPAddressRange Parse(string ipAddressLower, string ipAddressUpper)
        {

            if (string.IsNullOrWhiteSpace(ipAddressLower))
                throw new ArgumentNullException("ipAddressLower");

            if (IPAddressRange.IPAddressPattern != ipAddressLower)
                throw new ArgumentException("Invalid IPAddress specified.", "ipAddressLower");

            if (ipAddressUpper.IsNull())
                return IPAddressRange.Parse(ipAddressLower);


            if (string.IsNullOrWhiteSpace(ipAddressUpper))
                throw new ArgumentNullException("ipAddressUpper");

            if (IPAddressRange.IPAddressPattern != ipAddressUpper)
                throw new ArgumentException("Invalid IPAddress specified.", "ipAddressUpper");

            byte[] lower = new byte[4];
            byte[] upper = new byte[4];
            string[] subs = ipAddressLower.Split('.');

            for (int i = 0; i < subs.Length; i++)
                lower[i] = (subs[i].Equals("*")) ? byte.MinValue : byte.Parse(subs[i]);

            subs = ipAddressUpper.Split('.');
            for (int i = 0; i < subs.Length; i++)
                upper[i] = (subs[i].Equals("*")) ? byte.MaxValue : byte.Parse(subs[i]);

            return new IPAddressRange(new IPAddress(lower), new IPAddress(upper));
        }
        #endregion

        #region Private Static Methods & Functions
        private static IPAddress Increment(IPAddress address)
        {
            IPAddress result;

            byte[] bytes = address.GetAddressBytes();

            for (int k = bytes.Length - 1; k >= 0; k--)
            {
                if (bytes[k] == byte.MaxValue)
                {
                    bytes[k] = 0;
                    continue;
                }

                bytes[k]++;

                result = new IPAddress(bytes);
                return result;
            }

            return null;
        }
        #endregion

        #region Public Static Operators
        /// <summary>
        /// Determines if a <see cref="IPAddress"/> is within the bounds specified by a <see cref="IPAddressRange"/> instance.
        /// </summary>
        /// <param name="ipAddressRange">The <see cref="IPAddressRange"/> to compare against.</param>
        /// <param name="ipAddress">The <see cref="IPAddress"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="ipAddress"/> is within the bounds; otherwise <c>False</c>.</returns>
        public static bool operator ==(IPAddressRange ipAddressRange, IPAddress ipAddress)
        {
            if (ipAddressRange.IsNull())
                throw new ArgumentNullException("ipAddressRange");
            if (ipAddress.IsNull())
                throw new ArgumentNullException("ipAddress");

            return ipAddressRange.IsInRange(ipAddress);
        }

        /// <summary>
        /// Determines if a <see cref="IPAddress"/> is not within the bounds specified by a <see cref="IPAddressRange"/> instance.
        /// </summary>
        /// <param name="ipAddressRange">The <see cref="IPAddressRange"/> to compare against.</param>
        /// <param name="ipAddress">The <see cref="IPAddress"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="ipAddress"/> is not within the bounds; otherwise <c>False</c>.</returns>
        public static bool operator !=(IPAddressRange ipAddressRange, IPAddress ipAddress)
        {
            if (ipAddressRange.IsNull())
                throw new ArgumentNullException("ipAddressRange");
            if (ipAddress.IsNull())
                throw new ArgumentNullException("ipAddress");

            return !ipAddressRange.IsInRange(ipAddress);
        }

        /// <summary>
        /// Determines if a <see cref="IPAddress"/> is out of the lower bounds specified by a <see cref="IPAddressRange"/> instance.
        /// </summary>
        /// <param name="ipAddressRange">The <see cref="IPAddressRange"/> to compare against.</param>
        /// <param name="ipAddress">The <see cref="IPAddress"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="ipAddress"/> is out of the lower bounds; otherwise <c>False</c>.</returns>
        public static bool operator <(IPAddressRange ipAddressRange, IPAddress ipAddress)
        {
            if (ipAddressRange.IsNull())
                throw new ArgumentNullException("ipAddressRange");
            if (ipAddress.IsNull())
                throw new ArgumentNullException("ipAddress");

            byte[] addressBytes = ipAddress.GetAddressBytes();
            bool lowerBoundary = true;

            for (int i = 0; i < ipAddressRange.lowerBytes.Length &&
                lowerBoundary; i++)
            {
                if (lowerBoundary && addressBytes[i] < ipAddressRange.lowerBytes[i])
                    return true;

                lowerBoundary &= (addressBytes[i] == ipAddressRange.lowerBytes[i]);
            }
            return false;
        }

        /// <summary>
        /// Determines if a <see cref="IPAddress"/> is out of the upper bounds specified by a <see cref="IPAddressRange"/> instance.
        /// </summary>
        /// <param name="ipAddressRange">The <see cref="IPAddressRange"/> to compare against.</param>
        /// <param name="ipAddress">The <see cref="IPAddress"/> to check.</param>
        /// <returns><c>True</c> if the <paramref name="ipAddress"/> is out of the upper bounds; otherwise <c>False</c>.</returns>
        public static bool operator >(IPAddressRange ipAddressRange, IPAddress ipAddress)
        {
            if (ipAddressRange.IsNull())
                throw new ArgumentNullException("ipAddressRange");
            if (ipAddress.IsNull())
                throw new ArgumentNullException("ipAddress");

            byte[] addressBytes = ipAddress.GetAddressBytes();
            bool upperBoundary = true;

            for (int i = 0; i < ipAddressRange.upperBytes.Length &&
                upperBoundary; i++)
            {
                if (upperBoundary && addressBytes[i] < ipAddressRange.upperBytes[i])
                    return true;

                upperBoundary &= (addressBytes[i] == ipAddressRange.upperBytes[i]);
            }
            return false;
        }
        #endregion
    }
}
