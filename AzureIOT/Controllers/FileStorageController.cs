using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureIOT.Repositories;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileStorageController : ControllerBase
    {
        [HttpPost("CreateFileShare")]
        public async Task CreateFileShare(string fileShareName)
        {
            await FileStorageRepository.CreateFileShare(fileShareName);
            return;
        }

        [HttpPost("CreateDirectory")]
        public async Task CreateDirectory(string dirName, string fileShareName)
        {
            await FileStorageRepository.CreateDirectory(dirName, fileShareName);
            return;
        }

        [HttpPut("UploadFile")]
        public async Task UploadFile(IFormFile file, string dirName, string fileShareName)
        {
            await FileStorageRepository.UploadFile(file, dirName, fileShareName);
            return;
        }

        [HttpDelete("DeleteDirectory")]
        public async Task DeleteDirectory(string dirName, string fileShareName)
        {
            await FileStorageRepository.DeleteDirectory(dirName, fileShareName);
            return;
        }

        [HttpDelete("DeleteFile")]
        public async Task DeleteFile(string dirName, string fileShareName, string fileName)
        {
            await FileStorageRepository.DeleteFile(dirName, fileShareName, fileName);
            return;
        }

        [HttpGet("GetAllFiles")]
        public async Task<List<string>> GetAllFiles(string dirName, string fileShareName)
        {
            var data = await FileStorageRepository.GetAllFiles(dirName, fileShareName);
            return data;
        }

        [HttpGet("DownloadFile")]
        public async Task DownloadFile(string dirName, string fileShareName, string fileName)
        {
            await FileStorageRepository.DownloadFile(dirName, fileShareName, fileName);
            return;
        }

        [HttpDelete("DeleteFileShare")]
        public async Task DeleteFileShare(string fileShareName)
        {
            await FileStorageRepository.DeleteFileShare(fileShareName);
            return;
        }
    }
}
