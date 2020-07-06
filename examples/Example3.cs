using Tergie.source;

namespace Tergie.examples
{
    /// <summary>
    /// Hello! In this example, we make the entity detect when it has collided with another entity.
    /// </summary>
    public class Example3
    {
        public static void Start()
        {
            Scene scene = new Scene(80, 30);

            Entity entity = new Entity(Utils.GetTestGraphics());
            scene.AddEntity(entity);

            Entity entity2 = new Entity(Utils.GetTestGraphics());
            scene.AddEntity(entity2);
            entity2.Pos.X = 8;
            
            entity.Updated += (sender, dt) =>
            {
                entity.Pos.X += 1 * dt;
                // collision detection is done by checking bounding boxes
                if (entity.BoundingBox.CollidesWith(entity2.BoundingBox))
                    scene.RemoveEntity(entity2);
            };
            
            Game.Start(scene,80,30);
        }
    }
}