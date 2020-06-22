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

        public static Scene Scene
        {
            get
            {
                return Window.Scene;
            }
            set
            {
                Window.Scene = value;
            }
        }

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
                    foreach (var behavior in _behaviors)
                        behavior.OnKeyEvent(keyInfo);
                }

                // update
                var now = DateTime.Now;
                var dt = now - lastTime;
                float dtSeconds = (float) dt.TotalSeconds;
                lastTime = now;
                Window.Update(dtSeconds);
                foreach (var behavior in _behaviors)
                    behavior.Update(dtSeconds);
                foreach (var timer in _timers)
                    timer.Update(dtSeconds);
                
                // draw
                Window.Draw();
            }
        }

        public static void AddBehavior(Behavior behavior)
        {
            if (!_behaviors.Contains(behavior))
                _behaviors.Add(behavior);
        }

        public static void RemoveBehavior(Behavior behavior)
        {
            if (_behaviors.Contains(behavior))
                _behaviors.Remove(behavior);
        }

        public static void AddTimer(Timer timer)
        {
            if (!_timers.Contains(timer))
                _timers.Add(timer);
        }

        public static void RemoveTimer(Timer timer)
        {
            if (_timers.Contains(timer))
                _timers.Remove(timer);
        }

        /// <summary>
        /// Get all the entities in an enumerable that are colliding with a rect.
        /// </summary>
        public static HashSet<Entity> HitTest(IEnumerable<Entity> entities, AARect2 rect)
        {
            HashSet<Entity> results = new HashSet<Entity>();
            foreach (var entity in entities)
                if (entity.BoundingBox.CollidesWith(rect))
                    results.Add(entity);
            return results;
        }

        // private stuff
        private static List<ConsoleKey> _pressedKeys = new List<ConsoleKey>();
        private static List<Behavior> _behaviors = new List<Behavior>();
        private static List<Timer> _timers = new List<Timer>();
    }
}