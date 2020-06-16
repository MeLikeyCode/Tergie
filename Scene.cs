using System.Collections.Generic;
using System.IO;

namespace Tergie
{
    public class Scene
    {
        private int _width;
        private int _height;
        private char[,] _characters;

        public int Width => _width;
        public int Height => _height;

        /// <summary>
        /// Create a Scene from a text file that contains ASCII art.
        /// </summary>
        public static Scene FromAsciiFile(string filepath)
        {
            // read file and build 2d char array
            List<List<char>> fileContent = new List<List<char>>();
            int width = 0;
            int height = 0;
            using (StreamReader reader = new StreamReader(filepath))
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
            
            Scene scene = new Scene(width,height);
            scene._characters = contentArray;

            return scene;
        }

        public Scene(int width, int height)
        {
            _width = width;
            _height = height;
            
            _characters = new char[100,200];
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    _characters[i,j] = 'c';
                }

                if (i > 10)
                {
                    for (int j = 0; j < 200; j++)
                    {
                        _characters[i,j] = 'd';
                    }
                }
            }
        }

        /// <summary>
        /// Called every frame. 
        /// </summary>
        public void Update(float dtMilliseconds)
        {
            
        }

        public char CharAt(Vector2I pos)
        {
            return _characters[pos.Y, pos.X];
        }

        public bool IsInBounds(Vector2I pos)
        {
            return pos.X <= (_width - 1) && pos.X >= 0 && pos.Y >= 0 && pos.Y <= (_height - 1);
        }
    }
}