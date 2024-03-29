﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace KodiLuncher
{
    public sealed class Kodi
    {
        private static volatile Kodi m_instance;
        private static object syncRoot = new Object();
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();

        public Kodi()
        {
        }

        ~Kodi()
        {

        }

        public static Kodi Instance
        {
            get
            {
                if( m_instance == null )
                {
                    lock( syncRoot )
                    {
                        if( m_instance == null )
                            m_instance = new Kodi();
                    }
                }

                return m_instance;
            }
        }

        public void Dispose()
        {
            m_options.Dispose();
        }

        public void Run()
        {
            System.Diagnostics.Process processCodeBeautifier = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfoCodeBeautifier = new System.Diagnostics.ProcessStartInfo();

            if( !System.IO.File.Exists( m_options.options.applicationSettings.Application ) )
                return;

            startInfoCodeBeautifier.FileName = m_options.options.applicationSettings.Application;
            processCodeBeautifier.StartInfo = startInfoCodeBeautifier;

            try
            {
                processCodeBeautifier.Start();
            }
            catch( System.ComponentModel.Win32Exception /*e*/)
            {
            }
            catch( Exception /*e*/)
            {
            }
        }

        public void SetFocus()
        {

            if( KodiLuncher.ContextMenu.contextDraw || KodiLuncher.ContextMenu.aboutDraw || KodiLuncher.ContextMenu.settingsDraw )
                return;

            foreach( var process in kodiProcesses() )
            {
                if( !process.HasExited )
                {
                    IntPtr handle = GetForegroundWindow();
                    if( handle != process.MainWindowHandle )
                    {
                        Console.WriteLine( "Focused" );
                        ShowWindow( process.MainWindowHandle, SW_RESTORE );
                        SetForegroundWindow( process.MainWindowHandle );
                    }
                }
            }
        }

        public void Close()
        {
            foreach( var process in kodiProcesses() )
            {
                try
                {
                    process.CloseMainWindow();
                    process.Close();
                    Thread.Sleep( 1000 );
                    if( !process.HasExited )
                        process.Kill();
                }
                catch
                {
                }
            }
        }

        public void Terminate()
        {
            killWer();
            foreach( var process in kodiProcesses() )
            {
                try
                {
                    if( !process.HasExited )
                        process.Kill();
                }
                catch( System.ComponentModel.Win32Exception )
                {
                    killWer();
                }

                Console.WriteLine( "Killed" );
            }
        }

        private void killWer()
        {
            foreach( var werProc in werProcess() )
            {
                werProc.Kill();
            }
        }

        public bool isKodiProcess()
        {
            return kodiProcesses().Any();
        }

        private System.Collections.Generic.IEnumerable<System.Diagnostics.Process> kodiProcesses()
        {
            return System.Diagnostics.Process.GetProcesses().
                     Where( pr => pr.ProcessName.CompareTo( Path.GetFileNameWithoutExtension( m_options.options.applicationSettings.Application ) ) == 0 );
        }

        private System.Collections.Generic.IEnumerable<System.Diagnostics.Process> werProcess()
        {
            return System.Diagnostics.Process.GetProcesses().
                     Where( pr => pr.ProcessName.CompareTo( "WerFault" ) == 0 );
        }


        private const uint SW_RESTORE = 0x09;

        [DllImport( "user32.dll" )]
        static extern IntPtr SetForegroundWindow( IntPtr hWnd );

        [DllImport( "user32.dll" )]
        static extern IntPtr GetForegroundWindow();

        [DllImport( "user32.dll" )]
        private static extern int ShowWindow( IntPtr hWnd, uint Msg );
    }
}
