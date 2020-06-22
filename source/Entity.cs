using System;
using System.Collections.Generic;
using System.IO;

namespace Tergie.source
{
    public class Entity
    {
        public Vector2 Pos { get; set; }
        public float Rotation { get; set; }
        public char[,] Characters { get; set; }
        
        /// <summary>
        /// A string-object dictionary you can use to store stuff in.
        /// </summary>
        public Dictionary<string, object> Data { get; private set; }

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
        
        public AARect2 BoundingBox => new AARect2(Pos,Characters.GetLength(1),Characters.GetLength(0));

        /// <summary>
        /// Emitted every frame.
        /// </summary>
        public event UpdatedCallback Updated;
        public delegate void UpdatedCallback(Entity sender,float dt);

        /// <summary>
        /// Emitted when the entity receives a key event.
        /// </summary>
        public event KeyPressedCallback KeyPressed;
        public delegate void KeyPressedCallback(Entity sender, ConsoleKeyInfo keyInfo);
        
        public Entity()
        {
            Data = new Dictionary<string, object>();
            Pos = new Vector2(0,0);
            Characters = new char[0,0];
        }
        
        public Entity(char[,] characters)
        {
            Data = new Dictionary<string, object>();
            Pos = new Vector2(0,0);
            Characters = characters;
        }

        public void OnKeyEvent(ConsoleKeyInfo keyInfo)
        {
            KeyPressed?.Invoke(this,keyInfo);
        }

        public void Update(float dt)
        {
            Updated?.Invoke(this,dt);
        }
        
        
    }
}