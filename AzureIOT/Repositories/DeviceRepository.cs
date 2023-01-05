using System;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace AzureIOT.Repositories
{
    public class DeviceRepository
    {
        public static RegistryManager registryManager;
        private static string connStringIotHub = "";
        public static async Task AddDeviceAsync(string deviceName)
        {
            if (string.IsNullOrEmpty(deviceName))
            {
                throw new ArgumentNullException("noDeviceName");
            }

            Device device;
            registryManager = RegistryManager.CreateFromConnectionString(connStringIotHub);
            device = await registryManager.AddDeviceAsync(new Device(deviceName));
            return;
        }
    }
}
