﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Disconnecting;
using KodiLuncher;
using MQTTnet.Client.Receiving;

namespace KodiLuncher.Sources
{
    public class MqttClient
    {
        private ProgramSettings.SettingsContainer m_options = new ProgramSettings.SettingsContainer();
        private Task m_server = null;
        private IMqttClient m_mqttClient = null;
        private CancellationTokenSource m_tokenSource2 = null;
        private Kodi m_kodiLuncher = Kodi.Instance;
        private bool m_lastStatus = false;
        public MqttClient()
        {
            m_options.OptionsChanged += new EventHandler((Object sender, EventArgs e) => OptionsChanged());
            if (m_options.options.applicationSettings.Mqtt.Enable)
                ConnectClient();
        }

        public void Dispose()
        {
            DisconnectClient();

        }

        private void OptionsChanged()
        {
            if ((m_mqttClient != null) != m_options.options.applicationSettings.Mqtt.Enable)
            {
                if (m_options.options.applicationSettings.Mqtt.Enable)
                    ConnectClient();
                else
                    DisconnectClient();
            }
        }

        public bool Status()
        {
            return m_mqttClient != null && m_mqttClient.IsConnected;
        }

        public void DisconnectClient()
        {
            if (m_mqttClient != null)
            {
                CancellationToken ct = m_tokenSource2.Token;
                m_tokenSource2.Cancel();
                m_server.Wait();
                m_tokenSource2.Dispose();
            }
        }

        private async Task onMessage(MqttApplicationMessageReceivedEventArgs e)
        {
            try
            {
                var message = System.Text.Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
                if (!m_kodiLuncher.isKodiProcess() && message == "on")
                {
                    m_kodiLuncher.Run();
                }
                else if (message == "off")
                {
                    m_kodiLuncher.Close();
                }

                await publishStatus(true);
            }
            catch
            { }
        }

        private void subscribeMessage()
        {
            m_mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(e => onMessage(e)); ;

            var mqttFactory = new MqttFactory();
            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic("HTPC/kodi/command"); })
                .Build();

            m_mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None).Wait();

        }

        private async Task publishStatus(bool force = false)
        {
            if (force || m_lastStatus != m_kodiLuncher.isKodiProcess())
            {
                m_lastStatus = m_kodiLuncher.isKodiProcess();
                var applicationMessage = new MqttApplicationMessageBuilder()
                                                    .WithTopic("HTPC/kodi/running")
                                                    .WithPayload(m_lastStatus ? "on" : "off")
                                                    .WithRetainFlag()
                                                    .Build();

                await m_mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            }
        }

        public void ConnectClient()
        {
            m_tokenSource2 = new CancellationTokenSource();
            var mqttFactory = new MqttFactory();

            //using (var mqttClient = mqttFactory.CreateMqttClient())
            m_mqttClient = mqttFactory.CreateMqttClient();

            var mqttClientOptions = new MqttClientOptionsBuilder().WithProtocolVersion(MQTTnet.Formatter.MqttProtocolVersion.V500)
                .WithTcpServer(m_options.options.applicationSettings.Mqtt.Host, m_options.options.applicationSettings.Mqtt.Port)
                .WithCredentials(m_options.options.applicationSettings.Mqtt.UserName, m_options.options.applicationSettings.Mqtt.Password)
                .WithClientId("HTPC").Build();

            m_server = Task.Run(
                async () =>
                    {
                        CancellationToken ct = m_tokenSource2.Token;

                        // User proper cancellation and no while(true).
                        while (!ct.IsCancellationRequested)
                        {
                            try
                            {
                                // This code will also do the very first connect! So no call to _ConnectAsync_ is required
                                // in the first place.
                                if (!m_mqttClient.IsConnected)
                                {
                                    await m_mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                                    subscribeMessage();
                                    await publishStatus(true);
                                }
                                else
                                {
                                    await publishStatus();
                                }
                            }
                            catch
                            {
                                // Handle the exception properly (logging etc.).
                            }
                            finally
                            {
                                // Check the connection state every 5 seconds and perform a reconnect if required.
                                await Task.Delay(TimeSpan.FromSeconds(5));
                            }
                        }

                        m_mqttClient.DisconnectAsync(new MqttClientDisconnectOptions(), CancellationToken.None).Wait();
                        m_mqttClient = null;
                    });
        }
    }
}
