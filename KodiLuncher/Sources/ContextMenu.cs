using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KodiLuncher
{
    class ContextMenu
    {
        public ContextMenuStrip Create()
        {
            // Add the default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            item = new ToolStripMenuItem();
            item.Text = "Start Kodi";
            item.Click += new EventHandler(RunKodi);
            item.Image = KodiLuncher.Properties.Resources.Tray.ToBitmap();
            item.ShortcutKeyDisplayString = "Win+Alt+Enter";
            menu.Items.Add(item);

            item = new ToolStripMenuItem();
            item.Text = "Force Close Kodi Now";
            item.Click += new EventHandler(RunKodi);
            item.Image = KodiLuncher.Properties.Resources.Terminate;
            item.ShortcutKeyDisplayString = "Ctrl+F4";
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            item = new ToolStripMenuItem();
            item.Text = "Settings";
            //item.Click += new EventHandler(Explorer_Click);
            item.Image = KodiLuncher.Properties.Resources.Settings.ToBitmap();
            menu.Items.Add(item);

            // About.
            item = new ToolStripMenuItem();
            item.Text = "About";
            item.Image = KodiLuncher.Properties.Resources.StatusHelp.ToBitmap();
            item.Click += new EventHandler(About_Click);
            
            //item.Image = Resources.About;
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler(Exit_Click);
            item.Image = KodiLuncher.Properties.Resources.Close.ToBitmap();
            menu.Items.Add(item);

            return menu;
        }

        void Exit_Click(object sender, EventArgs e)
        {
            // Quit without further ado.
            Application.Exit();
        }

        void About_Click(object sender, EventArgs e)
        {
            Manobit.About about = new Manobit.About(AppDomain.CurrentDomain.GetAssemblies());
            about.StartPosition = FormStartPosition.WindowsDefaultLocation;
            about.ShowDialog();
        }

        void RunKodi(object sender, EventArgs e)
        {
            Kodi.Run();
        }
    }
}
