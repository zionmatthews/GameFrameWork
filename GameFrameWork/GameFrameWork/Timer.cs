using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameWork
{
    class Timer
    {
        Stopwatch _stopwatch = new Stopwatch();

        private long _currentTime = 0;
        private long _prevoiusTime = 0;

        private float _deltaTime = 0.005f;

        public Timer()
        {
            _stopwatch.Start();
        }

        public void Restart()
        {
           _stopwatch.Restart();
        }

        public float Seconds
        {
            get { return _stopwatch.ElapsedMilliseconds / 1000.0f; }
        }

        public float GetDeltaTime()
        {
            _prevoiusTime = _currentTime;
            _currentTime = _stopwatch.ElapsedMilliseconds / 100.0f;
            _deltaTime = (_currentTime - _prevoiusTime) / 1000.0f;
            return _deltaTime;
        }
    }
}
