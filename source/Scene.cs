using System;
using System.Collections.Generic;

namespace Tergie.source
{
    public class Scene
    {
        public int Width => _width;
        public int Height => _height;
        public LowLevel.CHAR_INFO[,] CharInfos => _charInfos;
        public Entity KeyFocusedEntity { get; set; }
        
        /// <summary>
        /// A string-object dictionary you can use to store stuff in.
        /// </summary>
        public Dictionary<string, object> Data { get; private set; }

        public event UpdatedCallback Updated;
        public delegate void UpdatedCallback(Scene sender, float dt);
        
        public event KeyPressedCallback KeyPressed;
        public delegate void KeyPressedCallback(Scene sender, ConsoleKeyInfo keyInfo);

        public Scene(int width, int height)
        {
            _width = width;
            _height = height;
            _characters = new char[height,width];
            _charInfos = new LowLevel.CHAR_INFO[height,width];
            Data = new Dictionary<string, object>();
        }

        public void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            KeyFocusedEntity?.OnKeyEvent(keyInfo);

            KeyPressed?.Invoke(this,keyInfo);
        }

        public HashSet<Entity> entitiesIn(AARect2 region)
        {
            return Game.HitTest(_entities, region);
        }

        private void DrawEntities()
        {
            Utils.SetChar(_characters,' '); // clear
            foreach (var entity in _entities) // "draw" each entity
                Utils.Blit(entity.Characters,_characters,entity.Pos.ToVector2I(),false);
            
            // keep CharInfos up to date with Characters
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    _charInfos[i, j].AsciiChar = _characters[i, j];
                    _charInfos[i, j].Attributes = 15;
                }
            }
        }
        
        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
            
        }
        
        public void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
        }

        public void Update(float dt)
        {
            DrawEntities();

            foreach (var entity in _entities.ToArray())
                entity.Update(dt);

            Updated?.Invoke(this,dt);
        }

        public bool IsInBounds(Vector2I pos)
        {
            return pos.X <= (_width - 1) && pos.X >= 0 && pos.Y >= 0 && pos.Y <= (_height - 1);
        }

        // private stuff
        private int _width;
        private int _height;
        private char[,] _characters;
        private List<Entity> _entities = new List<Entity>();
        private LowLevel.CHAR_INFO[,] _charInfos;
    }
}