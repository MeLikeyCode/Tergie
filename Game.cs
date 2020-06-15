using System;

namespace Tergie
{
    public class Game
    {
        private static Scene _scene;
        private static Vector2 _pos;
        
        public static int WindowWidth => Console.WindowWidth;
        public static int WindowHeight => Console.WindowHeight;

        public static Vector2 Pos
        {
            get => _pos;
            set
            {
                _pos = value;
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
        public static Vector2 WindowToScene(Vector2 pos)
        {
            return new Vector2(_pos.X + pos.X,_pos.Y + pos.Y);
        }

        /// <summary>
        /// Convert a position in scene space to window space.
        /// </summary>
        public static Vector2 SceneToWindow(Vector2 pos)
        {
            return new Vector2(pos.X - _pos.X);            
        }

        /// <summary>
        /// Draw the region of the Scene that is visible through the window.
        /// </summary>
        public static void Draw()
        {
            // clear
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < WindowWidth * WindowHeight; i++)
                Console.Write(" ");
            
            // draw
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < WindowWidth; i++)
            {
                for (int j = 0; j < WindowHeight; j++)
                {
                    
                }
            }
            
        }

        public static void Start(Scene startingScene)
        {
            Scene = startingScene;
            while (true)
            {
                _scene.Update();
                Draw();
            }
        }

    }
}