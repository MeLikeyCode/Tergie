namespace Tergie
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a Scene
            Scene scene = Scene.FromAsciiFile("../../../afg.txt");
            
            // add Entities to Scene (entities can have event call backs)
            
            // start game
            Game.Start(scene);

        }
    }
}