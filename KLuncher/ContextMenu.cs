using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KLuncher
{
    class ContextMenu
    {
        public  ContextMenuStrip Create()
        {
            // Add the default menu options.
            ContextMenuStrip menu = new ContextMenuStrip();
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            item = new ToolStripMenuItem();
            item.Text = "Start Kodi";
            item.Click += new EventHandler(RunKodi);
            //item.Image = Resources.Explorer;
            item.ShortcutKeyDisplayString = "Win+Alt+Enter";
            menu.Items.Add(item);

            item = new ToolStripMenuItem();
            item.Text = "Force Close Kodi Now";
            item.Click += new EventHandler(RunKodi);
            //item.Image = Resources.Explorer;
            item.ShortcutKeyDisplayString = "Ctrl+F4";
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            item = new ToolStripMenuItem();
            item.Text = "Settings";
            //item.Click += new EventHandler(Explorer_Click);
            //item.Image = Resources.Explorer;
            menu.Items.Add(item);

            // About.
            item = new ToolStripMenuItem();
            item.Text = "About";
            //item.Click += new EventHandler(About_Click);
            //item.Image = Resources.About;
            menu.Items.Add(item);

            // Separator.
            sep = new ToolStripSeparator();
            menu.Items.Add(sep);

            // Exit.
            item = new ToolStripMenuItem();
            item.Text = "Exit";
            item.Click += new System.EventHandler(Exit_Click);
            //item.Image = Resources.Exit;
            menu.Items.Add(item);

            return menu;
        }

        void Exit_Click(object sender, EventArgs e)
        {
            // Quit without further ado.
            Application.Exit();
        }

        void RunKodi(object sender, EventArgs e)
        {
            Kodi.Run();
        }
    }
}
