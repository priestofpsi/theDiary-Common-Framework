using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Terminal
{
    /// <summary>
    /// Represents a left and top position pairing used by a <see cref="ConsoleApp"/>.
    /// </summary>
    public struct Location
    {
        #region Constructors
        private Location(int left, int top)
        {
            if (left < 0)
                throw new ArgumentOutOfRangeException("left");

            if (top < 0)
                throw new ArgumentOutOfRangeException("top");

            this.left = left;
            this.top = top;
        }
        #endregion Constructors

        #region Private Declarations
        private int left;
        private int top;
        #endregion Private Declarations

        #region Public Properties
        public int Left
        {
            get
            {
                return this.left;
            }
            set
            {
                if (value >= 0 && value <= Console.WindowWidth)
                    this.left = value;
            }
        }

        public int Top
        {
            get
            {
                return this.top;
            }
            set
            {
                if (value >= 0 && value <= Console.WindowHeight)
                    this.top = value;
            }
        }
        #endregion

        #region Public Methods & Functions
        /// <summary>
        /// Gets the string representation of the <see cref="Location"/>.
        /// </summary>
        /// <returns>A <see cref="String"/> representing the location.</returns>
        public override string ToString()
        {
            return string.Format("Left={0}, Top={1}", this.Left, this.Top);
        }
        #endregion Public Methods & Functions

        #region Public Static Properties
        /// <summary>
        /// Gets a <see cref="Location"/> structure that has a Top and Left value of 0.
        /// </summary>
        public static readonly Location Empty = new Location(0, 0);

        /// <summary>
        /// Gets the location of the cursor in the console window.
        /// </summary>
        public static Location CursorLocation
        {
            get
            {
                return System.Terminal.ConsoleApp.CursorLocation;
            }
        }

        /// <summary>
        /// Gets the location of the console window.
        /// </summary>
        public static Location WindowLocation
        {
            get
            {
                return System.Terminal.ConsoleApp.WindowLocation;
            }
        }
        #endregion

        #region Public Static Methods & Functions
        /// <summary>
        /// Creates a location from the specified <paramref name="left"/> and <paramref name="top"/> values.
        /// </summary>
        /// <param name="left">The value specifying the left location.</param>
        /// <param name="top">The value specifying the top location.</param>
        /// <returns>A <see cref="Location"/> at the specified co-ordinates.</returns>
        public static Location New(int left, int top)
        {
            return new Location(left, top);
        }
        #endregion
    }
}
