using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Tergie.source
{
    public class Game
    {
        public static Window Window { get; private set; }
        public static StreamWriter DebugLog { get; private set; }

        public static List<Behavior> Behaviors => _behaviors;

        public static void Start(Scene startingScene, int windowWidth, int windowHeight)
        {
            // initialize
            DebugLog = new StreamWriter("debug_log.txt");
            
            Console.SetWindowSize(windowWidth,windowHeight);
            Console.CursorVisible = false;

            Window = new Window(startingScene);
            
            // main loop
            var lastTime = DateTime.Now;
            while (true)
            {
                // handle keyboard input
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);
                    Window.OnKeyEvent(keyInfo);
                    foreach (var behavior in Behaviors)
                        behavior.OnKeyEvent(keyInfo);
                }

                // update
                var now = DateTime.Now;
                var dt = now - lastTime;
                lastTime = now;
                Window.Update((float)dt.TotalMilliseconds);
                foreach (var behavior in Behaviors)
                    behavior.Update((float) dt.TotalMilliseconds);
                
                // draw
                Window.Draw();
            }
        }

        // private stuff
        private static List<ConsoleKey> _pressedKeys = new List<ConsoleKey>();
        private static List<Behavior> _behaviors = new List<Behavior>();
    }
}