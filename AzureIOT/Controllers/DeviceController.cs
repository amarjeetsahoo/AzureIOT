using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AzureIOT.Repositories;
using Microsoft.Azure.Devices;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        [HttpPost("AddDevice")]
        public async Task AddDevice(string deviceId)
        {
            await DeviceRepository.AddDeviceAsync(deviceId);
            return;
        }

        [HttpGet("GetDevice")]
        public async Task<Device> GetDevice(string deviceId)
        {
            Device device;
            device=await DeviceRepository.GetDeviceAsync(deviceId);
            return device;
        }

        [HttpDelete("DeleteDevice")]
        public async Task DeleteDevice(string deviceId)
        {
            await DeviceRepository.DeleteDeviceAsync(deviceId);
            return;
        }

        [HttpPut("UpdateDevice")]
        public async Task<Device> UpdateDevice(string deviceId)
        {
            Device device;
            device= await DeviceRepository.UpdateDeviceAsync(deviceId);
            return device;
        }
    }
}
