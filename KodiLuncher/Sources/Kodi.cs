using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace KodiLuncher
{
    public class Kodi
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();

        public Kodi()
        {
        }

        public void Run()
        {
            System.Diagnostics.Process processCodeBeautifier = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfoCodeBeautifier = new System.Diagnostics.ProcessStartInfo();

            if (!System.IO.File.Exists(m_options.options.applicationSettings.Application))
                return;

            startInfoCodeBeautifier.FileName = m_options.options.applicationSettings.Application;
            processCodeBeautifier.StartInfo = startInfoCodeBeautifier;

            try
            {
                processCodeBeautifier.Start();
            }
            catch (System.ComponentModel.Win32Exception /*e*/)
            {
            }
            catch (Exception /*e*/)
            {
            }
        }

        public void SetFocus()
        {

            if (KodiLuncher.ContextMenu.contextDraw)
                return;

            var kodiProcess = System.Diagnostics.Process.GetProcesses().
                     Where(pr => pr.ProcessName == "kodi");

            foreach (var process in kodiProcess)
            {
                if (!process.HasExited)
                {
                    IntPtr handle = GetForegroundWindow();
                    if (handle != process.MainWindowHandle)
                    {
                        Console.WriteLine("Focused");
                        SetForegroundWindow(process.MainWindowHandle);
                    }
                }
            }
        }

        public void Terminate()
        {
            var kodiProcess = System.Diagnostics.Process.GetProcesses().
                                 Where(pr => pr.ProcessName == "kodi");

            foreach (var process in kodiProcess)
            {
                if( !process.HasExited)
                    process.Kill();
                Console.WriteLine("Killed");
            }
        }

        [DllImport("user32.dll")]
        static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();
    }
}
