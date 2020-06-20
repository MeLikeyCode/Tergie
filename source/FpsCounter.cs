namespace Tergie.source
{
    /// <summary>
    /// Constantly displays the current frames per second.
    /// </summary>
    public class FpsCounter: Entity
    {
        public FpsCounter()
        {
            Characters = new char[1,15];
        }

        public override void Update(float dtMilliseconds)
        {
            _elapsedTimeSeconds += dtMilliseconds / 1000;
            _numFramesPassed += 1;
            
            // update fps text every 50 frames
            if (_numFramesPassed % 50 == 0)
            {
                float fps = _numFramesPassed / _elapsedTimeSeconds;
                string fpsText = "fps: " + fps.ToString();
                Utils.Blit(Utils.StrToCharArray(fpsText),Characters,new Vector2I(0,0),false );

                _elapsedTimeSeconds = 0.0001f;
                _numFramesPassed = 0;
            }
        }

        private float _elapsedTimeSeconds = 0.0001f;
        private int _numFramesPassed = 0;
    }
}