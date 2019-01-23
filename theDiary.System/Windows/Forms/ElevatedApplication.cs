using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security;
using System.Security.Permissions;
using System.ComponentModel;
using System.Diagnostics;

namespace System.Windows.Forms
{
    /// <summary>
    /// Provides static methods and properties to manage an application, such as methods to start and stop an application, to process Windows messages, and properties to get information about an application. 
    /// This class cannot be inherited.
    /// </summary>
    public sealed class ElevatedApplication
    {
        #region Private Constant Declarations
        private static volatile ApplicationElevationRequirement elevationRequirement = ApplicationElevationRequirement.ElevationRequired;
        private static volatile bool applicationRunning = false;
        #endregion Private Constant Declarations

        #region Private Properties
        private static bool IsElevationRequired
        {
            get
            {
                return ElevatedApplication.ElevationRequirement != ApplicationElevationRequirement.ElevationNotRequired;
            }
        }
        #endregion Private Properties

        #region Public Properties
        /// <summary>
        /// Gets a value indicating whether the caller can quit this application.
        /// </summary>
        /// 
        /// <returns>
        /// true if the caller can quit this application; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public static bool AllowQuit
        {
            get
            {
                return Application.AllowQuit;
            }
        }
        public static ApplicationElevationRequirement ElevationRequirement
        {
            get
            {
                return ElevatedApplication.elevationRequirement;
            }
        }
        public static bool IsElevated
        {
            get
            {
                return System.Security.UAC.UACHelper.IsProcessElevated;
            }
        }

        /// <summary>
        /// Gets the registry key for the application data that is shared among all users.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:Microsoft.Win32.RegistryKey"/> representing the registry key of the application data that is shared among all users.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static Microsoft.Win32.RegistryKey CommonAppDataRegistry
        {
            get
            {
                return Application.CommonAppDataRegistry;
            }
        }

        /// <summary>
        /// Gets the path for the application data that is shared among all users.
        /// </summary>
        /// 
        /// <returns>
        /// The path for the application data that is shared among all users.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string CommonAppDataPath
        {
            get
            {
                return Application.CommonAppDataPath;
            }
        }

        /// <summary>
        /// Gets the company name associated with the application.
        /// </summary>
        /// 
        /// <returns>
        /// The company name.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string CompanyName
        {
            get
            {
                return Application.CompanyName;
            }
        }

        /// <summary>
        /// Gets or sets the culture information for the current thread.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Globalization.CultureInfo"/> representing the culture information for the current thread.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread"/></PermissionSet>
        public static System.Globalization.CultureInfo CurrentCulture
        {
            get
            {
                return Application.CurrentCulture;
            }
            set
            {
                Application.CurrentCulture = value;
            }
        }

        /// <summary>
        /// Gets or sets the current input language for the current thread.
        /// </summary>
        /// 
        /// <returns>
        /// An <see cref="T:System.Windows.Forms.InputLanguage"/> representing the current input language for the current thread.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public static InputLanguage CurrentInputLanguage
        {
            get
            {
                return Application.CurrentInputLanguage;
            }
            set
            {
                Application.CurrentInputLanguage = value;
            }
        }

        /// <summary>
        /// Gets the path for the executable file that started the application, including the executable name.
        /// </summary>
        /// 
        /// <returns>
        /// The path and executable name for the executable file that started the application.This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string ExecutablePath
        {
            get
            {
                return Application.ExecutablePath;
            }
        }

        /// <summary>
        /// Gets the path for the application data of a local, non-roaming user.
        /// </summary>
        /// 
        /// <returns>
        /// The path for the application data of a local, non-roaming user.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string LocalUserAppDataPath
        {
            get
            {
                return Application.LocalUserAppDataPath;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a message loop exists on this thread.
        /// </summary>
        /// 
        /// <returns>
        /// true if a message loop exists; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static bool MessageLoop
        {
            get
            {
                return Application.MessageLoop;
            }
        }

        /// <summary>
        /// Gets a collection of open forms owned by the application.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:System.Windows.Forms.FormCollection"/> containing all the currently open forms owned by this application.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="AllWindows"/></PermissionSet>
        public static FormCollection OpenForms
        {
            get
            {
                return Application.OpenForms;
            }
        }

        /// <summary>
        /// Gets the product name associated with this application.
        /// </summary>
        /// 
        /// <returns>
        /// The product name.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string ProductName
        {
            get
            {
                return Application.ProductName;
            }
        }

        /// <summary>
        /// Gets the product version associated with this application.
        /// </summary>
        /// 
        /// <returns>
        /// The product version.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string ProductVersion
        {
            get
            {
                return Application.ProductVersion;
            }
        }

        /// <summary>
        /// Gets a value specifying whether the current application is drawing controls with visual styles.
        /// </summary>
        /// 
        /// <returns>
        /// true if visual styles are enabled for controls in the client area of application windows; otherwise, false.
        /// </returns>
        public static bool RenderWithVisualStyles
        {
            get
            {
                return Application.RenderWithVisualStyles;
            }
        }

        /// <summary>
        /// Gets or sets the format string to apply to top-level window captions when they are displayed with a warning banner.
        /// </summary>
        /// 
        /// <returns>
        /// The format string to apply to top-level window captions.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public static string SafeTopLevelCaptionFormat
        {
            get
            {
                return Application.SafeTopLevelCaptionFormat;
            }
            set
            {
                Application.SafeTopLevelCaptionFormat = value;
            }
        }

        /// <summary>
        /// Gets the path for the executable file that started the application, not including the executable name.
        /// </summary>
        /// 
        /// <returns>
        /// The path for the executable file that started the application.This path will be different depending on whether the Windows Forms application is deployed using ClickOnce. ClickOnce applications are stored in a per-user application cache in the C:\Documents and Settings\username directory. For more information, see Accessing Local and Remote Data in ClickOnce Applications.
        /// </returns>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string StartupPath
        {
            get
            {
                return Application.StartupPath;
            }
        }

        /// <summary>
        /// Gets or sets whether the wait cursor is used for all open forms of the application.
        /// </summary>
        /// 
        /// <returns>
        /// true is the wait cursor is used for all open forms; otherwise, false.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public static bool UseWaitCursor
        {
            get
            {
                return Application.UseWaitCursor;
            }
            set
            {
                Application.UseWaitCursor = value;
            }
        }

        /// <summary>
        /// Gets the path for the application data of a user.
        /// </summary>
        /// 
        /// <returns>
        /// The path for the application data of a user.
        /// </returns>
        /// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static string UserAppDataPath
        {
            get
            {
                return Application.UserAppDataPath;
            }
        }

        /// <summary>
        /// Gets the registry key for the application data of a user.
        /// </summary>
        /// 
        /// <returns>
        /// A <see cref="T:Microsoft.Win32.RegistryKey"/> representing the registry key for the application data specific to the user.
        /// </returns>
        /// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static Microsoft.Win32.RegistryKey UserAppDataRegistry
        {
            get
            {
                return Application.UserAppDataRegistry;
            }
        }

        /// <summary>
        /// Gets a value that specifies how visual styles are applied to application windows.
        /// </summary>
        /// 
        /// <returns>
        /// A bitwise combination of the <see cref="T:System.Windows.Forms.VisualStyles.VisualStyleState"/> values.
        /// </returns>
        /// <filterpriority>1</filterpriority>
        public static System.Windows.Forms.VisualStyles.VisualStyleState VisualStyleState
        {
            get
            {
                return Application.VisualStyleState;
            }
            set
            {
                Application.VisualStyleState = value;
            }
        }
        #endregion

        #region Public Events

        /// <summary>
        /// Occurs when the application is about to shut down.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static event EventHandler ApplicationExit
        {
            add
            {
                Application.ApplicationExit += value;
            }
            remove
            {
                Application.ApplicationExit -= value;
            }
        }

        /// <summary>
        /// Occurs when the application finishes processing and is about to enter the idle state.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static event EventHandler Idle
        {
            add
            {
                Application.Idle += value;
            }
            remove
            {
                Application.Idle -= value;
            }
        }

        /// <summary>
        /// Occurs when the application is about to enter a modal state.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static event EventHandler EnterThreadModal
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            add
            {
                Application.EnterThreadModal += value;
            }
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            remove
            {
                Application.EnterThreadModal -= value;
            }
        }

        /// <summary>
        /// Occurs when the application is about to leave a modal state.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static event EventHandler LeaveThreadModal
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            add
            {
                Application.LeaveThreadModal += value;
            }
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            remove
            {
                Application.LeaveThreadModal -= value;
            }
        }

        /// <summary>
        /// Occurs when an untrapped thread exception is thrown.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static event System.Threading.ThreadExceptionEventHandler ThreadException
        {
            add
            {
                Application.ThreadException += value;
            }
            remove
            {
                Application.ThreadException -= value;
            }
        }

        /// <summary>
        /// Occurs when a thread is about to shut down. When the main thread for an application is about to be shut down, this event is raised first, followed by an <see cref="E:System.Windows.Forms.Application.ApplicationExit"/> event.
        /// </summary>
        /// <filterpriority>1</filterpriority>
        public static event EventHandler ThreadExit
        {
            add
            {
                Application.ThreadExit += value;
            }
            remove
            {
                Application.ThreadExit -= value;
            }
        }
        #endregion Public Events

        #region Public Static Methods & Functions

        /// <summary>
        /// Registers a callback for checking whether the message loop is running in hosted environments.
        /// </summary>
        /// <param name="callback">The method to call when Windows Forms needs to check if the hosting environment is still sending messages.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void RegisterMessageLoop(Application.MessageLoopCallback callback)
        {
            Application.RegisterMessageLoop(callback);
        }

        /// <summary>
        /// Unregisters the message loop callback made with <see cref="M:System.Windows.Forms.Application.RegisterMessageLoop(System.Windows.Forms.Application.MessageLoopCallback)"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void UnregisterMessageLoop()
        {
            Application.UnregisterMessageLoop();
        }

        /// <summary>
        /// Adds a message filter to monitor Windows messages as they are routed to their destinations.
        /// </summary>
        /// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter"/> interface you want to install. </param><filterpriority>1</filterpriority>
        public static void AddMessageFilter(IMessageFilter value)
        {
            Application.AddMessageFilter(value);
        }

        /// <summary>
        /// Runs any filters against a window message, and returns a copy of the modified message.
        /// </summary>
        /// 
        /// <returns>
        /// True if the filters were processed; otherwise, false.
        /// </returns>
        /// <param name="message">The Windows event message to filter. </param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static bool FilterMessage(ref Message message)
        {
            return Application.FilterMessage(ref message);
        }

        /// <summary>
        /// Processes all Windows messages currently in the message queue.
        /// </summary>
        /// <filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static void DoEvents()
        {
            Application.DoEvents();
        }

        /// <summary>
        /// Enables visual styles for the application.
        /// </summary>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static void EnableVisualStyles()
        {
            Application.EnableVisualStyles();
        }

        /// <summary>
        /// Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.
        /// </summary>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static void Exit()
        {
            Application.Exit((CancelEventArgs) null);
        }

        /// <summary>
        /// Informs all message pumps that they must terminate, and then closes all application windows after the messages have been processed.
        /// </summary>
        /// <param name="e">Returns whether any <see cref="T:System.Windows.Forms.Form"/> within the application cancelled the exit.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        public static void Exit(CancelEventArgs e)
        {
            Application.Exit(e);
        }

        /// <summary>
        /// Exits the message loop on the current thread and closes all windows on the thread.
        /// </summary>
        /// <filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static void ExitThread()
        {
            Application.ExitThread();
        }
        
        /// <summary>
        /// Initializes OLE on the current thread.
        /// </summary>
        /// 
        /// <returns>
        /// One of the <see cref="T:System.Threading.ApartmentState"/> values.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public static System.Threading.ApartmentState OleRequired()
        {
            return Application.OleRequired();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Application.ThreadException"/> event.
        /// </summary>
        /// <param name="t">An <see cref="T:System.Exception"/> that represents the exception that was thrown. </param><filterpriority>1</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static void OnThreadException(Exception t)
        {
            Application.OnThreadException(t);
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Application.Idle"/> event in hosted scenarios.
        /// </summary>
        /// <param name="e">The <see cref="T:System.EventArgs"/> objects to pass to the <see cref="E:System.Windows.Forms.Application.Idle"/> event.</param>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void RaiseIdle(EventArgs e)
        {
            Application.RaiseIdle(e);
        }

        /// <summary>
        /// Removes a message filter from the message pump of the application.
        /// </summary>
        /// <param name="value">The implementation of the <see cref="T:System.Windows.Forms.IMessageFilter"/> to remove from the application. </param><filterpriority>1</filterpriority>
        public static void RemoveMessageFilter(IMessageFilter value)
        {
            Application.RemoveMessageFilter(value);
        }

        /// <summary>
        /// Shuts down the application and starts a new instance immediately.
        /// </summary>
        /// <exception cref="T:System.NotSupportedException">Your code is not a Windows Forms application. You cannot call this method in this context.</exception>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public static void Restart()
        {
            Application.Restart();
        }

        /// <summary>
        /// Begins running a standard application message loop on the current thread, without a form.
        /// </summary>
        /// <exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static ElevationRequestResult Run()
        {
            ElevationRequestResult returnValue = ElevationRequestResult.Unknown;
            try
            {
                returnValue = ElevatedApplication.RunElevated();
                return returnValue;
            }
            finally
            {
                if (returnValue == ElevationRequestResult.ElevationNotRequired)
                {
                    ElevatedApplication.applicationRunning = true;
                    Application.Run();
                }
            }
        }

        /// <summary>
        /// Begins running a standard application message loop on the current thread, and makes the specified form visible.
        /// </summary>
        /// <param name="mainForm">A <see cref="T:System.Windows.Forms.Form"/> that represents the form to make visible. </param><exception cref="T:System.InvalidOperationException">A main message loop is already running on the current thread. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static ElevationRequestResult Run(Form mainForm)
        {
            ElevationRequestResult returnValue = ElevationRequestResult.Unknown;
            try
            {
                returnValue = ElevatedApplication.RunElevated();
                return returnValue;
            }
            finally
            {
                if (returnValue == ElevationRequestResult.ElevationNotRequired)
                {
                    ElevatedApplication.applicationRunning = true;
                    Application.Run(mainForm);
                }
            }
        }

        /// <summary>
        /// Begins running a standard application message loop on the current thread, with an <see cref="T:System.Windows.Forms.ApplicationContext"/>.
        /// </summary>
        /// <param name="context">An <see cref="T:System.Windows.Forms.ApplicationContext"/> in which the application is run. </param><exception cref="T:System.InvalidOperationException">A main message loop is already running on this thread. </exception><filterpriority>2</filterpriority><PermissionSet><IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence"/><IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/><IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true"/></PermissionSet>
        public static ElevationRequestResult Run(ApplicationContext context)
        {
            ElevationRequestResult returnValue = ElevationRequestResult.Unknown;
            try
            {
                returnValue = ElevatedApplication.RunElevated();
                return returnValue;
            }
            finally
            {
                if (returnValue == ElevationRequestResult.ElevationNotRequired)
                {
                    ElevatedApplication.applicationRunning = true;
                    Application.Run(context);
                }
            }
        }

        public static void SetCompatibleTextRenderingDefault(bool defaultValue)
        {
            Application.SetCompatibleTextRenderingDefault(defaultValue);
        }

        /// <summary>
        /// Suspends or hibernates the system, or requests that the system be suspended or hibernated.
        /// </summary>
        /// 
        /// <returns>
        /// true if the system is being suspended, otherwise, false.
        /// </returns>
        /// <param name="state">A <see cref="T:System.Windows.Forms.PowerState"/> indicating the power activity mode to which to transition. </param><param name="force">true to force the suspended mode immediately; false to cause Windows to send a suspend request to every application. </param><param name="disableWakeEvent">true to disable restoring the system's power status to active on a wake event, false to enable restoring the system's power status to active on a wake event. </param><filterpriority>1</filterpriority>
        public static bool SetSuspendState(PowerState state, bool force, bool disableWakeEvent)
        {
            return Application.SetSuspendState(state, force, disableWakeEvent);
        }

        /// <summary>
        /// Instructs the application how to respond to unhandled exceptions.
        /// </summary>
        /// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode"/> value describing how the application should behave if an exception is thrown without being caught.</param><exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
        public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode)
        {
            Application.SetUnhandledExceptionMode(mode);
        }

        /// <summary>
        /// Instructs the application how to respond to unhandled exceptions, optionally applying thread-specific behavior.
        /// </summary>
        /// <param name="mode">An <see cref="T:System.Windows.Forms.UnhandledExceptionMode"/> value describing how the application should behave if an exception is thrown without being caught.</param><param name="threadScope">true to set the thread exception mode; otherwise, false.</param><exception cref="T:System.InvalidOperationException">You cannot set the exception mode after the application has created its first window.</exception>
        public static void SetUnhandledExceptionMode(UnhandledExceptionMode mode, bool threadScope)
        {
            Application.SetUnhandledExceptionMode(mode, threadScope);
        }

        public static void SetElevationRequirement(bool elevationRequired)
        {
            ElevatedApplication.SetElevationRequirement(elevationRequired ? ApplicationElevationRequirement.ElevationRequired : ApplicationElevationRequirement.ElevationNotRequired);
        }

        public static void SetElevationRequirement(ApplicationElevationRequirement elevationRequirement)
        {
            if (ElevatedApplication.applicationRunning && ElevatedApplication.elevationRequirement != elevationRequirement)
                throw new InvalidOperationException("The elevation requirements for the application can not be changed when running.");

            ElevatedApplication.elevationRequirement = elevationRequirement;
        }
        #endregion Public Static Methods & Functions

        #region Private Static Methods & Functions
        private static ElevationRequestResult RunElevated()
        {
            if (!ElevatedApplication.IsElevationRequired ||
                    (ElevatedApplication.IsElevationRequired
                        && ElevatedApplication.IsElevated))
                return ElevationRequestResult.ElevationNotRequired;

            System.Diagnostics.ProcessStartInfo proc = new System.Diagnostics.ProcessStartInfo();
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;

            bool exitApplication = true;
            try
            {
                proc.StartElevated();
                return ElevationRequestResult.ElevationGranted;
            }
            catch
            {
                if (ElevatedApplication.ElevationRequirement != ApplicationElevationRequirement.MixedElevation)
                    return ElevationRequestResult.ElevationRefused;

                exitApplication = false;
                return ElevationRequestResult.ElevationNotRequired;
            }
            finally
            {
                if (exitApplication)
                    Application.Exit();
            }
        }
        #endregion Private Static Methods & Functions
    }
}
