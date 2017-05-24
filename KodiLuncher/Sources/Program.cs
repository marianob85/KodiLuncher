using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using KodiLuncher.TrayIcon;


namespace KodiLuncher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //AllocConsole();

            String[] arguments = Environment.GetCommandLineArgs();
            bool boot = false;

            try
            {
                if ( arguments.Length > 0)
                {
                    var parsedArgs = arguments.Select(s => s.Split(new[] { ':' }, 1)).ToDictionary(s => s[0], s => s[1]);
                    boot = Boolean.Parse(parsedArgs["boot"]);
                }
            }
            catch (System.Exception)
            { }


            Timer kodiTimer = new Timer(boot);

            // Show the system tray icon.					
            using (KeyboardHook kh = new KeyboardHook())
            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();

                Console.WriteLine("App started");

#if DEBUG
                KodiLuncher.Forms.Settings.Settings settings = new KodiLuncher.Forms.Settings.Settings();
                settings.StartPosition = FormStartPosition.WindowsDefaultLocation;
                settings.ShowDialog();
#endif

                // Make sure the application runs!
                Application.Run();
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

    }
}
