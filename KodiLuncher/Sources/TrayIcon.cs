using System;
using System.Windows.Forms;
using KodiLuncher.Properties;
using KodiLuncher.Sources;

namespace KodiLuncher
{
    namespace TrayIcon
    {
        class ProcessIcon : IDisposable
        {
            NotifyIcon m_notificationIcon;
            private MqttClient m_mqttClient = null;

            public ProcessIcon( MqttClient mqttClient )
            {
                m_mqttClient = mqttClient;

                // Instantiate the NotifyIcon object.
                m_notificationIcon = new NotifyIcon();
            }

            public void Dispose()
            {
                // When the application closes, this will remove the icon from the system tray immediately.
                m_notificationIcon.Dispose();
            }

            public void Display()
            {
                m_notificationIcon.Icon = Resources.Tray;
                m_notificationIcon.Text = "Kodi luncher";
                m_notificationIcon.Visible = true;

                m_notificationIcon.ContextMenuStrip = new ContextMenu().Create( m_mqttClient );
            }
        }
    }
}