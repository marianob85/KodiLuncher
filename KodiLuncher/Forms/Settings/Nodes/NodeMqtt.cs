using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KodiLuncher.Forms.Settings.Nodes
{
    public partial class NodeMqtt : UserControl
    {
        private ProgramSettings.SettingsContainer m_options = null;

        public NodeMqtt(ProgramSettings.SettingsContainer options)
        {
            m_options = options;
            m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => RefreshValues());

            InitializeComponent();

            RefreshValues();
        }

        private void RefreshValues()
        {
            enable.Checked = m_options.options.applicationSettings.Mqtt.Enable;
            panel.Enabled = enable.Checked;
            port.Text = m_options.options.applicationSettings.Mqtt.Port.ToString();
            host.Text = m_options.options.applicationSettings.Mqtt.Host.ToString();
        }

        private void port_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void host_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.Mqtt.Host = textBox.Text;
        }

        private void port_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.Mqtt.Port = uint.Parse(textBox.Text);
        }

        private void enable_CheckedChanged(object sender, EventArgs e)
        {
            m_options.options.applicationSettings.Mqtt.Enable = (sender as CheckBox).Checked;
            panel.Enabled = (sender as CheckBox).Checked;
        }
    }
}
