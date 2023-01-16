using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;
using Microsoft.AspNetCore.Http;

namespace AzureIOT.Repositories
{
    public class FileStorageRepository
    {
        private static string connStringStorage = "";
        private static ShareServiceClient serviceClient;

        public static async Task CreateFileShare(string fileShareName)
        {
            if (string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("File Name Missing!");
            }
            try
            {
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                await service.CreateIfNotExistsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task CreateDirectory(string dirName, string fileShareName)
        {
            if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                ShareDirectoryClient rootDir = service.GetRootDirectoryClient();
                ShareDirectoryClient fileDir = rootDir.GetSubdirectoryClient(dirName);
                await fileDir.CreateIfNotExistsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task UploadFile(IFormFile file, string dirName, string fileShareName)
        {
            if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                string currentFile = file.FileName;
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                var dir = service.GetDirectoryClient(dirName);

                var fileStorage = dir.GetFileClient(currentFile);
                await using (var data = file.OpenReadStream())
                {
                    await fileStorage.CreateAsync(data.Length);
                    await fileStorage.UploadAsync(data);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DeleteDirectory(string dirName, string fileShareName)
        {
            if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                var dir = service.GetDirectoryClient(dirName);
                await dir.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DeleteFile(string dirName, string fileShareName, string fileName)
        {
            if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                var dir = service.GetDirectoryClient(dirName);
                var file = dir.GetFileClient(fileName);
                await file.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<string>> GetAllFiles(string dirName, string fileShareName)
        {
            if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                var files = service.GetRootDirectoryClient();
                var dir = service.GetDirectoryClient(dirName);
                List<string> names = new List<string>();
                await foreach (ShareFileItem file in dir.GetFilesAndDirectoriesAsync())
                {
                    names.Add(file.Name);
                }
                return names;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DownloadFile(string dirName, string fileShareName, string fileName)
        {
            if (string.IsNullOrEmpty(dirName) || string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                string path = @"D:\Code C#\AzureIOT\AzureIOT\Downloads\Files\" + fileShareName + "-" + fileName;
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                var dir = service.GetDirectoryClient(dirName);
                var file = dir.GetFileClient(fileName);
                ShareFileDownloadInfo downloadInfo = await file.DownloadAsync();
                using (FileStream stream = File.OpenWrite(path))
                {
                    await downloadInfo.Content.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DeleteFileShare(string fileShareName)
        {
            if (string.IsNullOrEmpty(fileShareName))
            {
                throw new ArgumentNullException("File Name Missing!");
            }
            try
            {
                serviceClient = new ShareServiceClient(connStringStorage);
                var service = serviceClient.GetShareClient(fileShareName);
                await service.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
