using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Terminal.Animations
{
    /// <summary>
    /// Defines the functionality required for performing animations in a console window.
    /// </summary>
    public interface IConsoleAnimation
    {
        #region Property Definitions
        /// <summary>
        /// Gets the step that the animation is currently on.
        /// </summary>
        int CurrentStep { get; }

        /// <summary>
        /// Gets the starting location that the animation is currently on.
        /// </summary>
        Location Location { get; }
        #endregion

        #region Method & Funtion Definitions
        /// <summary>
        /// Moves the animation to the next available step.
        /// </summary>
        void Step();
        #endregion
    }
}
