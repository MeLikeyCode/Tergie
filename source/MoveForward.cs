using System;

namespace Tergie.source
{
    /// <summary>
    /// Move an entity forward at some speed.
    /// </summary>
    public class MoveForward: Behavior
    {
        public int Speed { get; set; } // in characters per second

        public MoveForward(Entity entity, int speed)
        {
            _entity = entity;
            Speed = speed;
        }

        public override void Update(float dt)
        {
            _entity.Pos += _entity.FacingDirection * Speed * dt;
        }
        
        // private stuff
        private Entity _entity;
    }
}