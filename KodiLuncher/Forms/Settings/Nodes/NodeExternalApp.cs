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
            tbAppPath.Enabled = false;
            preventFocus.Enabled = false;
            browseProgram.Enabled = false;
            RefreshValues();
        }

        private void RefreshValues(ProgramSettings.ExternalAppSettings selectedApp = null)
        {
            listView.Items.Clear();

            foreach( var item in m_options.options.applicationSettings.ExtApp)
            {
                ListViewItem listItem = new ListViewItem(item.AppPath);
                listItem.SubItems.Add(item.PreventFocus.ToString());
                listItem.Tag = item;

                listView.Items.Add(listItem);
            }


            foreach (ListViewItem item in listView.Items)
            {
                if (item.Tag as ProgramSettings.ExternalAppSettings == selectedApp)
                    item.Selected = true;
            }
        }

        

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newApp = new ProgramSettings.ExternalAppSettings();
            m_options.options.applicationSettings.ExtApp.Add(newApp);
            RefreshValues();

            // Find new added
            foreach (ListViewItem selectedItem in listView.Items)
            {
                if( selectedItem.Tag as ProgramSettings.ExternalAppSettings == newApp )
                {
                    selectedItem.Selected = true;
                    break;
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem selectedItem in listView.SelectedItems)
            {
                var selectedApp = selectedItem.Tag as ProgramSettings.ExternalAppSettings;
                m_options.options.applicationSettings.ExtApp.Remove(selectedApp);
            }

            RefreshValues();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView lw = sender as ListView;

            this.tbAppPath.Clear();
            this.preventFocus.Checked = false;

            if ( lw.SelectedItems.Count == 1 )
            {
                ProgramSettings.ExternalAppSettings appSettings = lw.SelectedItems[0].Tag as ProgramSettings.ExternalAppSettings;
                this.tbAppPath.Text = appSettings.AppPath;
            }
            this.tbAppPath.Enabled = lw.SelectedItems.Count == 1;
            this.preventFocus.Enabled = lw.SelectedItems.Count > 0;
            browseProgram.Enabled = lw.SelectedItems.Count == 1;
            bool? checkValue = null;

            foreach(ListViewItem selectedItem in lw.SelectedItems)
            {
                ProgramSettings.ExternalAppSettings appSettings = selectedItem.Tag as ProgramSettings.ExternalAppSettings;
                if (!checkValue.HasValue)
                    checkValue = appSettings.PreventFocus;
                else if (checkValue != appSettings.PreventFocus)
                {
                    checkValue = null;
                    break;
                }
            }

            if (checkValue.HasValue)
                this.preventFocus.Checked = checkValue.Value;
            else
                this.preventFocus.CheckState =  CheckState.Indeterminate;
        }

        private void tbAppPath_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.Focused)
            {
                ProgramSettings.ExternalAppSettings appSettings = listView.SelectedItems[0].Tag as ProgramSettings.ExternalAppSettings;

                appSettings.AppPath = textBox.Text;
                listView.SelectedItems[0].SubItems[0].Text = appSettings.AppPath;
            }
        }

        private void preventFocus_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;

            if (checkBox.Focused)
            {
                ProgramSettings.ExternalAppSettings appSettings = listView.SelectedItems[0].Tag as ProgramSettings.ExternalAppSettings;

                appSettings.PreventFocus = checkBox.Checked;

                listView.SelectedItems[0].SubItems[1].Text = appSettings.PreventFocus.ToString();
            }
        }

        private void browseProgram_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.OpenFileDialog AppFileDialog = new System.Windows.Forms.OpenFileDialog();

            AppFileDialog.Filter += "Executable|*.exe";
            AppFileDialog.Multiselect = false;

            if (AppFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.tbAppPath.Text = AppFileDialog.FileName;
                ProgramSettings.ExternalAppSettings app = listView.SelectedItems[0].Tag as ProgramSettings.ExternalAppSettings;
                app.AppPath = AppFileDialog.FileName;
                RefreshValues(app);
            }
        }

        private void tbAppPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Array.IndexOf(System.IO.Path.GetInvalidPathChars(), e.KeyChar) > -1 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
