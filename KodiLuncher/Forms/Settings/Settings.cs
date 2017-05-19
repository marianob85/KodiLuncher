using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KodiLuncher.Forms.Settings
{
    public partial class Settings : Form
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();

        public Settings()
        {
            InitializeComponent();

            m_treeView.AfterSelect += new TreeViewEventHandler(EventNodeAfterSelect);

            CreateListView();
        }

        private void CreateListView()
        {
            m_treeView.Nodes.Clear();
        }

        private void EventNodeAfterSelect(System.Object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            //TreeView hTreeView = sender as TreeView;
            //TreeNode hCurrentNode = e.Node;

            //try
            //{
            //    m_hMainPanel.Controls.Clear();
            //    UserControl hUserControl = hCurrentNode.Tag as UserControl;
            //    hUserControl.Size = m_hMainPanel.Size;
            //    hUserControl.Anchor = (System.Windows.Forms.AnchorStyles)(System.Windows.Forms.AnchorStyles.Bottom
            //                                    | System.Windows.Forms.AnchorStyles.Top
            //                                    | System.Windows.Forms.AnchorStyles.Left
            //                                    | System.Windows.Forms.AnchorStyles.Right);
            //    m_hMainPanel.Controls.Add(hUserControl);
            //}
            //catch (System.Exception)
            //{ }
        }
    }
}
