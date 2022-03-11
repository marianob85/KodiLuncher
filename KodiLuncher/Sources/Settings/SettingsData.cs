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
    public class ExternalAppSettings
    {
        public String AppPath = String.Empty;
        public bool PreventFocus = false;

        public override int GetHashCode()
        {
            return AppPath.GetHashCode()
                    * PreventFocus.GetHashCode() ^ 2;
        }

        public override bool Equals(Object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType())
                return false;

            return Equals((ExternalAppSettings)other);
        }

        private bool Equals(ExternalAppSettings options)
        {
            return AppPath.Equals(options.AppPath)
                    && PreventFocus.Equals(options.PreventFocus);
        }
    }

    public class WatchDog
    {
        public bool Enable = false;
        public uint Port = 8080;
        public uint CheckInterval = 5000;

        public override int GetHashCode()
        {
            return Enable.GetHashCode()
                    * Port.GetHashCode() ^ 2
                    * CheckInterval.GetHashCode() ^ 3;
        }

        public override bool Equals(Object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType())
                return false;

            return Equals((WatchDog)other);
        }

        private bool Equals(WatchDog options)
        {
            return Enable.Equals(options.Enable)
                    && Port.Equals(options.Port)
                    && CheckInterval.Equals(options.CheckInterval);
        }
    }

    public class MQTT
    {
        public bool Enable = false;
        public uint Port = 8080;
        public String Host = String.Empty;
        public override int GetHashCode()
        {
            return Enable.GetHashCode()
                    * Port.GetHashCode() ^ 2
                     * Host.GetHashCode() ^ 3;
        }

        public override bool Equals(Object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType())
                return false;

            return Equals((MQTT)other);
        }

        private bool Equals(MQTT options)
        {
            return Enable.Equals(options.Enable)
                 && Port.Equals(options.Port)
                 && Host.Equals(options.Host);
        }
    }

    public class ApplicationSettings
    {
        public Int64 StartDelayMS = 5000;
        public bool StartDelayEnable = false;
        public Int64 FocusIntervalMS = 5000;
        public bool FocusIntervalEnable = false;
        public List<ExternalAppSettings> ExtApp = new List<ExternalAppSettings>();
        public String Application = String.Empty;
        public WatchDog AppWatchDog = new WatchDog();
        public MQTT Mqtt = new MQTT();

        public ApplicationSettings()
        {

        }

        public override int GetHashCode()
        {
            return StartDelayMS.GetHashCode()
                    * StartDelayEnable.GetHashCode() ^ 2
                    * FocusIntervalMS.GetHashCode() ^ 3
                    * FocusIntervalEnable.GetHashCode() ^ 4
                    * ExtApp.GetHashCode() ^ 5
                    * Application.GetHashCode() ^ 6
                    * AppWatchDog.GetHashCode() ^ 7
                    * Mqtt.GetHashCode() ^ 8;
        }

        public override bool Equals(Object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType())
                return false;

            return Equals((ApplicationSettings)other);
        }

        private bool Equals(ApplicationSettings options)
        {
            return StartDelayMS.Equals(options.StartDelayMS)
                    && StartDelayEnable.Equals(options.StartDelayEnable)
                    && FocusIntervalMS.Equals(options.FocusIntervalMS)
                    && FocusIntervalEnable.Equals(options.FocusIntervalEnable)
                    && ExtApp.TrueForAll(options.ExtApp.Contains) && options.ExtApp.TrueForAll(ExtApp.Contains)
                    && Application.Equals(options.Application)
                    && AppWatchDog.Equals(options.AppWatchDog)
                    && Mqtt.Equals(options.Mqtt);
        }
    }

    [XmlRootAttribute("KodiLuncher", Namespace = "http://www.manobit.com")]
    public class Options
    {
        private ApplicationSettings m_applications = new ApplicationSettings();

        public Options()
        { }

        public ApplicationSettings applicationSettings
        {
            get { return m_applications; }
            set
            {
                m_applications = value;
            }
        }

        internal void save()
        {
            MemoryStream stream = serialize();

            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey sub_keyManobit = reg_key.CreateSubKey("Manobit");
            RegistryKey sub_key = sub_keyManobit.CreateSubKey("KodiLuncher");
            sub_key.SetValue("Options", stream.ToArray());
        }

        static internal Options read()
        {
            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey sub_keyManobit = reg_key.CreateSubKey("Manobit");
            RegistryKey sub_key = sub_keyManobit.CreateSubKey("KodiLuncher");

            byte[] settings = (byte[])sub_key.GetValue("Options");
            return deserialize(new MemoryStream(settings));
        }

        private MemoryStream serialize()
        {
            XmlSerializer serializer = new XmlSerializer(this.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.Serialize(stream, this);
            return stream;
        }

        static private Options deserialize(MemoryStream stream)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof(Options));
            return (Options)deserializer.Deserialize(stream);
        }

        internal MemoryStream export()
        {
            return serialize();
        }

        static internal Options import(MemoryStream stream)
        {
            return deserialize(stream);
        }

        public override int GetHashCode()
        {
            return m_applications.GetHashCode() ^ 2;
        }

        public override bool Equals(Object other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (this.GetType() != other.GetType())
                return false;

            return Equals((Options)other);
        }

        private bool Equals(Options options)
        {
            return m_applications.Equals(options.m_applications);
        }
    }
}