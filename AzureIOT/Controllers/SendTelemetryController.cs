using AzureIOT.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendTelemetryController : ControllerBase
    {
        [HttpPost("SendTelemetryMessage")]
        public async Task SendMessage(string deviceId)
        {
            await SendTelemetryRepository.SendMessage(deviceId);
            return;
        }
    }
}
