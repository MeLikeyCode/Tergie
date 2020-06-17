using System;

namespace Tergie.source
{
    public class Window
    {
        private Vector2I _pos = new Vector2I(0,0);
        
        public Scene Scene { get; set; }
        public int Width => Console.WindowWidth;
        public int Height => Console.WindowHeight;

        public Window(Scene scene)
        {
            Scene = scene;
        }

        public Vector2I Pos
        {
            get => _pos;
            set
            {
                _pos = value;
            }
        }
        
        /// <summary>
        /// Convert a position in window space to scene space.
        /// </summary>
        public Vector2I WindowToScene(Vector2I windowPos)
        {
            return new Vector2I(_pos.X + windowPos.X,_pos.Y + windowPos.Y);
        }

        /// <summary>
        /// Convert a position in scene space to window space.
        /// </summary>
        public Vector2I SceneToWindow(Vector2I scenePos)
        {
            return new Vector2I(scenePos.X - _pos.X,scenePos.Y - _pos.Y);            
        }
        
        /// <summary>
        /// Draw the region of the Scene that is visible through the window.
        /// </summary>
        public void Draw()
        {
            Console.SetCursorPosition(0,0);
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
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

        public void Update(float dtMilliseconds)
        {
            if (Game.PressedKeys.Contains(ConsoleKey.W))
                _pos.Y -= 1;
            if (Game.PressedKeys.Contains(ConsoleKey.S))
                _pos.Y += 1;
            if (Game.PressedKeys.Contains(ConsoleKey.A))
                _pos.X -= 1;
            if (Game.PressedKeys.Contains(ConsoleKey.D))
                _pos.X += 1;
            
            Scene.Update(dtMilliseconds);
        }
    }
}