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
        
        public Scene(int width, int height)
        {
            _width = width;
            _height = height;
            _characters = new char[height,width];
            _charInfos = new LowLevel.CHAR_INFO[height,width];
        }

        public void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            KeyFocusedEntity?.OnKeyEvent(keyInfo);
        }

        List<CollisionEntity> entitiesIn(AARect2I region)
        {
            List<CollisionEntity> results = new List<CollisionEntity>();
            foreach (var entity in _collisionEntities)
            {
                if (entity.BoundingBox.CollidesWith(region))
                    results.Add(entity);
            }

            return results;
        }

        /// <summary>
        /// Draw all entities into the scene's character array. Expensive operation. Try to do a bunch of operations that dirties
        /// the character array (adding/removing entities, moving entities) and then call UpdateChars() just once.
        ///
        /// This gets called once per frame by the game's main loop, so it's always be relatively up to date. Only call this function
        /// if you need the character array up to date right away.
        /// </summary>
        public void UpdateChars()
        {
            // "draw" all entities in the scene
            Utils.SetChar(_characters,' '); // "clear"
            foreach (var entity in _entities) // "draw" each entity
                Utils.Blit(entity.Characters,_characters,entity.Pos,false);
            
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
            
            if (entity is CollisionEntity collisionEntity)
                _collisionEntities.Add(collisionEntity);
        }
        
        public void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);

            if (entity is CollisionEntity collisionEntity)
                _collisionEntities.Remove(collisionEntity);
        }

        /// <summary>
        /// Called every frame. 
        /// </summary>
        public void Update(float dtMilliseconds)
        {
            // call update of each entity
            foreach (var entity in _entities)
                entity.Update(dtMilliseconds);
        }

        // TODO, remove?
        public char[] Row(int number,int start, int end)
        {
            char[] emptyRow = new char[end - start];
            for (int i = 0; i < end - start; i++)
                emptyRow[i] = ' ';

            if (number < 0 || number >= Height)
                return emptyRow;
            
            char[] row = new char[end - start];
            for (int i = 0; i < end - start; i++)
            {
                if (i + start >= 0 && i + start < _width)
                    row[i] = _characters[number, i + start];
                else
                    row[i] = ' ';
            }

            return row;
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
        private List<CollisionEntity> _collisionEntities = new List<CollisionEntity>();
        private LowLevel.CHAR_INFO[,] _charInfos;
    }
}