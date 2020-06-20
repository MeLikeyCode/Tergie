using System;

namespace Tergie.source
{
    class Program
    {
        static void Main(string[] args)
        {
            
            
            // create a Scene
            Scene scene = new Scene(400,400);

            // add entities to the scene
            FpsCounter fpsCounter = new FpsCounter();
            scene.AddEntity(fpsCounter);
            
            CollisionEntity player = new CollisionEntity(Utils.FileToCharArray("../../../resources/ascii_art/plane.txt",true));
            scene.AddEntity(player);
            Game.Behaviors.Add(new MoveInResponseToKeyboard(player,2));
            
            
            // start game
            Game.Start(scene,400,120);
        }
    }
}