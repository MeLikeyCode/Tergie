namespace Tergie.source
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a Scene
            Scene scene = new Scene(100,100);
            
            // add Entities to Scene (entities can have event call backs)
            Entity e = new Entity(Utils.FileToCharArray("../../../resources/ascii_art/scorpian.txt"));
            scene.AddEntity(e);
            e.Pos = new Vector2I(20,20);
            
            // start game
            Game.Start(scene);
        }
    }
}