using System;
using System.Collections.Generic;
using System.IO;

namespace Tergie.source
{
    public class Game
    {
        public static Window Window { get; private set; }

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
                foreach (var doLater in _doLaters.ToArray())
                {
                    if (DateTime.Now > doLater.Item1)
                    {
                        doLater.Item2();
                        _doLaters.Remove(doLater);
                    }
                }
                
                // draw
                Window.Draw();
            }
        }

        /// <summary>
        /// Execute a function (that takes nothing and returns nothing) in a few seconds from now.
        /// </summary>
        public static void DoLater(Action function, float inHowLong)
        {
            DateTime when = DateTime.Now.AddSeconds(inHowLong);
            Action what = function;
            _doLaters.Add(new Tuple<DateTime,Action>(when,what));
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

        public static void DebugWrite(string what)
        {
            _debugLog.WriteLine(what);
        }

        // private stuff
        private static List<ConsoleKey> _pressedKeys = new List<ConsoleKey>();
        private static List<Behavior> _behaviors = new List<Behavior>();
        private static List<Timer> _timers = new List<Timer>();
        private static List<Tuple<DateTime,Action>> _doLaters = new List<Tuple<DateTime, Action>>();
        private static StreamWriter _debugLog = new StreamWriter("debug_log.txt");
        
    }
}