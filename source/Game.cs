using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Tergie.source
{
    public class Game
    {
        public delegate void KeyPressedEventHandler(ConsoleKey key);
        public delegate void KeyReleasedEventHandler(ConsoleKey key);

        public static event KeyPressedEventHandler KeyPressed;
        public static event KeyReleasedEventHandler KeyReleased;
        
        public static Window Window { get; private set; }
        public static List<ConsoleKey> PressedKeys => _pressedKeys;

        public static void Start(Scene startingScene)
        {
            // make cursor invisible
            Console.CursorVisible = false;

            Window = new Window(startingScene);
            
            // main loop
            var lastTime = DateTime.Now;
            while (true)
            {
                // handle keyboard input
                var oldKeysPressed = new List<ConsoleKey>(PressedKeys);
                PressedKeys.Clear();
                while (Console.KeyAvailable)
                    PressedKeys.Add(Console.ReadKey(false).Key);
                foreach (var key in PressedKeys)
                    if (!oldKeysPressed.Contains(key))
                        KeyPressed?.Invoke(key); // KeyPressedEvent
                foreach (var key in oldKeysPressed)
                    if (!PressedKeys.Contains(key))
                        KeyReleased?.Invoke(key); // KeyReleasedEvent
                
                // update
                var now = DateTime.Now;
                var dt = now - lastTime;
                lastTime = now;
                Window.Update((float)dt.TotalMilliseconds);
                
                // draw
                Window.Draw();
            }
        }

        // private stuff
        private static List<ConsoleKey> _pressedKeys = new List<ConsoleKey>();
    }
}