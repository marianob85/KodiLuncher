using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using KodiLuncher.TrayIcon;

namespace KodiLuncher
{
    static class Program
    {
        private static string appGuid = "c0a76b5a-12ab-45c5-b9d9-d693faa6e7b9";

        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                AppDomain currentDomain = AppDomain.CurrentDomain;
                currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (!mutex.WaitOne(0, false))
                {
                    Kodi.Instance.Run();
                    return;
                }

                //AllocConsole();
                bool boot = false;

                var parameters = new NDesk.Options.OptionSet()
                    .Add("b", "Execute kodi on boot", b => boot = true);

                parameters.Parse(Environment.GetCommandLineArgs());

                Timer kodiTimer = new Timer(boot);
                WatchDog watchDog = new WatchDog();

                // Show the system tray icon.					
                //using (KeyboardHook kh = new KeyboardHook())
                using (ProcessIcon pi = new ProcessIcon())
                {
                    pi.Display();

                    Console.WriteLine("App started");

                    // Make sure the application runs!
                    Application.Run();
                }
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
