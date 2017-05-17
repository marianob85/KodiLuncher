using System;
using System.Windows.Forms;
using KLuncher.Properties;

namespace KLuncher
{
    namespace TryIcon
    {
        class ProcessIcon : IDisposable
        {
            NotifyIcon m_notificationIcon;

            public ProcessIcon()
            {
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

                m_notificationIcon.ContextMenuStrip = new ContextMenu().Create();
            }
        }
    }
}