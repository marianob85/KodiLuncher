using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Threading;

namespace KodiLuncher
{
    public sealed class Kodi
    {
        private static volatile Kodi m_instance;
        private static object syncRoot = new Object();
        private Thread m_kodiThread;
        System.Diagnostics.Process kodiProcess;
        System.Timers.Timer m_restartTimer;
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();

        private Kodi()
        {
            m_kodiThread = new Thread(new ThreadStart(kodiRunner));
        }

        ~Kodi()
        {
            m_options.Dispose();
        }

        public static Kodi Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (syncRoot)
                    {
                        if (m_instance == null)
                            m_instance = new Kodi();
                    }
                }

                return m_instance;
            }
        }

        public void Run()
        {
            if ( !m_kodiThread.IsAlive )
            {
                m_kodiThread = new Thread(new ThreadStart(kodiRunner));
                m_kodiThread.IsBackground = true;
                m_kodiThread.Start();
            }
        }

        private void kodiRunner()
        {
            kodiProcess = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfoCodeBeautifier = new System.Diagnostics.ProcessStartInfo();

            if (!System.IO.File.Exists(m_options.options.applicationSettings.Application))
                return;

            startInfoCodeBeautifier.FileName = m_options.options.applicationSettings.Application;
            startInfoCodeBeautifier.ErrorDialog = false;
            startInfoCodeBeautifier.RedirectStandardError = true;
            startInfoCodeBeautifier.RedirectStandardOutput = true;
            startInfoCodeBeautifier.CreateNoWindow = true;
            startInfoCodeBeautifier.UseShellExecute = false;


            kodiProcess.StartInfo = startInfoCodeBeautifier;
            kodiProcess.EnableRaisingEvents = true;
            
            try
            {
                kodiProcess.Start();
                kodiProcess.WaitForExit();
            }
            catch (System.ComponentModel.Win32Exception /*e*/)
            {
                restart();
            }
            catch (Exception /*e*/)
            {
                restart();
            }

            kodiProcess.Dispose();
            kodiProcess = null;
        }

        private void restart()
        {
            m_restartTimer = new System.Timers.Timer(10000);
            m_restartTimer.Elapsed += onRestart;
            m_restartTimer.Start();
        }

        private void onRestart(Object source, System.Timers.ElapsedEventArgs e)
        {
            System.Timers.Timer timer = source as System.Timers.Timer;
            timer.Close();
            Run();
        }

        public void SetFocus()
        {

            if (KodiLuncher.ContextMenu.contextDraw || KodiLuncher.ContextMenu.aboutDraw || KodiLuncher.ContextMenu.settingsDraw) 
                return;

            if (kodiProcess != null)
            {
                ShowWindow(kodiProcess.Handle, SW_RESTORE);
                SetForegroundWindow(kodiProcess.Handle);
            }
        }

        public void Terminate()
        {
            m_kodiThread.Abort();
            m_restartTimer.Close();
            if (kodiProcess !=null )
                kodiProcess.Kill();
        }

        private const uint SW_RESTORE = 0x09;

        [DllImport("user32.dll")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hWnd, uint Msg);
    }
}
