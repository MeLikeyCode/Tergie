using System;
using System.Diagnostics;

namespace Tergie
{
    public class Game
    {
        private static Scene _scene;
        private static Vector2I _windowPos = new Vector2I(0,0);
        
        public static int WindowWidth => Console.WindowWidth;
        public static int WindowHeight => Console.WindowHeight;

        public static Vector2I WindowPos
        {
            get => _windowPos;
            set
            {
                _windowPos = value;
                Draw();
            }
        }

        public static Scene Scene
        {
            get => _scene;
            set
            {
                _scene = value;
                Draw();
            }
        }

        /// <summary>
        /// Convert a position in window space to scene space.
        /// </summary>
        public static Vector2I WindowToScene(Vector2I windowPos)
        {
            return new Vector2I(_windowPos.X + windowPos.X,_windowPos.Y + windowPos.Y);
        }

        /// <summary>
        /// Convert a position in scene space to window space.
        /// </summary>
        public static Vector2I SceneToWindow(Vector2I scenePos)
        {
            return new Vector2I(scenePos.X - _windowPos.X,scenePos.Y - _windowPos.Y);            
        }

        /// <summary>
        /// Draw the region of the Scene that is visible through the window.
        /// </summary>
        public static void Draw()
        {
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < WindowHeight; i++)
            {
                for (int j = 0; j < WindowWidth; j++)
                {
                    Vector2I scenePos = WindowToScene(new Vector2I(j, i));
                    if (Scene.IsInBounds(scenePos))
                    {
                        char c = Scene.CharAt(scenePos);
                        Console.Write(c);
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
            }
        }

        public static void Start(Scene startingScene)
        {
            // make cursor invisible
            Process.Start("tput","civis"); 
            
            Scene = startingScene;
            // main loop
            var lastTime = DateTime.Now;
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var theKey = Console.ReadKey(true).Key;
                    if (theKey == ConsoleKey.W)
                        _windowPos.Y -= 1;
                    else if (theKey == ConsoleKey.S)
                        _windowPos.Y += 1;
                    else if (theKey == ConsoleKey.A)
                        _windowPos.X -= 1;
                    else if (theKey == ConsoleKey.D)
                        _windowPos.X += 1;
                }
                
                var now = DateTime.Now;
                var dt = now - lastTime;
                lastTime = now;
                _scene.Update((float)dt.TotalMilliseconds);
                Draw();
            }
        }

    }
}