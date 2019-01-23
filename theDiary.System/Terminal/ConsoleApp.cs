using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Terminal.Animations;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Terminal
{
    /// <summary>
    /// Represents the standard input, output, and error streams for console applications with an easier implementation for access to methods and functionality. 
    /// This class cannot be inherited.To browse the .NET Framework source code for the <see cref="Console"/>, see the Reference Source.
    /// </summary>
    public static class ConsoleApp
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="ConsoleApp"/> class.
        /// </summary>
        static ConsoleApp()
        {
            ConsoleApp.lineInputThread = ConsoleApp.InitializeThread(ref ConsoleApp.getLineInput, ref ConsoleApp.gotLineInput, ConsoleApp.ReadLineOrWait);
            ConsoleApp.keyInputThread = ConsoleApp.InitializeThread(ref ConsoleApp.getKeyInput, ref ConsoleApp.gotKeyInput, ConsoleApp.ReadKeyOrWait);
            Console.CancelKeyPress += ConsoleApp.CancelKeyPressHandler;
        }
        #endregion

        #region Private Declarations
        private static Thread lineInputThread;
        private static Thread keyInputThread;
        private static AutoResetEvent getLineInput, gotLineInput, getKeyInput, gotKeyInput;
        private static string lineInput;
        private static ConsoleKeyInfo keyInput;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the background color of the console.
        /// </summary>
        public static ConsoleColor BackgroundColor
        {
            get
            {
                return Console.BackgroundColor;
            }
            set
            {
                Console.BackgroundColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreground color of the console.
        /// </summary>
        public static ConsoleColor ForegroundColor
        {
            get
            {
                return Console.ForegroundColor;
            }
            set
            {
                Console.ForegroundColor = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the buffer area.
        /// </summary>
        public static int BufferHeight
        {
            get
            {
                return Console.BufferHeight;
            }
            set
            {
                Console.BufferHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the width of the buffer area.
        /// </summary>
        public static int BufferWidth
        {
            get
            {
                return Console.BufferWidth;
            }
            set
            {
                Console.BufferWidth = value;
            }
        }

        /// <summary>
        /// Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or
        /// </summary>
        public static bool CapsLock
        {
            get
            {
                return Console.CapsLock;
            }
        }

        /// <summary>
        /// Gets or sets the height of the cursor within a character cell.
        /// </summary>
        public static int CursorSize
        {
            get
            {
                return Console.CursorSize;
            }
            set
            {
                Console.CursorSize = value;
            }
        }

        /// <summary>
        /// Gets or sets the column position of the cursor within the buffer area.
        /// </summary>
        public static int CursorLeft
        {
            get
            {
                return Console.CursorLeft;
            }
            set
            {
                Console.CursorLeft = value;
            }
        }

        /// <summary>
        /// Gets or sets the row position of the cursor within the buffer area.
        /// </summary>
        public static int CursorTop
        {
            get
            {
                return Console.CursorTop;
            }
            set
            {
                Console.CursorTop = value;
            }
        }

        /// <summary>
        /// Gets or sets the column and row locations of the cursor within the buffer area.
        /// </summary>
        public static Location CursorLocation
        {
            get
            {
                return Location.New(ConsoleApp.CursorLeft, ConsoleApp.CursorTop);
            }
            set
            {
                ConsoleApp.CursorLeft = value.Left;
                ConsoleApp.CursorTop = value.Top;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating if the cursor is visible or not.
        /// </summary>
        public static bool CursorVisible
        {
            get
            {
                return Console.CursorVisible;
            }
            set
            {
                Console.CursorVisible = value;
            }
        }

        /// <summary>
        /// Gets the standard input stream.
        /// </summary>
        public static TextReader In
        {
            get
            {
                return Console.In;
            }
        }

        /// <summary>
        /// Gets the standard output stream.
        /// </summary>
        public static TextWriter Out
        {
            get
            {
                return Console.Out;
            }
        }

        /// <summary>
        /// Gets the standard error output stream.
        /// </summary>
        public static TextWriter Error
        {
            get
            {
                return Console.Error;
            }
        }

        /// <summary>
        /// Gets or sets the encoding the console uses to read input.
        /// </summary>
        public static Encoding InputEncoding
        {
            get
            {
                return Console.InputEncoding;
            }
            set
            {
                Console.InputEncoding = value;
            }
        }

        /// <summary>
        /// Gets or sets the encoding the console uses to write output.
        /// </summary>
        public static Encoding OutputEncoding
        {
            get
            {
                return Console.OutputEncoding;
            }
            set
            {
                Console.OutputEncoding = value;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether the error output stream has been redirected from the standard error stream.
        /// </summary>
        public static bool IsErrorRedirected
        {
            get
            {
                return Console.IsErrorRedirected;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether input has been redirected from the standard input stream.
        /// </summary>
        public static bool IsInputRedirected
        {
            get
            {
                return Console.IsInputRedirected;
            }
        }

        /// <summary>
        /// Gets a value that indicates whether output has been redirected from the standard output stream.
        /// </summary>
        public static bool IsOutputRedirected
        {
            get
            {
                return Console.IsOutputRedirected;
            }
        }

        /// <summary>
        /// Gets a value indicating whether a key press is available in the input stream.
        /// </summary>
        public static bool KeyAvailable
        {
            get
            {
                return Console.KeyAvailable;
            }
        }

        /// <summary>
        /// Gets the largest possible number of console window rows, based on the current font and screen resolution.
        /// </summary>
        public static int LargestWindowHeight
        {
            get
            {
                return Console.LargestWindowHeight;
            }
        }

        /// <summary>
        /// Gets the largest possible number of console window columns, based on the current font and screen resolution.
        /// </summary>
        public static int LargestWindowWidth
        {
            get
            {
                return Console.LargestWindowWidth;
            }
        }

        /// <summary>
        /// Gets or sets the title to display in the console title bar.
        /// </summary>
        public static string Title
        {
            get
            {
                return Console.Title;
            }
            set
            {
                Console.Title = value;
            }
        }

        public static bool TreatControlCAsInput
        {
            get
            {
                return Console.TreatControlCAsInput;
            }
            set
            {
                Console.TreatControlCAsInput = value;
            }
        }

        /// <summary>
        /// Gets or sets the leftmost position of the console window area relative to the screen buffer.
        /// </summary>
        public static int WindowLeft
        {
            get
            {
                return Console.WindowLeft;
            }
            set
            {
                Console.WindowLeft = value;
            }
        }

        /// <summary>
        /// Gets or sets the top position of the console window area relative to the screen buffer.
        /// </summary>
        public static int WindowTop
        {
            get
            {
                return Console.WindowTop;
            }
            set
            {
                Console.WindowTop = value;
            }
        }

        /// <summary>
        /// Gets or sets the location of the console window area relative to the screen buffer.
        /// </summary>
        public static Location WindowLocation
        {
            get
            {
                return Location.New(ConsoleApp.WindowLeft, ConsoleApp.WindowTop);
            }
            set
            {
                ConsoleApp.WindowLeft = value.Left;
                ConsoleApp.WindowTop = value.Top;
            }
        }

        /// <summary>
        /// Gets or sets the width of the console window area.
        /// </summary>
        public static int WindowWidth
        {
            get
            {
                return Console.WindowWidth;
            }
            set
            {
                Console.WindowWidth = value;
            }
        }

        /// <summary>
        /// Gets or sets the height of the console window area.
        /// </summary>
        public static int WindowHeight
        {
            get
            {
                return Console.WindowHeight;
            }
            set
            {
                Console.WindowHeight = value;
            }
        }

        /// <summary>
        /// Gets or sets the size of the console window area.
        /// </summary>
        public static System.Drawing.Size WindowSize
        {
            get
            {
                return new Drawing.Size(Console.WindowWidth, Console.WindowHeight);
            }
            set
            {
                Console.WindowHeight = value.Height;
                Console.WindowWidth = value.Width;
            }
        }
        #endregion

        #region Public Events
        /// <summary>
        /// The event that is raised when the Cancel key has been pressed.
        /// </summary>
        public static event ConsoleCancelEventHandler CancelKeyPress;
        #endregion

        public static void SetColor(ConsoleColor foregroundColor)
        {
            Console.ForegroundColor = foregroundColor;
        }

        public static void SetColor(ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
        }

        public static void ResetColor()
        {
            Console.ResetColor();
        }

        public static void Beep()
        {
            Console.Beep();
        }

        public static void Beep(int frequency, int duration)
        {
            Console.Beep(frequency, duration);
        }

        /// <summary>
        /// Clears the console buffer and corresponding console window of display information.
        /// </summary>
        /// <exception cref="IOException">An I/O error occurred</exception>
        public static void Clear()
        {
            Console.Clear();
        }

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
        {
            ConsoleApp.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
        }

        public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
        {
            ConsoleApp.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
        }

        public static Stream OpenStandardError()
        {
            return Console.OpenStandardError();
        }

        public static Stream OpenStandardError(int bufferSize)
        {
            return Console.OpenStandardError(bufferSize);
        }

        public static Stream OpenStandardInput()
        {
            return Console.OpenStandardInput();
        }

        public static Stream OpenStandardInput(int bufferSize)
        {
            return Console.OpenStandardInput(bufferSize);
        }

        public static Stream OpenStandardOutput()
        {
            return Console.OpenStandardOutput();
        }

        public static Stream OpenStandardOutput(int bufferSize)
        {
            return Console.OpenStandardOutput(bufferSize);
        }

        public static void SetBufferSize(int width, int height)
        {
            Console.SetBufferSize(width, height);
        }

        public static void SetCursorPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }

        public static void SetCursorPosition(Location position)
        {
            Console.SetCursorPosition(position.Left, position.Top);
        }

        public static void SetError(TextWriter newError)
        {
            Console.SetError(newError);
        }

        public static void SetIn(TextReader newIn)
        {
            Console.SetIn(newIn);
        }

        public static void SetOut(TextWriter newOut)
        {
            Console.SetOut(newOut);
        }

        public static void SetWindowPosition(int left, int top)
        {
            Console.SetWindowPosition(left, top);
        }

        public static void SetWindowPosition(Location position)
        {
            Console.SetWindowPosition(position.Left, position.Top);
        }

        public static void SetWindowSize(int width, int height)
        {
            Console.SetWindowPosition(width, height);
        }

        public static int Read()
        {
            return Console.Read();
        }

        #region ReadKey Methods & Functions
        public static ConsoleKeyInfo ReadKey()
        {
            return Console.ReadKey();
        }

        public static ConsoleKeyInfo ReadKey(TimeSpan timeOut)
        {
            return ConsoleApp.ReadKey(false, timeOut);
        }
        
        public static ConsoleKeyInfo ReadKey(TimeSpan timeOut, ConsoleKeyInfo timeOutValue)
        {
            return ConsoleApp.ReadKey(false, timeOut, timeOutValue);
        }

        public static ConsoleKeyInfo ReadKey(TimeSpan timeOut, ConsoleKeyInfo timeOutValue, IConsoleAnimation animation, TimeSpan animationStepFrequency)
        {
            return ConsoleApp.ReadKey(false, timeOut, timeOutValue, animation, animationStepFrequency);
        }
        
        public static ConsoleKeyInfo ReadKey(TimeSpan timeOut, IConsoleAnimation animation, TimeSpan animationStepFrequency)
        {
            return ConsoleApp.ReadKey(timeOut, default(ConsoleKeyInfo), animation, animationStepFrequency);
        }

        public static bool ReadKey(TimeSpan timeOut, out ConsoleKeyInfo value)
        {
            return ConsoleApp.ReadKey(false, timeOut, out value);
        }

        public static ConsoleKeyInfo ReadKey(bool intercept)
        {
            return Console.ReadKey(intercept);
        }

        public static ConsoleKeyInfo ReadKey(bool intercept, TimeSpan timeOut)
        {
            ConsoleKeyInfo value;
            ConsoleApp.ReadKey(intercept, timeOut, out value);

            return value;
        }

        public static ConsoleKeyInfo ReadKey(bool intercept, TimeSpan timeOut, ConsoleKeyInfo timeOutValue)
        {
            ConsoleKeyInfo value = default(ConsoleKeyInfo);
            if (!ConsoleApp.ReadKey(intercept, timeOut, out value))
                value = timeOutValue;

            return value;
        }

        public static ConsoleKeyInfo ReadKey(bool intercept, TimeSpan timeOut, IConsoleAnimation animation, TimeSpan animationStepFrequency)
        {
            return ConsoleApp.ReadKey(intercept, timeOut, default(ConsoleKeyInfo), animation, animationStepFrequency);
        }

        public static ConsoleKeyInfo ReadKey(bool intercept, TimeSpan timeOut, ConsoleKeyInfo timeOutValue, IConsoleAnimation animation, TimeSpan animationStepFrequency)
        {
            ConsoleKeyInfo value = default(ConsoleKeyInfo);
            bool canStep = true;
            using (Timer timer = new Timer((a) =>
            {
                if (canStep)
                    animation.Step();
            }))
            {
                timer.Change(0, Convert.ToInt32(animationStepFrequency.TotalMilliseconds));
                if (!ConsoleApp.ReadKey(intercept, timeOut, out value))
                    value = timeOutValue;
                timer.Change(Timeout.Infinite, 0);
            }
            return value;
        }

        public static bool ReadKey(bool intercept, TimeSpan timeOut, out ConsoleKeyInfo value)
        {
            value = default(ConsoleKeyInfo);
            if (timeOut.Ticks <= 0)
            {
                value = ConsoleApp.ReadKey(intercept);
                return true;
            }

            ConsoleApp.getKeyInput.Set();
            bool success = ConsoleApp.gotKeyInput.WaitOne(timeOut);
            if (success)
                value = ConsoleApp.keyInput;
            
            if (!intercept)
                Console.Write(value);
            
            return success;
        }
        #endregion

        #region ReadChar Methods & Functions
        public static Char ReadChar()
        {
            return ConsoleApp.ReadChar(false);
        }

        public static Char ReadChar(bool intercept)
        {
            return ConsoleApp.ReadKey(intercept).KeyChar;
        }

        public static Char ReadChar(TimeSpan timeOut)
        {
            Char value;
            ConsoleApp.ReadChar(timeOut, out value);

            return value;
        }
        
        public static Char ReadChar(TimeSpan timeOut, IConsoleAnimation animation, TimeSpan animationStepFrequency)
        {
            return ConsoleApp.ReadChar(timeOut, default(char), animation, animationStepFrequency);
        }

        public static Char ReadChar(TimeSpan timeOut, Char timeOutValue, IConsoleAnimation animation, TimeSpan animationStepFrequency)
        {
            Char value = default(char);
            bool canStep = true;
            using (Timer timer = new Timer((a) =>
            {
                if (canStep)
                    animation.Step();
            }))
            {
                timer.Change(0, Convert.ToInt32(animationStepFrequency.TotalMilliseconds));
                if (!ConsoleApp.ReadChar(timeOut, out value))
                    value = timeOutValue;

                timer.Change(Timeout.Infinite, 0);
            }
            return value;
        }

        public static bool ReadChar(TimeSpan timeOut, out char value)
        {
            value = default(char);
            if (timeOut.Ticks <= 0)
            {
                value = ConsoleApp.ReadKey().KeyChar;
                return true;
            }
            ConsoleKeyInfo key;
            bool success = ConsoleApp.ReadKey(timeOut, out key);
            value = key.KeyChar;
            return success;
        }
        #endregion

        #region ReadLine Methods & Functions
        public static string ReadLine()
        {
            return Console.ReadLine();
        }

        public static string ReadLine(int timeOut)
        {
            return ConsoleApp.ReadLine(new TimeSpan(0, 0, 0, 0, timeOut));
        }

        public static string ReadLine(int timeOut, string timeOutValue)
        {
            string value;
            if (ConsoleApp.ReadLine(new TimeSpan(0, 0, 0, 0, timeOut), out value))
                return value;

            return timeOutValue;
        }

        public static string ReadLine(TimeSpan timeOut)
        {
            string value;
            ConsoleApp.ReadLine(timeOut, out value);
            return value;
        }

        public static string ReadLine(TimeSpan timeOut, IConsoleAnimation animation, TimeSpan animationStepFrequency)
        {
            string value = null;
            bool canStep = true;
            using (Timer timer = new Timer((a) => {
                if (canStep)
                    animation.Step();
            }))
            {
                timer.Change(0, Convert.ToInt32(animationStepFrequency.TotalMilliseconds));
                value = ConsoleApp.ReadLine(timeOut);
                timer.Change(Timeout.Infinite, 0);
            }
            return value;
        }

        public static string ReadLine(TimeSpan timeOut, string timeOutValue)
        {
            string value;
            if (ConsoleApp.ReadLine(timeOut, out value))
                return value;

            return timeOutValue;
        }

        public static bool ReadLine(int timeOut, out string value)
        {
            return ConsoleApp.ReadLine(new TimeSpan(0, 0, 0, 0, timeOut), out value);
        }

        public static bool ReadLine(TimeSpan timeOut, out string value)
        {
            value = null;
            if (timeOut.Ticks <= 0)
            {
                value = ConsoleApp.ReadLine();
                return true;
            }

            ConsoleApp.getLineInput.Set();
            bool success = ConsoleApp.gotLineInput.WaitOne(timeOut);
            if (success)
                value = ConsoleApp.lineInput;

            return success;
        }
        #endregion

        #region Write Methods & Functions
        /// <summary>
        /// Writes the text representation of the specified <see cref="Boolean"/> value to the standard output stream.
        /// </summary>
        /// <param name="value">The value to write.</param>
        /// <exception cref="IOException">An I/O error occurred.</exception>
        public static void Write(bool value)
        {
            Console.Write(value);
        }

        public static void Write(char value)
        {
            Console.Write(value);
        }

        public static void Write(char[] buffer)
        {
            Console.Write(buffer);
        }

        public static void Write(decimal value)
        {
            Console.Write(value);
        }

        public static void Write(double value)
        {
            Console.Write(value);
        }

        public static void Write(float value)
        {
            Console.Write(value);
        }

        public static void Write(int value)
        {
            Console.Write(value);
        }

        public static void Write(long value)
        {
            Console.Write(value);
        }

        public static void Write(object value)
        {
            Console.Write(value);
        }

        public static void Write(string value)
        {
            Console.Write(value);
        }

        public static void Write(uint value)
        {
            Console.Write(value);
        }

        public static void Write(ulong value)
        {
            Console.Write(value);
        }

        public static void Write(string format, params object[] arg)
        {
            Console.Write(format, arg);
        }

        public static void Write(ConsoleColor foregroundColor, bool value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, char value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, char[] buffer)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(buffer);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, decimal value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, double value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, float value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, int value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, long value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, object value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, string value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, uint value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, ulong value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(value);
            Console.ForegroundColor = current;
        }

        public static void Write(ConsoleColor foregroundColor, string format, params object[] arg)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(format, arg);
            Console.ForegroundColor = current;
        }
        #endregion

        #region WriteLine Methods & Functions
        public static void WriteLine()
        {
            Console.WriteLine();
        }

        public static void WriteLine(bool value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(char value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(char[] buffer)
        {
            Console.WriteLine(buffer);
        }

        public static void WriteLine(decimal value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(double value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(float value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(long value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(object value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(uint value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(ulong value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(string value)
        {
            Console.WriteLine(value);
        }

        public static void WriteLine(string format, params object[] arg)
        {
            Console.WriteLine(format, arg);
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, bool value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, char value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, char[] buffer)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(buffer);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, decimal value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, double value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, float value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, long value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, object value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, uint value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, ulong value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, string value)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(value);
            Console.ForegroundColor = current;
        }

        public static void WriteLine(System.ConsoleColor foregroundColor, string format, params object[] arg)
        {
            ConsoleColor current = Console.ForegroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.WriteLine(format, arg);
            Console.ForegroundColor = current;
        }
        #endregion

        #region Private Methods & Functions
        private static void CancelKeyPressHandler(object sender, ConsoleCancelEventArgs e)
        {
            if (ConsoleApp.CancelKeyPress != null)
                ConsoleApp.CancelKeyPress(sender, e);
        }
        private static void ReadLineOrWait()
        {
            while (true)
            {
                ConsoleApp.getLineInput.WaitOne();
                ConsoleApp.lineInput = Console.ReadLine();
                ConsoleApp.gotLineInput.Set();
            }
        }

        private static void ReadKeyOrWait()
        {
            while (true)
            {
                ConsoleApp.getKeyInput.WaitOne();
                ConsoleApp.keyInput = Console.ReadKey(true);
                ConsoleApp.gotKeyInput.Set();
            }
        }

        private static Thread InitializeThread(ref AutoResetEvent getEvent, ref AutoResetEvent gotEvent, ThreadStart @delegate)
        {
            Thread returnValue = new Thread(@delegate);
            returnValue.IsBackground = true;
            returnValue.Start();

            getEvent = new AutoResetEvent(false);
            gotEvent = new AutoResetEvent(false);

            return returnValue;
        }
        #endregion
    }
}