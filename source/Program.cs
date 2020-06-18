namespace Tergie.source
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a Scene
            Scene scene = new Scene(100,100);
            
            // add Entities to Scene
            Entity e = new Entity(Utils.FileToCharArray("../../../resources/ascii_art/plane.txt",true));
            scene.AddEntity(e);
            
            Entity e2 = new Entity(Utils.FileToCharArray("../../../resources/ascii_art/plane.txt",true));
            e2.Pos = new Vector2I(5,5);
            scene.AddEntity(e2);

            
            // start game
            Game.Start(scene);
        }
    }
}