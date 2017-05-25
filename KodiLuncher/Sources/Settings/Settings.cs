using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.ComponentModel;

namespace ProgramSettings
{
    public class SettingsContainer
    {
        public event EventHandler OptionsChanged = delegate { };

        private Options m_options = new Options();
        private RegistryUtils.RegistryMonitor monitor = new RegistryUtils.RegistryMonitor(RegistryHive.CurrentUser, @"Software\Manobit\KodiLuncher");

        public SettingsContainer()
        {
            read();

            monitor.RegChanged += new EventHandler(onRegChanged);
            monitor.Start();
        }

        ~SettingsContainer()
        {
            monitor.Stop();
        }

        private void onRegChanged(Object sender, EventArgs e)
        {
            read();
            OptionsChanged(this, e);
        }

        public Options options
        {
            get { return m_options; }
        }

        [XmlIgnore]
        public bool changed
        {
            get
            {
                try
                {
                    return !m_options.Equals(Options.read());
                }
                catch (System.ArgumentNullException)
                {
                    return !m_options.Equals(new Options());
                }
            }
        }

        public bool read()
        {
            try
            {
                m_options = Options.read();
                return true;
            }
            catch (System.ArgumentNullException)
            {
                m_options = new Options();
                return false;
            }
        }

        public bool save()
        {
            m_options.save();
            return true;
        }

        public MemoryStream export()
        {
            return m_options.export();
        }

        public bool import(MemoryStream stream)
        {
            try
            {
                m_options = Options.import(stream);
                return true;
            }
            catch (System.InvalidOperationException)
            {
                return false;
            }
        }
    }
}
