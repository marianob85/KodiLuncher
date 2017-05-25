using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace KodiLuncher.Sources
{
    class AutoStart
    {
        public bool Enable
        {
            get {
                RegistryKey reg_key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", false);
                return reg_key.GetValue("KodiLuncher", null) != null;
            }
            set
            {
                if (value)
                {
                    var options = new ProgramSettings.SettingsContainer();

                    if (!System.IO.File.Exists(options.options.applicationSettings.Application))
                        return;

                    RegistryKey reg_key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    reg_key.SetValue("KodiLuncher", options.options.applicationSettings.Application + " -b");
                }
                else
                {
                    RegistryKey reg_key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run", true);
                    reg_key.DeleteValue("KodiLuncher");
                }
            }
        }
    }
}
