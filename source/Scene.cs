using System.Collections.Generic;

namespace Tergie.source
{
    public class Scene
    {
        public int Width => _width;
        public int Height => _height;

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            _entities.Remove(entity);
        }

        public Scene(int width, int height)
        {
            _width = width;
            _height = height;
            _characters = new char[height,width];
        }

        /// <summary>
        /// Called every frame. 
        /// </summary>
        public void Update(float dtMilliseconds)
        {
            // "draw" all entities in the scene
            Utils.SetChar(_characters,' '); // "clear"
            foreach (var entity in _entities) // "draw" each entity
                Utils.Blit(entity.Characters,_characters,entity.Pos);
        }

        public char CharAt(Vector2I pos)
        {
            return _characters[pos.Y, pos.X];
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
    }
}