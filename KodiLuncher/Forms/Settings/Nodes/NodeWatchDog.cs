﻿using System;
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
    public partial class NodeWatchDog : UserControl
    {
        ProgramSettings.SettingsContainer m_options = null;

        public NodeWatchDog(ProgramSettings.SettingsContainer options)
        {
            m_options = options;
            m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => RefreshValues());

            InitializeComponent();

            RefreshValues();
        }

        private void RefreshValues()
        {
            checkBoxEnable.Checked = m_options.options.applicationSettings.AppWatchDog.Enable;
            textBoxPort.Text = m_options.options.applicationSettings.AppWatchDog.Port.ToString();
            textBoxInterval.Text = m_options.options.applicationSettings.AppWatchDog.CheckInterval.ToString();
            panelSettings.Enabled = m_options.options.applicationSettings.AppWatchDog.Enable;
        }

        private void textBoxPort_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.AppWatchDog.Port = uint.Parse(textBox.Text);
        }

        private void checkBoxEnable_CheckedChanged(object sender, EventArgs e)
        {
            m_options.options.applicationSettings.AppWatchDog.Enable = (sender as CheckBox).Checked;
            panelSettings.Enabled = m_options.options.applicationSettings.AppWatchDog.Enable;
        }

        private void textBoxInterval_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.AppWatchDog.CheckInterval = uint.Parse(textBox.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WatchDog watchDog = new WatchDog(false);
            buttonTest.BackColor = watchDog.Ping() ? Color.Green : Color.Red;


        }
    }
}
