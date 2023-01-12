using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AzureIOT.Repositories;
using Azure.Storage.Queues.Models;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QueueStorageController : ControllerBase
    {
        [HttpPost("AddQueue")]
        public async Task<string> AddQueue(string queueName)
        {
            await QueueStorageRepository.CreateQueue(queueName);
            return null;
        }

        [HttpPost("InsertMessage")]
        public async Task<string> InsertMessage(string queueName, string msg)
        {
            await QueueStorageRepository.InsertMessage(queueName, msg);
            return null;
        }

        [HttpGet("PeekMessage")]
        public PeekedMessage[] PeekMessage(string queueName, int peekValue)
        {
            var data = QueueStorageRepository.PeekMessage(queueName, peekValue);
            return data;
        }

        [HttpPut("UpdateMessage")]
        public void UpdateMessage(string queueName, string updatedMsg)
        {
            QueueStorageRepository.UpdateMessage(queueName, updatedMsg);
            return;
        }

        [HttpPut("DequeueMessage")]
        public void DequeueMessage(string queueName)
        {
            QueueStorageRepository.DequeueMessage(queueName);
            return;
        }

        [HttpDelete("DeleteQueue")]
        public async Task<string> DeleteQueue(string queueName)
        {
            await QueueStorageRepository.DeleteQueue(queueName);
            return null;
        }
    }
}
