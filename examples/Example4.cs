using Tergie.source;

namespace Tergie.examples
{
    /// <summary>
    /// Hello! In this example, we learn how to use Timers to do stuff periodically.
    /// You already know how to do something periodically using the Update() method of entities,
    /// but using Timers for periodic tasks is often more intuitive.
    /// </summary>
    public class Example4
    {
        public static void Start()
        {
            Scene scene = new Scene(80, 30);

            Entity entity = new Entity(Utils.GetTestGraphics());
            scene.AddEntity(entity);

            // let's use a timer to make the entity move down every 2 seconds
            Timer timer = new Timer(-1,2);
            
            // note: the -1 above specifies that the timer should continue to fire every 2 seconds, forever.
            // If instead of -1, we had passed in say, 5, then the timer would still execute every 2 seconds,
            // but only 5 times!
            
            Game.AddTimer(timer); // don't forget to add the Timer to the Game!
            timer.Start();
            timer.Fired += sender => // what do we wanna do each time the timer fires?
            {
                // move the entity down
                entity.Pos.Y += 3;
            };
            
            Game.Start(scene,80,30);
        }
    }
}