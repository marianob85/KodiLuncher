using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
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

            // Show the system tray icon.					
            using (KeyboardHook kh = new KeyboardHook())
            using (ProcessIcon pi = new ProcessIcon())
            {
                pi.Display();

                Console.WriteLine("App started");
                // Make sure the application runs!
                Application.Run();
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

    }
}
