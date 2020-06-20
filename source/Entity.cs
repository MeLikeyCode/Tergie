using System;

namespace Tergie.source
{
    public class Entity
    {
        public Vector2 Pos { get; set; }
        public float Rotation { get; set; }
        public char[,] Characters { get; set; }

        public Vector2 FacingDirection
        {
            get
            {
                return Vector2.FromRotationAndMagnitude(Rotation,1);
            }
            set
            {
                Rotation = value.Rotation;
            }
        }

        public Entity()
        {
            Pos = new Vector2(0,0);
            Characters = new char[0,0];
        }
        
        public Entity(char[,] characters)
        {
            Pos = new Vector2(0,0);
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