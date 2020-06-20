
namespace Tergie.source
{
    /// <summary>
    /// Moves an entity along a specified vector.
    /// </summary>
    public class MoveAlong: Behavior
    {
        public float Speed { get; set; }

        public Vector2 Direction
        {
            get
            {
                return _direction;
            }
            set
            {
                _direction = value;
                _direction.Normalize();
            }
        }

        public MoveAlong(Entity entity, Vector2 direction,float speed)
        {
            _entity = entity;
            Speed = speed;
            Direction = direction;
        }

        public override void Update(float dtMilliseconds)
        {
            float seconds = dtMilliseconds / 1000;
            _entity.Pos += Direction * seconds * Speed;
        }
        
        // private stuff
        private Entity _entity;
        private Vector2 _direction;
    }
}