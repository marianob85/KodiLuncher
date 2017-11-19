using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace KodiLuncher
{
    class WatchDog
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();
        System.Timers.Timer m_watchDog;

        public WatchDog( bool createTime = true)
        {
            if (createTime)
            {
                m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => CreateTimers());
                CreateTimers();
            }
        }

        private void CreateTimers()
        {
            m_watchDog = null;
            //Ping(@"http:\\192.168.1.101:8083");

            if (m_options.options.applicationSettings.AppWatchDog.Enable)
            {
                m_watchDog = new System.Timers.Timer(m_options.options.applicationSettings.AppWatchDog.CheckInterval);
                m_watchDog.Elapsed += onWatchDog;
                m_watchDog.Start();
            }
        }

        private void onWatchDog(Object source, System.Timers.ElapsedEventArgs e)
        {
            var kodi = Kodi.Instance;
            if (!Ping())
            {
                if (kodi.isKodiProcess())
                {
                    kodi.Terminate();
                    System.Threading.Thread.Sleep(100);
                    kodi.Run();
                }
            }

        }

        public bool Ping()
        {
            var url = "http://localhost:" + m_options.options.applicationSettings.AppWatchDog.Port;

            bool result = false;
            string Message = string.Empty;
            try
            {
                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                webRequest.Timeout = 1200;
                webRequest.Method = "GET";
                //webRequest.AllowAutoRedirect = true;

                HttpWebResponse response = null;

                try
                {
                    response = (HttpWebResponse)webRequest.GetResponse();
                    result = checkRsponseCode(response);
                }
                catch (WebException webException)
                {
                    result = checkRsponseCode(webException.Response);
                }
                finally
                {
                    if (response != null)
                    {
                        response.Close();
                    }
                }
            }
            catch (Exception /*ex*/)
            {
                return false;
            }

            return result;
        }

        private bool checkRsponseCode(WebResponse response)
        {
            return response != null;
        }

        
    }
}
