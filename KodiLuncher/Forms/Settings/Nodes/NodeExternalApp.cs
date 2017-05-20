using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KodiLuncher.Forms.Settings.Nodes
{
    public partial class NodeExternalApp : UserControl
    {
        ProgramSettings.SettingsContainer m_options = null;

        public NodeExternalApp(ProgramSettings.SettingsContainer options)
        {
            m_options = options;
            m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => RefreshValues());
            InitializeComponent();
            RefreshValues();
        }

        private void RefreshValues()
        {
            listView.Items.Clear();

            foreach( var item in m_options.options.applicationSettings.ExtApp)
            {
                ListViewItem test = new ListViewItem();

                test.SubItems.Add();

                listView.Items.Add(test);
            }
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            m_options.options.applicationSettings.ExtApp.Add(new ProgramSettings.ExternalAppSettings()) ;

            RefreshValues();
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
