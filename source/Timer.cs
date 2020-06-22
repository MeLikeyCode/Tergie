namespace Tergie.source
{
    public class Timer
    {
        public int Num { get; set; } // number of times this timer will fire each time Start() is called
        public float Period { get; set; } // how often this timer fires (in seconds)

        public event FiredCallback Fired;
        public delegate void FiredCallback(Timer sender);

        public Timer(int num, float period)
        {
            Num = num;
            Period = period;
        }

        public void Update(float dt)
        {
            if (Num != -1 && _numTimesFired >= Num)
                return;
            
            _timeElapsedSinceLastFire += dt;
            if (_timeElapsedSinceLastFire > Period)
            {
                Fired?.Invoke(this);
                _numTimesFired += 1;
                _timeElapsedSinceLastFire = 0;
            }
        }

        public void Start()
        {
            _timeElapsedSinceLastFire = 0;
            _numTimesFired = 0;
        }
        
        // private stuff
        private float _timeElapsedSinceLastFire; // number of milliseconds that have been elapsed since the last time the timer was fired
        private int _numTimesFired;
    }
}