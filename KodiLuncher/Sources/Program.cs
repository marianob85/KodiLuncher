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
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //AllocConsole();
            bool boot = false;

            var parameters = new NDesk.Options.OptionSet()
                .Add("b", "Execute kodi on boot", b => boot = true);
            
            parameters.Parse (Environment.GetCommandLineArgs());

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

        static private void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            System.Diagnostics.MiniDump.CreateDump(@"d:\");
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
