﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using KLuncher.Properties;

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


            // Show the system tray icon.					
            using (NotifyIcon pi = new NotifyIcon())
            {
                pi.Icon = Resources.Tray;
                pi.Visible = true;

                // Make sure the application runs!
                Application.Run();
            }
        }
    }
}