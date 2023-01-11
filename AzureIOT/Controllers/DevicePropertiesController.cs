using AzureIOT.Models;
using AzureIOT.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices.Shared;
using System.Threading.Tasks;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicePropertiesController : ControllerBase
    {
        [HttpPut("UpdateReportedProperty")]
        public async Task UpdateDeviceReportedProperty(string deviceId, DevicePropertiesModel properties )
        {
            await DevicePropertiesRepository.UpdateReportedPropertiesAsync(deviceId, properties);
            return;
        }

        [HttpPut("UpdateDesiredProperty")]
        public async Task UpdateDeviceDesiredProperty(string deviceId)
        {
            await DevicePropertiesRepository.UpdateDesiredPropertiesAsync(deviceId);
            return;
        }

        [HttpPut("UpdateTagProperty")]
        public async Task UpdateDeviceTagProperty(string deviceId)
        {
            await DevicePropertiesRepository.UpdateTagPropertiesAsync(deviceId);
            return;
        }

        [HttpGet("GetProperties")]
        public async Task<Twin> GetDeviceProperties(string deviceId)
        {
            var device= await DevicePropertiesRepository.GetPropertiesAsync(deviceId);
            return device;
        }
    }
}
