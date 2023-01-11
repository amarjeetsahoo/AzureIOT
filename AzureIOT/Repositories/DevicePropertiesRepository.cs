using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using AzureIOT.Models;
using Microsoft.Azure.Devices.Shared;

namespace AzureIOT.Repositories
{
    public class DevicePropertiesRepository
    {
        private static string connStringIotHub = "HostName=iothub-ahs230107.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=12aNxV/0+wXiN8efYUzcY2JyyGybnDISK0WGMY5zabQ=";
        public static RegistryManager registryManager = RegistryManager.CreateFromConnectionString(connStringIotHub);

        public static DeviceClient client;
        private static string connStringDevice = "HostName=iothub-ahs230107.azure-devices.net;DeviceId=sensor-th-0001;SharedAccessKey=MT0CRXhxV9fzO3JWV4Yvye9bmwQf1kXBrSgWuisVDC8=";

        public static async Task UpdateReportedPropertiesAsync(string deviceId, DevicePropertiesModel properties)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            else
            {
                client = DeviceClient.CreateFromConnectionString(connStringDevice, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection reportedProperties, connectivity;
                reportedProperties = new TwinCollection();
                connectivity = new TwinCollection();
                connectivity["type"] = "cellular";
                reportedProperties["connectivity"] = connectivity;
                reportedProperties["temperature"] = properties.temperature;
                reportedProperties["humidity"] = properties.humidity;

                await client.UpdateReportedPropertiesAsync(reportedProperties);
                return;
            }
        }

        public static async Task UpdateDesiredPropertiesAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            else
            {
                client = DeviceClient.CreateFromConnectionString(connStringDevice, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection desiredProperties, telemetryConfig;
                desiredProperties = new TwinCollection();
                telemetryConfig = new TwinCollection();
                var device = await registryManager.GetTwinAsync(deviceId);
                telemetryConfig["temperature"] = 39.56756856;
                desiredProperties["telemetryConfig"] = telemetryConfig;
                device.Properties.Desired["telemetryConfig"] = telemetryConfig;

                await registryManager.UpdateTwinAsync(device.DeviceId, device, device.ETag);
                return;
            }
        }

        public static async Task<Twin> GetPropertiesAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            var device = await registryManager.GetTwinAsync(deviceId);
            return device;
        }

        public static async Task UpdateTagPropertiesAsync(string deviceId)
        {
            if (string.IsNullOrEmpty(deviceId))
            {
                throw new ArgumentNullException("noDeviceId");
            }
            else
            {
                var twin = await registryManager.GetTwinAsync(deviceId);
                var patch =
                @"{
                    tags:{
                location:
                    {
                        region: 'India East'
                    }
                }
                }";

                client = DeviceClient.CreateFromConnectionString(connStringDevice, Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                TwinCollection connectivity;
                connectivity = new TwinCollection();
                connectivity["type"] = "cellular";
                await registryManager.UpdateTwinAsync(twin.DeviceId, patch, twin.ETag);
                return;
            }
        }
    }
}
