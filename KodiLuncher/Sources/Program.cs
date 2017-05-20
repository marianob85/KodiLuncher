﻿using System;
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

            Timer kodiTimer = new Timer();

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
