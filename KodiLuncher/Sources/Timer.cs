using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace KodiLuncher
{
    class Timer
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();

        System.Timers.Timer m_startTimer;
        System.Timers.Timer m_FocusTimer;

        public Timer( bool boot )
        {
            m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => CreateTimers());

            if (m_options.options.applicationSettings.StartDelayEnable && boot)
            {
                m_startTimer = new System.Timers.Timer(m_options.options.applicationSettings.StartDelayMS);
                m_startTimer.Elapsed += OnTimedEvent;
                m_startTimer.Start();
            }

            CreateTimers();
        }

        private void CreateTimers()
        {
            m_FocusTimer = null;

            if (m_options.options.applicationSettings.FocusIntervalEnable)
            {
                m_FocusTimer = new System.Timers.Timer(m_options.options.applicationSettings.FocusIntervalMS);
                m_FocusTimer.Elapsed += onFocusTimer;
                m_FocusTimer.Start();
            }
        }

        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = source as System.Timers.Timer;
            timer.Close();

            new Kodi().Run();
        }

        private void onFocusTimer(Object source, System.Timers.ElapsedEventArgs e)
        {
            foreach ( var app in m_options.options.applicationSettings.ExtApp )
            {
                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses())
                {
                    if ( app.PreventFocus && GetProcessName( process.Id ) == app.AppPath)
                        return;
                }
            }
            new Kodi().SetFocus();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetProcessName(int pid)
        {
            var processHandle = OpenProcess(0x0400 | 0x0010, false, pid);

            if (processHandle == IntPtr.Zero)
            {
                return null;
            }

            const int lengthSb = 4000;

            var sb = new StringBuilder(lengthSb);

            string result = null;

            if (GetModuleFileNameEx(processHandle, IntPtr.Zero, sb, lengthSb) > 0)
            {
                result = sb.ToString();
            }

            CloseHandle(processHandle);

            return result;
        }

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, int processId);

        [DllImport("psapi.dll")]
        static extern uint GetModuleFileNameEx(IntPtr hProcess, IntPtr hModule, [Out] StringBuilder lpBaseName, [In] [MarshalAs(UnmanagedType.U4)] int nSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);
    }
}
