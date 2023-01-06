using AzureIOT.Models;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureIOT.Repositories
{
    public class SendTelemetryRepository
    {
        public static RegistryManager registryManager;
        public static DeviceClient client;
        private static string connStringIotHub = "";
        public static string connStringDevice = "";

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

                while (true)
                {
                    var telemetry = new
                    {
                        temperature = Prop["temperature"],
                        humidity = Prop["humidity"]
                    };
                    var telemetryString = JsonConvert.SerializeObject(telemetry);
                    var message = new Microsoft.Azure.Devices.Client.Message(
                        Encoding.ASCII.GetBytes(telemetryString));
                    await client.SendEventAsync(message);
                    Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, telemetryString);
                    await Task.Delay(1000);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
