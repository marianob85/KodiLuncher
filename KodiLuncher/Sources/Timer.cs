using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiLuncher
{
    class Timer
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();

        System.Timers.Timer m_startTimer;
        System.Timers.Timer m_FocusTimer;

        public Timer()
        {
            m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => CreateTimers());

            m_startTimer = new System.Timers.Timer(m_options.options.applicationSettings.StartDelayMS);
            m_startTimer.Elapsed += OnTimedEvent;
            m_startTimer.Start();

            CreateTimers();
        }

        private void CreateTimers()
        {
            m_FocusTimer = new System.Timers.Timer(m_options.options.applicationSettings.FocusIntervalMS);
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
