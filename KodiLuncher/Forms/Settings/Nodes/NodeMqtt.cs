using System;
using System.Windows.Forms;
using KodiLuncher.Sources;

namespace KodiLuncher.Forms.Settings.Nodes
{
    public partial class NodeMqtt : UserControl
    {
        private ProgramSettings.SettingsContainer m_options = null;
        private MqttClient m_mqttClient = null;

        public NodeMqtt( ProgramSettings.SettingsContainer options, MqttClient mqttClient )
        {
            m_mqttClient = mqttClient;
            m_options = options;
            m_options.OptionsChanged += new EventHandler( ( Object sender, EventArgs e ) => RefreshValues() );

            InitializeComponent();

            RefreshValues();
        }

        private void RefreshValues()
        {
            enable.Checked = m_options.options.applicationSettings.Mqtt.Enable;
            panel.Enabled = enable.Checked;
            port.Text = m_options.options.applicationSettings.Mqtt.Port.ToString();
            host.Text = m_options.options.applicationSettings.Mqtt.Host.ToString();
            user.Text = m_options.options.applicationSettings.Mqtt.UserName.ToString();
            mqttAction.BackColor = m_mqttClient.Status() ? System.Drawing.Color.LightGreen : System.Drawing.Color.IndianRed;
        }

        private void port_KeyPress( object sender, KeyPressEventArgs e )
        {
            e.Handled = !char.IsDigit( e.KeyChar ) && !char.IsControl( e.KeyChar );
        }

        private void host_TextChanged( object sender, EventArgs e )
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.Mqtt.Host = textBox.Text;
        }

        private void port_TextChanged( object sender, EventArgs e )
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.Mqtt.Port = int.Parse( textBox.Text );
        }

        private void enable_CheckedChanged( object sender, EventArgs e )
        {
            m_options.options.applicationSettings.Mqtt.Enable = ( sender as CheckBox ).Checked;
            panel.Enabled = ( sender as CheckBox ).Checked;
        }

        private void mqttAction_Click( object sender, EventArgs e )
        {
            if( m_mqttClient.Status() )
                m_mqttClient.DisconnectClient();
            else
                m_mqttClient.ConnectClient();

            mqttAction.BackColor = m_mqttClient.Status() ? System.Drawing.Color.LightGreen : System.Drawing.Color.IndianRed;
        }

        private void user_TextChanged( object sender, EventArgs e )
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.Mqtt.UserName = textBox.Text;
        }

        private void password_TextChanged( object sender, EventArgs e )
        {
            TextBox textBox = sender as TextBox;
            m_options.options.applicationSettings.Mqtt.Password = textBox.Text;
        }
    }
}
