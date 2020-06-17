using System.Collections.Generic;
using System.IO;

namespace Tergie.source
{
    public class Utils
    {
        /// <summary>
        /// Create a 2d char array from a text file.
        /// </summary>
        public static char[,] FileToCharArray(string file)
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
                    List<char> lineList = new List<char>();
                    for (int i = 0; i < line.Length; i++)
                        lineList.Add(line[i]);
                    fileContent.Add(lineList);
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
                        contentArray[i, j] = ' ';
                }
            }

            return contentArray;
        }

        public static void Blit(char[,] source, char[,] dest, Vector2I pos)
        {
            int startX = pos.X;
            int startY = pos.Y;
            int width = source.GetLength(1);
            int height = source.GetLength(0);
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    dest[i + startY, j + startX] = source[i, j];
                }
            }
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

    }
}