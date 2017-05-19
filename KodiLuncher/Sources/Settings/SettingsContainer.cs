﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.ComponentModel;

namespace ProgramSettings
{
    public class ApplicationSettings
    {
        public Int64 StartDelayMS = 5000;
        public Int64 FocusIntervalMS = 5000;

        public ApplicationSettings()
        {

        }

        public override int GetHashCode()
        {
            return StartDelayMS.GetHashCode()
                    * FocusIntervalMS.GetHashCode() ^ 2;
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
                    && FocusIntervalMS.Equals(options.FocusIntervalMS);
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
            RegistryKey sub_key = reg_key.CreateSubKey("Manobit");
            sub_key = sub_key.CreateSubKey("KodiLuncher");
            sub_key.SetValue("Options", stream.ToArray());
        }

        static internal Options read()
        {
            RegistryKey reg_key = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey sub_key = reg_key.CreateSubKey("Manobit");
            sub_key = reg_key.CreateSubKey("KodiLuncher");

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