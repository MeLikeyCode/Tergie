using Tergie.source;

namespace Tergie.examples
{
    /// <summary>
    /// Hello! This short and simple example shows the *very* basics of the game engine.
    /// </summary>
    public class Example1
    {
        public static void Start()
        {
            // create a scene
            int sceneWidth = 120;
            int sceneHeight = 60;
            Scene scene = new Scene(sceneWidth,sceneHeight);
            
            // create an entity

            // first, create the graphics for the entity (just a 2d array of characters)
            char[,] graphics = new char[,]
            {
                {'x','x','x'},
                {'x','o','x'},
                {'x','x','x'},
            };

            // now create the entity
            Entity entity = new Entity(graphics);
            
            // add the entity to the scene
            scene.AddEntity(entity);

            // start the game
            int windowWidth = sceneWidth;
            int windowHeight = sceneHeight;
            Game.Start(scene,windowWidth,windowHeight);
        }
    }
}