using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Terminal.Animations
{
    /// <summary>
    /// A simple animation used in for performing a spinner animation.
    /// </summary>
    public class ConsoleSpiner
            : IConsoleAnimation
    {
        #region Constructors
        /// <summary>
        /// Initializes a new <see cref="ConsoleSpiner"/> instance at the location of the cursor in the console window.
        /// </summary>
        public ConsoleSpiner()
            : this(Location.CursorLocation)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleSpiner"/> class at the position specified by the <paramref name="left"/> and <paramref name="top"/> values.
        /// </summary>
        /// <param name="left">The position relative to the left of the window buffer.</param>
        /// <param name="top">The position relative to the top of the window buffer.</param>
        public ConsoleSpiner(int left, int top)
            : this(Location.New(left, top))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleSpiner"/> class at the specified <paramref name="location"/>.
        /// </summary>
        /// <param name="location">The location of the animation relative to the left and top of the window buffer.</param>
        public ConsoleSpiner(Location location)
            : base()
        {
            this.CurrentStep = 0;
            this.Location = location;
        }
        #endregion Constructors

        #region Private Declarations
        private ConsoleColor? backgroundColor;
        private ConsoleColor? foregroundColor;
        #endregion Private Declarations

        #region Public Read-Only Properties
        /// <summary>
        /// Gets the step that the <see cref="ConsoleSpiner"/> is currently on.
        /// </summary>
        public int CurrentStep { get; private set; }

        /// <summary>
        /// Gets the location of the <see cref="ConsoleSpiner"/>.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Gets or sets the foreground color used for the animation.
        /// </summary>
        public ConsoleColor ForegroundColor
        {
            get
            {
                if (this.foregroundColor.HasValue)
                    return this.foregroundColor.Value;

                return ConsoleApp.ForegroundColor;
            }
            set
            {
                if (this.foregroundColor == value)
                    return;

                this.foregroundColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the background color used for the animation.
        /// </summary>
        public ConsoleColor BackgroundColor
        {
            get
            {
                if (this.backgroundColor.HasValue)
                    return this.backgroundColor.Value;

                return ConsoleApp.BackgroundColor;
            }
            set
            {
                if (this.backgroundColor == value)
                    return;

                this.backgroundColor = value;
            }
        }
        #endregion Public Read-Only Properties

        #region Public Methods & Functions
        /// <summary>
        /// Renders the next step of the <see cref="ConsoleSpiner"/> animation.
        /// </summary>
        public void Step()
        {
            Location cursorLocation = Location.CursorLocation;
            ConsoleColor backgroundColor = ConsoleApp.BackgroundColor;
            ConsoleColor foregroundColor = ConsoleApp.ForegroundColor;

            this.CurrentStep++;
            ConsoleApp.SetCursorPosition(this.Location);
            switch (this.CurrentStep % 4)
            {
                case 0: ConsoleApp.Write("/");
                    break;
                case 1: ConsoleApp.Write("-");
                    break;
                case 2: ConsoleApp.Write("\\");
                    break;
                case 3: ConsoleApp.Write("|");
                    break;
            }

            if (this.foregroundColor.HasValue)
                ConsoleApp.ForegroundColor = foregroundColor;
            if (this.backgroundColor.HasValue)
                ConsoleApp.BackgroundColor = backgroundColor;
            ConsoleApp.SetCursorPosition(cursorLocation);
        }
        #endregion Public Methods & Functions
    }
}
