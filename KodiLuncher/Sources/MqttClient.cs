using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KodiLuncher.Sources
{
    internal class MqttClient
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();

        public MqttClient()
        {
            m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => ConnectClient());
            ConnectClient();
        }

        private void ConnectClient()
        {

        }
    }
}
