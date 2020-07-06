using Tergie.source;

namespace Tergie.examples
{
    /// <summary>
    /// Hello! In this example, we make an entity that will do *something* in its Update() method.
    ///
    /// The Update() method of every entity is called several times per second (usually around 60).
    /// It tells you (as an argument) how long since the last time it was called. Using that info,
    /// you can determine how much to move.
    /// </summary>
    public class Example2
    {
        public static void Start()
        {
            Scene scene = new Scene(80, 30);

            Entity entity = new Entity(Utils.GetTestGraphics());
            scene.AddEntity(entity);

            // make the entity move in its Update() method
            entity.Updated += (sender, dt) =>
            {
                entity.Pos.X += 1 * dt;
            };
            
            Game.Start(scene,80,30);
        }
    }
}