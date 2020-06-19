using System;

namespace Tergie.source
{
    public class Entity
    {
        public Vector2I Pos { get; set; }
        public char[,] Characters { get; set; }

        public Entity()
        {
            Pos = new Vector2I(0,0);
            Characters = new char[0,0];
        }
        
        public Entity(char[,] characters)
        {
            Pos = new Vector2I(0,0);
            Characters = characters;
        }

        public virtual void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            // default implementation is empty (override in subclass)
        }

        public virtual void Update(float dtMilliseconds)
        {
            // default implementation is empty (override in subclass)
        }
    }
}