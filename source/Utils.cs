using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace Tergie.source
{
    public class Utils
    {
        public static char TransparentChar = '\0';
        
        /// <summary>
        /// Create a 2d char array from a text file.
        /// </summary>
        public static char[,] FileToCharArray(string file, bool replaceSpaceWithTransparentChar)
        {
            // read file and build 2d char array
            List<List<char>> fileContent = new List<List<char>>();
            int width = 0;
            int height = 0;
            using (StreamReader reader = new StreamReader(file))
            {
                while (true)
                {
                    string line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (line.Length > width)
                        width = line.Length;
                    height += 1;
                    List<char> rowOfChars = new List<char>();
                    foreach (var character in line)
                    {
                        if (character == ' ' && replaceSpaceWithTransparentChar)
                            rowOfChars.Add(Utils.TransparentChar);
                        else
                            rowOfChars.Add(character);
                    }
                    fileContent.Add(rowOfChars);
                }
            }
            
            char[,] contentArray = new char[height,width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (j < fileContent[i].Count)
                        contentArray[i, j] = fileContent[i][j];
                    else
                        contentArray[i, j] = TransparentChar;
                }
            }

            return contentArray;
        }

        public static void Blit(char[,] source, char[,] dest, Vector2I pos, bool copyTransparentPixel)
        {
            int startX = pos.X;
            int startY = pos.Y;
            int width = source.GetLength(1);
            int height = source.GetLength(0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (source[i,j] != TransparentChar || copyTransparentPixel)
                        dest[i + startY, j + startX] = source[i, j];
                }
            }
        }

        public static char[,] StrToCharArray(string text)
        {
            char[,] charArray = new char[1,text.Length];
            for (int i = 0; i < text.Length; i++)
                charArray[0, i] = text[i];
            return charArray;
        }

        /// <summary>
        /// Set every element of a 2d char array to a specific value.
        /// </summary>
        public static void SetChar(char[,] charArray, char value)
        {
            for (int i = 0; i < charArray.GetLength(0); i++)
            {
                for (int j = 0; j < charArray.GetLength(1); j++)
                {
                    charArray[i, j] = value;
                }
            }
        }

        public static IntPtr CreateScreenBuffer()
        {
            return LowLevel.CreateConsoleScreenBuffer((uint) (0x80000000L | 0x40000000L), 0, IntPtr.Zero, 0, IntPtr.Zero);
        }

        public static void SetActiveScreenBuffer(IntPtr screenBuffer)
        {
            LowLevel.SetConsoleActiveScreenBuffer(screenBuffer);
        }

        public static void WriteStringToScreenBuffer(IntPtr screenBufferToWriteOn, string stringToWrite, Vector2I posOfScreenBufferToStartTheWritingAt)
        {
            UInt32 numCharsWritten;
            LowLevel.WriteConsoleOutputCharacter(screenBufferToWriteOn, stringToWrite,(uint) stringToWrite.Length, new  LowLevel.COORD((short) posOfScreenBufferToStartTheWritingAt.X,(short) posOfScreenBufferToStartTheWritingAt.Y), out numCharsWritten);
        }
        
        public static void WriteToScreenBuffer(IntPtr screenBufferToWriteOn, 
            LowLevel.SMALL_RECT regionOfScreenBufferToWriteOn, LowLevel.CHAR_INFO[,] contentToWrite, LowLevel.COORD writeStartPos)
        {
            bool r = LowLevel.WriteConsoleOutput(screenBufferToWriteOn, contentToWrite, new LowLevel.COORD((short) contentToWrite.GetLength(1), (short) contentToWrite.GetLength(0)), writeStartPos, ref regionOfScreenBufferToWriteOn);
        }

    }
}