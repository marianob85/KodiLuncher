using System;
using System.Windows.Forms;
using KLuncher.Properties;

namespace KLuncher
{
    namespace TryIcon
    {
        class ProcessIcon : IDisposable
        {
            NotifyIcon ni;

            public ProcessIcon()
            {
                // Instantiate the NotifyIcon object.
                ni = new NotifyIcon();
            }

            public void Dispose()
            {
                // When the application closes, this will remove the icon from the system tray immediately.
                ni.Dispose();
            }

            public void Display()
            {
                ni.Icon = Resources.Tray;
                ni.Text = "Kodi luncher";
                ni.Visible = true;

                ni.ContextMenuStrip = new ContextMenu().Create();
            }
        }

    }
}