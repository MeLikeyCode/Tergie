namespace Tergie.source
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a Scene
            Scene scene = new Scene(100,100);
            
            // add Entities to Scene
            Entity e = new Entity(Utils.FileToCharArray("../../../resources/ascii_art/plane.txt"));
            scene.AddEntity(e);
            e.Pos = new Vector2I(0,0);
            
            // start game
            Game.Start(scene);
        }
    }
}