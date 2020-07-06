using System;
using System.Runtime.InteropServices;

namespace Tergie.source
{
    /// <summary>
    /// All low level static functions and such are in this class.
    /// </summary>
    public class LowLevel
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct COORD
        {
            public short X;
            public short Y;

            public COORD(short x, short y)
            {
                X = x;
                Y = y;
            }
        }
        
        [StructLayout(LayoutKind.Sequential)]
        public struct SMALL_RECT
        {
            public short Left;
            public short Top;
            public short Right;
            public short Bottom;

            public SMALL_RECT(short left, short top, short right, short bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct CHAR_INFO
        {
            [FieldOffset(0)]
            public char UnicodeChar;
            [FieldOffset(0)]
            public char AsciiChar;
            [FieldOffset(2)] //2 bytes seems to work properly
            public UInt16 Attributes;
        }
        
        public enum CharAttributes : ushort
        {
            /// <summary>
            /// None.
            /// </summary>
            None = 0x0000,

            /// <summary>
            /// Text color contains blue.
            /// </summary>
            FOREGROUND_BLUE = 0x0001,

            /// <summary>
            /// Text color contains green.
            /// </summary>
            FOREGROUND_GREEN = 0x0002,

            /// <summary>
            /// Text color contains red.
            /// </summary>
            FOREGROUND_RED = 0x0004,

            /// <summary>
            /// Text color is intensified.
            /// </summary>
            FOREGROUND_INTENSITY = 0x0008,

            /// <summary>
            /// Background color contains blue.
            /// </summary>
            BACKGROUND_BLUE = 0x0010,

            /// <summary>
            /// Background color contains green.
            /// </summary>
            BACKGROUND_GREEN = 0x0020,

            /// <summary>
            /// Background color contains red.
            /// </summary>
            BACKGROUND_RED = 0x0040,

            /// <summary>
            /// Background color is intensified.
            /// </summary>
            BACKGROUND_INTENSITY = 0x0080,

            /// <summary>
            /// Leading byte.
            /// </summary>
            COMMON_LVB_LEADING_BYTE = 0x0100,

            /// <summary>
            /// Trailing byte.
            /// </summary>
            COMMON_LVB_TRAILING_BYTE = 0x0200,

            /// <summary>
            /// Top horizontal
            /// </summary>
            COMMON_LVB_GRID_HORIZONTAL = 0x0400,

            /// <summary>
            /// Left vertical.
            /// </summary>
            COMMON_LVB_GRID_LVERTICAL = 0x0800,

            /// <summary>
            /// Right vertical.
            /// </summary>
            COMMON_LVB_GRID_RVERTICAL = 0x1000,

            /// <summary>
            /// Reverse foreground and background attribute.
            /// </summary>
            COMMON_LVB_REVERSE_VIDEO = 0x4000,

            /// <summary>
            /// Underscore.
            /// </summary>
            COMMON_LVB_UNDERSCORE = 0x8000,
        }
        
        [DllImport("Kernel32.dll")] public static extern IntPtr CreateConsoleScreenBuffer(uint desiredAccess,
            uint shareMode, IntPtr securityAttributes, uint flags, IntPtr reserved);

        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleActiveScreenBuffer(IntPtr screenBuffer);

        [DllImport("Kernel32.dll",CharSet = CharSet.Ansi)]
        public static extern bool WriteConsoleOutputCharacter(IntPtr screenBuffer,string characters,uint length,COORD writeCoord, out UInt32 numCharsWritten );

        [DllImport("kernel32.dll", EntryPoint = "WriteConsoleOutputW", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool WriteConsoleOutput(
            IntPtr hConsoleOutput, // screen buffer to write on
            /* This pointer is treated as the origin of a two-dimensional array of CHAR_INFO structures
            whose size is specified by the dwBufferSize parameter.*/
            [MarshalAs(UnmanagedType.LPArray), In] CHAR_INFO[,] lpBuffer, // 2d array of content to write
            COORD dwBufferSize, // size of lpBuffer
            COORD dwBufferCoord, // what part of content to write to start writing from
            ref SMALL_RECT lpWriteRegion); // region of screen buffer to write on
        
    }
}