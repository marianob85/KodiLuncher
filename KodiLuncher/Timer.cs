using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiLuncher
{
    class Timer
    {
        System.Timers.Timer m_startTimer;
        System.Timers.Timer m_FocusTimer;

        public Timer()
        {
            m_startTimer = new System.Timers.Timer(5000);
            m_startTimer.Elapsed += OnTimedEvent;
            m_startTimer.Start();

            m_FocusTimer = new System.Timers.Timer(5000);
            m_FocusTimer.Elapsed += onFocusTimer;
            m_FocusTimer.Start();
        }

        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = source as System.Timers.Timer;
            timer.Close();

            Kodi.Run();
        }

        private static void onFocusTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            Kodi.SetFocus();
        }
    }
}
