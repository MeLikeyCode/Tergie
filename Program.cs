using System;
using System.Diagnostics;

namespace Tergie
{
    class Program
    {
        static void Main(string[] args)
        {
            Process.Start("tput","civis"); // make cursor invisible
            Console.WriteLine(Console.BufferHeight);
            Console.WriteLine(Console.WindowHeight);
            // Console.SetCursorPosition(0,0);
            
            // create a Scene
            Scene scene = new Scene(200,50);
            
            // add Entities to Scene (entities can have event call backs)
            
            // start game
            Game.Start(scene);

        }
    }
}