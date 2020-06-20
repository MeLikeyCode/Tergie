using System;

namespace Tergie.source
{
    public class Window
    {
        public Scene Scene { get; set; }
        public int Width => Console.WindowWidth;
        public int Height => Console.WindowHeight;
        
        public Vector2I Pos
        {
            get => _pos;
            set
            {
                _pos = value;
            }
        }

        public Window(Scene scene)
        {
            Scene = scene;

            _backBuffer = Utils.CreateScreenBuffer();
            _frontBuffer = Utils.CreateScreenBuffer();
            Utils.SetActiveScreenBuffer(_frontBuffer);
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
            // draw to back buffer
            LowLevel.CHAR_INFO[,] writeContent = Scene.CharInfos;
            Utils.WriteToScreenBuffer(_backBuffer,new LowLevel.SMALL_RECT(0,0,(short) Width,(short) Height),writeContent,new LowLevel.COORD((short) Pos.X,(short) Pos.Y) );
            
            // swap buffers
            IntPtr tempBuffer = _frontBuffer;
            _frontBuffer = _backBuffer;
            _backBuffer = tempBuffer;
            Utils.SetActiveScreenBuffer(_frontBuffer);
        }

        public void Update(float dtMilliseconds)
        {
            Scene.Update(dtMilliseconds);
        }

        public void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            Scene.OnKeyEvent(keyInfo);
        }

        // private stuff
        private Vector2I _pos = new Vector2I(0,0);
        private IntPtr _backBuffer;
        private IntPtr _frontBuffer;
    }
}