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
    public partial class NodeGeneral : UserControl
    {
        ProgramSettings.SettingsContainer m_options = null;

        public NodeGeneral(ProgramSettings.SettingsContainer options)
        {
            m_options = options;
            m_options.onOptionsChanged += new EventHandler( (Object sender, EventArgs e) => RefreshValues());

            InitializeComponent();

            RefreshValues();
        }

        private void RefreshValues()
        {
            m_focusInterval.Text = m_options.options.applicationSettings.FocusIntervalMS.ToString();
            m_kodiDelay.Text = m_options.options.applicationSettings.StartDelayMS.ToString();
        }

        private void m_kodiDelay_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.StartDelayMS = Int64.Parse(textBox.Text);
        }

        private void m_focusInterval_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.FocusIntervalMS = Int64.Parse(textBox.Text);
        }
    }
}
