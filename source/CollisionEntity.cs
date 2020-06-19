
namespace Tergie.source

{
    public class CollisionEntity: Entity
    {
        /// <summary>
        /// The bounding box of the entity.
        /// Coordinates of the bounding box are specified in entity space.
        /// </summary>
        public AARect2I BoundingBox { get; set; }

        public CollisionEntity(char[,] characters) : base(characters)
        {
            BoundingBox = new AARect2I(new Vector2I(0,0), characters.GetLength(1),characters.GetLength(0));
        }
    }
}