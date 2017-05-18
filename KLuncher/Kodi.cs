using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KLuncher
{
    class Kodi
    {
        static public void Run()
        {
            System.Diagnostics.Process processCodeBeautifier = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfoCodeBeautifier = new System.Diagnostics.ProcessStartInfo();

            startInfoCodeBeautifier.FileName = "c:/Program Files/Kodi/kodi.exe";
            processCodeBeautifier.StartInfo = startInfoCodeBeautifier;

            try
            {
                processCodeBeautifier.Start();
            }
            catch (System.ComponentModel.Win32Exception /*e*/)
            {
            }
            catch (Exception /*e*/)
            {
            }
        }

        static public void Terminate()
        {
            var kodiProcess = System.Diagnostics.Process.GetProcesses().
                                 Where(pr => pr.ProcessName == "kodi");

            foreach (var process in kodiProcess)
            {
                if( !process.HasExited)
                    process.Kill();
                Console.WriteLine("Killed");
            }
        }
    }
}
