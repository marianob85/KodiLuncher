using System;
using System.IO;
using System.Windows.Forms;
using KodiLuncher.Sources;

namespace KodiLuncher.Forms.Settings
{
    public partial class Settings : Form
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();
        private delegate UserControl CreateSettingsPanel( Object sender );
        private MqttClient m_mqttClient = null;

        public Settings( MqttClient mqttClient )
        {
            m_mqttClient = mqttClient;
            InitializeComponent();

            this.Icon = KodiLuncher.Properties.Resources.Tray;

            m_treeView.AfterSelect += new TreeViewEventHandler( EventNodeAfterSelect );

            CreateListView();
        }

        private void CreateListView()
        {
            m_treeView.Nodes.Clear();

            System.Windows.Forms.TreeNode nodeGeneral = new System.Windows.Forms.TreeNode();
            nodeGeneral.Name = "General";
            nodeGeneral.Text = "General";
            nodeGeneral.Tag = new CreateSettingsPanel( ( object sender ) => new KodiLuncher.Forms.Settings.Nodes.NodeGeneral( m_options ) );

            System.Windows.Forms.TreeNode nodeExternalApp = new System.Windows.Forms.TreeNode();
            nodeExternalApp.Name = "ExternalApplication";
            nodeExternalApp.Text = "External Application";
            nodeExternalApp.Tag = new CreateSettingsPanel( ( object sender ) => new KodiLuncher.Forms.Settings.Nodes.NodeExternalApp( m_options ) );

            System.Windows.Forms.TreeNode watchDog = new System.Windows.Forms.TreeNode();
            watchDog.Name = "WatchDog";
            watchDog.Text = "WatchDog";
            watchDog.Tag = new CreateSettingsPanel( ( object sender ) => new KodiLuncher.Forms.Settings.Nodes.NodeWatchDog( m_options ) );

            System.Windows.Forms.TreeNode mqtt = new System.Windows.Forms.TreeNode();
            mqtt.Name = "Mqtt";
            mqtt.Text = "Mqtt";
            mqtt.Tag = new CreateSettingsPanel( ( object sender ) => new KodiLuncher.Forms.Settings.Nodes.NodeMqtt( m_options, m_mqttClient ) );

            m_treeView.Nodes.Add( nodeGeneral );
            m_treeView.Nodes.Add( nodeExternalApp );
            m_treeView.Nodes.Add( watchDog );
            m_treeView.Nodes.Add( mqtt );
        }

        private void EventNodeAfterSelect( System.Object sender, System.Windows.Forms.TreeViewEventArgs e )
        {
            TreeView TreeView = sender as TreeView;
            TreeNode currentNode = e.Node;

            try
            {
                var panelCreator = currentNode.Tag as CreateSettingsPanel;

                UserControl panel = panelCreator(null);
                panel.Size = m_mainPanel.Size;
                panel.Anchor = ( System.Windows.Forms.AnchorStyles )( System.Windows.Forms.AnchorStyles.Bottom
                                                | System.Windows.Forms.AnchorStyles.Top
                                                | System.Windows.Forms.AnchorStyles.Left
                                                | System.Windows.Forms.AnchorStyles.Right );

                m_mainPanel.Controls.Clear();
                m_mainPanel.Controls.Add( panel );
            }
            catch( System.Exception )
            { }
        }

        private void m_import_Click( object sender, EventArgs e )
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.AddExtension = true;
            openFile.DefaultExt = "xml";
            openFile.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult result = openFile.ShowDialog();

            if( result == DialogResult.OK )
            {
                MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(openFile.FileName));
                stream.Seek( 0, SeekOrigin.Begin );
                m_options.import( stream );
                CreateListView();
            }
        }

        private void m_export_Click( object sender, EventArgs e )
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.AddExtension = true;
            saveFile.DefaultExt = "xml";
            saveFile.Filter = "Xml files (*.xml)|*.xml|All files (*.*)|*.*";
            DialogResult result = saveFile.ShowDialog();

            if( result == DialogResult.OK )
            {
                FileStream file = new FileStream(saveFile.FileName, FileMode.Create);
                MemoryStream test = m_options.export();
                test.WriteTo( file );
                file.Close();
            }
        }

        private void m_cancel_Click( object sender, EventArgs e )
        {
            m_options.read();
            Close();
            //Cancel(this, m_options.options);
        }

        private void m_save_Click( object sender, EventArgs e )
        {
            m_options.save();
            //Save(this, m_options.options);
        }

        private void m_close_Click( object sender, EventArgs e )
        {
            if( m_options.changed )
            {
                DialogResult dl = MessageBox.Show("Save changes ?", "File modified", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch( dl )
                {
                    case DialogResult.Cancel:
                        return;
                    case DialogResult.Yes:
                        {
                            m_options.save();
                            //Save(this, m_options.options);
                        }
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        return;
                }
            }
            Close();
            //Close(this, m_options.options);
        }
    }
}
