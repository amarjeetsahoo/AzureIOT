using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AzureIOT.Repositories;
using Azure.Storage.Blobs.Models;
using System.Collections.Generic;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlobStorageController : ControllerBase
    {
        [HttpPost("AddContainer")]
        public async Task AddContainer(string containerName)
        {
            await BlobStorageRepository.CreateContainer(containerName);
            return;
        }

        [HttpDelete("DeleteContainer")]
        public async Task DeleteContainer(string containerName)
        {
            await BlobStorageRepository.DeleteContainer(containerName);
            return;
        }

        [HttpDelete("DeleteBlobData")]
        public async Task DeleteBlobData(string containerName, string blobName)
        {
            await BlobStorageRepository.DeleteBlobData(containerName, blobName);
            return;
        }

        [HttpPut("AddBlobData")]
        public async Task AddBlobData(string containerName, string blobName)
        {
            await BlobStorageRepository.AddBlobData(containerName, blobName);
            return;
        }

        [HttpGet("GetBlobData")]
        public async Task<BlobProperties> GetBlobData(string containerName, string blobName)
        {
            var data = await BlobStorageRepository.GetBlobData(containerName, blobName);
            return data;
        }

        [HttpGet("GetBlobs")]
        public async Task<List<string>> GetBlobs(string containerName)
        {
            var data = await BlobStorageRepository.GetBlobs(containerName);
            return data;
        }

        [HttpGet("DownloadBlobData")]
        public async Task<BlobProperties> DownloadBlobData(string containerName, string blobName)
        {
            var data = await BlobStorageRepository.DownloadBlobData(containerName, blobName);
            return data;
        }
    }
}
