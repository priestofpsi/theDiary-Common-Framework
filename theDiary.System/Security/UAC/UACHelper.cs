using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace System.Security.UAC
{
    public static class UACHelper
    {
        private const string UACRegistryKey = "Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System";
        private const string UACRegistryValue = "EnableLUA";

        private static uint STANDARD_RIGHTS_READ = 0x00020000;
        private static uint TOKEN_QUERY = 0x0008;
        private static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);

        #region Private Interop Methods & Functions
        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenProcessToken(IntPtr processHandle, UInt32 desiredAccess, out IntPtr tokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool GetTokenInformation(IntPtr tokenHandle, TokenInformationClasses tokenInformationClass, IntPtr tokenInformation, uint tokenInformationLength, out uint ReturnLength);
        #endregion

        public static bool IsUACEnabled
        {
            get
            {
                using (RegistryKey uacKey = Registry.LocalMachine.OpenSubKey(UACHelper.UACRegistryKey, false))
                {
                    bool result = uacKey.GetValue(UACHelper.UACRegistryValue).Equals(1);
                    return result;
                }
            }
        }

        public static bool IsProcessElevated
        {
            get
            {
                if (UACHelper.IsUACEnabled)
                {
                    IntPtr tokenHandle = IntPtr.Zero;
                    if (!UACHelper.OpenProcessToken(Process.GetCurrentProcess().Handle, TOKEN_READ, out tokenHandle))
                    {
                        throw new ApplicationException(string.Format("Could not get process token.  Win32 Error Code: {0}", Marshal.GetLastWin32Error()));
                    }

                    try
                    {
                        TokenElevationType elevationResult = TokenElevationType.TokenElevationTypeDefault;

                        int elevationResultSize = Marshal.SizeOf((int) elevationResult);
                        uint returnedSize = 0;

                        IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);
                        try
                        {
                            bool success = UACHelper.GetTokenInformation(tokenHandle, TokenInformationClasses.TokenElevationType,
                                                               elevationTypePtr, (uint) elevationResultSize,
                                                               out returnedSize);
                            if (success)
                            {
                                elevationResult = (TokenElevationType) Marshal.ReadInt32(elevationTypePtr);
                                bool isProcessAdmin = elevationResult == TokenElevationType.TokenElevationTypeFull;
                                return isProcessAdmin;
                            }
                            else
                            {
                                throw new ApplicationException("Unable to determine the current elevation.");
                            }
                        }
                        finally
                        {
                            if (elevationTypePtr != IntPtr.Zero)
                                Marshal.FreeHGlobal(elevationTypePtr);
                        }
                    }
                    finally
                    {
                        if (tokenHandle != IntPtr.Zero)
                            UACHelper.CloseHandle(tokenHandle);
                    }
                }
                else
                {
                    WindowsIdentity identity = WindowsIdentity.GetCurrent();
                    WindowsPrincipal principal = new WindowsPrincipal(identity);
                    bool result = principal.IsInRole(WindowsBuiltInRole.Administrator)
                               || principal.IsInRole(0x200);
                    return result;
                }
            }
        }

        public static bool IsElevatedProcess(Process process)
        {
            if (process == null)
                throw new ArgumentNullException("process");

            if (process.HasExited)
                throw new InvalidOperationException("The process is not running.");

            IntPtr tokenHandle = IntPtr.Zero;
            if (!UACHelper.OpenProcessToken(process.Handle, UACHelper.TOKEN_READ, out tokenHandle))
                throw new ApplicationException(string.Format("Could not get process token. Win32 Error Code: {0}", Marshal.GetLastWin32Error()));

            try
            {
                TokenElevationType elevationResult = TokenElevationType.TokenElevationTypeDefault;

                int elevationResultSize = Marshal.SizeOf((int) elevationResult);
                uint returnedSize = 0;

                IntPtr elevationTypePtr = Marshal.AllocHGlobal(elevationResultSize);
                try
                {
                    bool success = UACHelper.GetTokenInformation(tokenHandle, TokenInformationClasses.TokenElevationType, elevationTypePtr, (uint) elevationResultSize, out returnedSize);
                    if (!success)
                        throw new ApplicationException("Unable to determine the current elevation.");

                    elevationResult = (TokenElevationType) Marshal.ReadInt32(elevationTypePtr);
                    bool isProcessAdmin = elevationResult == TokenElevationType.TokenElevationTypeFull;
                    return isProcessAdmin;
                }
                finally
                {
                    if (elevationTypePtr.IsNotEmpty())
                        Marshal.FreeHGlobal(elevationTypePtr);
                }
            }
            finally
            {
                if (tokenHandle.IsNotEmpty())
                    UACHelper.CloseHandle(tokenHandle);
            }
        }

    }
}
