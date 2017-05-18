using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using KLuncher.TryIcon;

namespace KLuncher
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

            AllocConsole();

            System.Timers.Timer startTimer = new System.Timers.Timer(5000);
            startTimer.Elapsed += async (sender, e) => await HandleTimer(sender);
            //startTimer.Elapsed += OnTimedEvent;
            startTimer.Start();

            // Show the system tray icon.					
            using(KeyboardHook kh = new KeyboardHook())
            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();

                Console.WriteLine("App started");
                // Make sure the application runs!
                Application.Run();
            }
        }
        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            Console.WriteLine("\nHandler not implemented...");
        }

        private static Task HandleTimer(Object source)
        {
            System.Timers.Timer timer = source as System.Timers.Timer;
            timer.Close();

            Console.WriteLine("\nHandler not implemented...");
            Task taskA = new Task(() => Console.WriteLine("Hello from taskA."));
            return taskA;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

    }
}
