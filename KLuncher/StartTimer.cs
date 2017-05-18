using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLuncher
{
    class StartTimer
    {
        System.Timers.Timer m_timer;

        public StartTimer()
        {
            m_timer = new System.Timers.Timer(5000);
            m_timer.Elapsed += OnTimedEvent;
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = source as System.Timers.Timer;
            timer.Close();

            Kodi.Run();
        }
    }
}
