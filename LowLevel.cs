using System;
using System.Runtime.InteropServices;

namespace Tergie
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
        
        [DllImport("Kernel32.dll")] public static extern IntPtr CreateConsoleScreenBuffer(uint desiredAccess,
            uint shareMode, IntPtr securityAttributes, uint flags, IntPtr reserved);

        [DllImport("Kernel32.dll")]
        public static extern bool SetConsoleActiveScreenBuffer(IntPtr screenBuffer);

        [DllImport("Kernel32.dll",CharSet = CharSet.Ansi)]
        public static extern bool WriteConsoleOutputCharacter(IntPtr screenBuffer,string characters,uint length,COORD writeCoord, out UInt32 numCharsWritten );
    }
}