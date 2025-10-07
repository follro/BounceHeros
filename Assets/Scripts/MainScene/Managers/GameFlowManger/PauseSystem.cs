using System.Collections;

namespace BounceHeros
{
    public class PauseSystem 
    {
        public bool IsGamePause { get; private set; }

        public PauseSystem() 
        {
            IsGamePause = false;
        }

        public void Pause()
        {
            IsGamePause = true;
            UnityEngine.Time.timeScale = 0;
        }

        public void Resume()
        {
            IsGamePause = false;
            UnityEngine.Time.timeScale = 1f;
        }
    }
}