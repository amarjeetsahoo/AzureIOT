using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureIOT.Repositories;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        [HttpPost("AddDevice")]
        public async Task<string> AddDevice(string deviceName)
        {
            await DeviceRepository.AddDeviceAsync(deviceName);
            return null;
        }
    }
}
