using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tanks.BuisnessLogic
{
    class Timer
    {
        public static Timer instance;

        public static double DeltaTime
        {
            get
            {
                return instance._deltaTime;
            }
        }

        private long _lastTicks;
        private double _deltaTime = 0;
        
        public Timer()
        {
            instance = this;
            Tick();
        }

        public void Tick()
        {
            var newTicks = DateTime.Now.Ticks;
            _deltaTime = (newTicks - _lastTicks) / 1000;
            _lastTicks = newTicks;
        }
    }
}
