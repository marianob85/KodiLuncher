using System;
using System.Windows.Forms;
using KodiLuncher.Sources;

namespace KodiLuncher
{
    class ContextMenu
    {
        // TODO ! Refactor 
        public static bool contextDraw = false;
        public static bool aboutDraw = false;
        public static bool settingsDraw = false;
        private MqttClient m_mqttClient = null;

        public ContextMenuStrip Create( MqttClient mqttClient )
        {
            m_mqttClient = mqttClient;

            // Add the default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
            menu.VisibleChanged += Menu_VisibleChanged;
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            item = new ToolStripMenuItem();
            item.Text = "Start Kodi";
            item.Click += new EventHandler( RunKodi );
            item.Image = KodiLuncher.Properties.Resources.Tray.ToBitmap();
            item.ShortcutKeyDisplayString = "Win+Alt+Enter";
            menu.Items.Add( item );

            item = new ToolStripMenuItem();
            item.Text = "Force Close Kodi Now";
            item.Click += new EventHandler( TerminateKodi );
            item.Image = KodiLuncher.Properties.Resources.Terminate;
            item.ShortcutKeyDisplayString = "Ctrl+F4";
            menu.Items.Add( item );

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add( sep );

            item = new ToolStripMenuItem();
            item.Text = "Start with windows";
            item.Click += new EventHandler( SwitchAppAutoStart );
            item.Checked = new Sources.AutoStart().Enable;
            menu.Items.Add( item );

            item = new ToolStripMenuItem();
            item.Text = "Settings";
            item.Click += new EventHandler( Settings_Click );
            item.Image = KodiLuncher.Properties.Resources.Settings.ToBitmap();
            menu.Items.Add( item );

            // About
            item = new ToolStripMenuItem();
            item.Text = "About";
            item.Image = KodiLuncher.Properties.Resources.StatusHelp.ToBitmap();
            item.Click += new EventHandler( About_Click );
            menu.Items.Add( item );

            // Separator
            sep = new ToolStripSeparator();
            menu.Items.Add( sep );

            // Exit
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler( Exit_Click );
            item.Image = KodiLuncher.Properties.Resources.Close.ToBitmap();
            menu.Items.Add( item );

            return menu;
        }

        private void SwitchAppAutoStart( object sender, EventArgs e )
        {
            ToolStripMenuItem obj = sender as ToolStripMenuItem;
            var autoStart = new Sources.AutoStart();
            autoStart.Enable = !autoStart.Enable;

            obj.Checked = autoStart.Enable;

        }

        private void Menu_VisibleChanged( object sender, EventArgs e )
        {
            var cm = sender as ContextMenuStrip;

            contextDraw = cm.Visible;
        }

        void Settings_Click( object sender, EventArgs e )
        {
            settingsDraw = true;
            KodiLuncher.Forms.Settings.Settings settings = new KodiLuncher.Forms.Settings.Settings(m_mqttClient);
            settings.StartPosition = FormStartPosition.WindowsDefaultLocation;
            settings.ShowDialog();
            settingsDraw = false;
        }

        void Exit_Click( object sender, EventArgs e )
        {
            // Quit without further ado.
            Application.Exit();
        }

        void About_Click( object sender, EventArgs e )
        {
            aboutDraw = true;
            Manobit.About about = new Manobit.About(AppDomain.CurrentDomain.GetAssemblies());
            about.StartPosition = FormStartPosition.WindowsDefaultLocation;
            about.ShowDialog();
            aboutDraw = false;
        }

        void RunKodi( object sender, EventArgs e )
        {
            Kodi.Instance.Run();
        }

        void TerminateKodi( object sender, EventArgs e )
        {
            Kodi.Instance.Terminate();
        }
    }
}
