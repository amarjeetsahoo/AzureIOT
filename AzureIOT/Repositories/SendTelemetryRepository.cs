using AzureIOT.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace AzureIOT.Repositories
{
    public class SendTelemetryRepository
    {
        public static RegistryManager registryManager;
        public static DeviceClient client;
        private static string connStringIotHub = "HostName=iothub-ahs230107.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=12aNxV/0+wXiN8efYUzcY2JyyGybnDISK0WGMY5zabQ=";
        public static string connStringDevice = "HostName=iothub-ahs230107.azure-devices.net;DeviceId=sensor-th-0001;SharedAccessKey=MT0CRXhxV9fzO3JWV4Yvye9bmwQf1kXBrSgWuisVDC8=";

        public static async Task SendMessage(string deviceId)
        {
            try
            {
                registryManager = RegistryManager.CreateFromConnectionString(connStringIotHub);
                var device = await registryManager.GetTwinAsync(deviceId);
                Properties properties = new Properties();
                TwinCollection Prop;
                Prop = device.Properties.Reported;
                client = DeviceClient.CreateFromConnectionString(connStringDevice,
                    Microsoft.Azure.Devices.Client.TransportType.Mqtt);

                Random rand = new Random();

                while (true)
                {
                    var telemetry = new
                    {
                        temperature = Prop["temperature"] + rand.NextDouble() * 15,
                        humidity = Prop["humidity"] + rand.NextDouble() * 20
                };
                    var telemetryString = JsonConvert.SerializeObject(telemetry);
                    var message = new Microsoft.Azure.Devices.Client.Message(
                        Encoding.ASCII.GetBytes(telemetryString));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, telemetryString);
                    await Task.Delay(5000);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
