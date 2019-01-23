using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Diagnostics
{
    /// <summary>
    /// Extension methods for use by <see cref="Process"/> related classes.
    /// </summary>
    public static class ProcessExtensions
    {
        #region Private Constant Declarations
        private const string RunElevatedVerb = "runas";
        #endregion Private Constant Declarations

        #region Public Extension Methods & Functions
        /// <summary>
        /// Determines if a <see cref="Process"/> instance is running in an elevated state.
        /// </summary>
        /// <param name="process">The <see cref="Process"/> instance to check.</param>
        /// <returns><c>True</c> if the <paramref name="process"/> is running in an elevated state; otherwise <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">thrown when <paramref name="process"/> is <c>Null</c>.</exception>
        public static bool IsElevated(this Process process)
        {
            if (process.IsNull())
                throw new ArgumentNullException(nameof(process));

            return System.Security.UAC.UACHelper.IsElevatedProcess(process);
        }

        /// <summary>
        /// Determines if a <see cref="ProcessStartInfo"/> instance is configured to run in an elevated state.
        /// </summary>
        /// <param name="startInfo">The <see cref="ProcessStartInfo"/> instance to check.</param>
        /// <returns><c>True</c> if the <paramref name="startInfo"/> is configured to run in an elevated state; otherwise <c>False</c>.</returns>
        /// <exception cref="ArgumentNullException">thrown when <paramref name="startInfo"/> is <c>Null</c>.</exception>
        public static bool IsElevated(this ProcessStartInfo startInfo)
        {
            return startInfo.UseShellExecute
                && startInfo.Verb.Contains(ProcessExtensions.RunElevatedVerb, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Starts the specified <paramref name="process"/> in an elevated state.
        /// </summary>
        /// <param name="process">The <see cref="Process"/> instance to Start in an elevated state.</param>
        /// <exception cref="ArgumentNullException">thrown when <paramref name="process"/> is <c>Null</c>.</exception>
        public static void StartElevated(this Process process)
        {
            if (process.IsNull())
                throw new ArgumentNullException(nameof(process));
            try
            {
                process.StartInfo.ConfigureElevatedState();
                process.Start();
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Starts a new elevated <see cref="Process"/> from the details of the <paramref name="startInfo"/>.
        /// </summary>
        /// <param name="startInfo">The <see cref="ProcessStartInfo"/> instance to start.</param>
        /// <returns>The <see cref="Process"/> instance that was started.</returns>
        /// <exception cref="ArgumentNullException">thrown if <paramref name="startInfo"/> is <c>Null</c>.</exception>
        public static Process StartElevated(this ProcessStartInfo startInfo)
        {
            if (startInfo.IsNull())
                throw new ArgumentNullException(nameof(startInfo));

            startInfo.ConfigureElevatedState();

            return Process.Start(startInfo);
        }
        #endregion Public Extension Methods & Functions

        #region Private Extension Methods & Functions
        /// <summary>
        /// Configures a <see cref="ProcessStartInfo"/> instance to run in an elevated state.
        /// </summary>
        /// <param name="startInfo">The <see cref="ProcessStartInfo"/> instance to configure.</param>
        private static void ConfigureElevatedState(this ProcessStartInfo startInfo)
        {
            startInfo.UseShellExecute = true;
            startInfo.Verb = ProcessExtensions.RunElevatedVerb;
        }
        #endregion Private Extension Methods & Functions
    }
}
