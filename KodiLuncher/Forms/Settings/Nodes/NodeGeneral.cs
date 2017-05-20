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
            m_options.OptionsChanged += new EventHandler( (Object sender, EventArgs e) => RefreshValues());

            InitializeComponent();

            RefreshValues();
        }

        private void RefreshValues()
        {
            m_focusIntervalEnable.Checked = m_options.options.applicationSettings.FocusIntervalEnable;
            m_focusInterval.Text = m_options.options.applicationSettings.FocusIntervalMS.ToString();
            m_focusInterval.Enabled = m_focusIntervalEnable.Checked;

            m_kodiDelayEnable.Checked = m_options.options.applicationSettings.StartDelayEnable;
            m_kodiDelay.Text = m_options.options.applicationSettings.StartDelayMS.ToString();
            m_kodiDelay.Enabled = m_kodiDelayEnable.Checked;

            m_application.Text = m_options.options.applicationSettings.Application;
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

        private void m_kodiDelayEnable_CheckedChanged(object sender, EventArgs e)
        {
            m_options.options.applicationSettings.StartDelayEnable = (sender as CheckBox).Checked;
            m_kodiDelay.Enabled = m_options.options.applicationSettings.StartDelayEnable;
        }

        private void m_focusIntervalEnable_CheckedChanged(object sender, EventArgs e)
        {
            m_options.options.applicationSettings.FocusIntervalEnable = (sender as CheckBox).Checked;
            m_focusInterval.Enabled = m_options.options.applicationSettings.FocusIntervalEnable;
        }

        private void m_application_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.Application = textBox.Text;
        }

        private void m_browse_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog AppFileDialog = new System.Windows.Forms.OpenFileDialog();

            AppFileDialog.Filter += "Executable|*.exe";
            AppFileDialog.Multiselect = false;

            if (AppFileDialog.ShowDialog() == DialogResult.OK)
            {
                m_options.options.applicationSettings.Application = AppFileDialog.FileName;
                m_application.Text = AppFileDialog.FileName;



            }
        }

        private void m_application_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Array.IndexOf(System.IO.Path.GetInvalidPathChars(), e.KeyChar) > -1 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
