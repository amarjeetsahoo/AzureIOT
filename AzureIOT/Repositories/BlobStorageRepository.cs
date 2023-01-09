using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzureIOT.Repositories
{
    public class BlobStorageRepository
    {
        private static string connStringStorage = "";

        public static async Task CreateContainer(string containerName)
        {
            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException("Container Name Missing!");
            }
            try
            {
                BlobContainerClient containerClient =
                    new BlobContainerClient(connStringStorage, containerName);
                await containerClient.CreateAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DeleteContainer(string containerName)
        {
            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException("Container Name Missing!");
            }
            try
            {
                BlobContainerClient containerClient =
                    new BlobContainerClient(connStringStorage, containerName);
                await containerClient.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DeleteBlobData(string containerName, string blobName)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                BlobContainerClient containerClient =
                    new BlobContainerClient(connStringStorage, containerName);
                await containerClient.DeleteBlobAsync(blobName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BlobProperties> AddBlobData(string containerName, string blobName)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                string file = Path.GetTempFileName();
                BlobContainerClient containerClient =
                    new BlobContainerClient(connStringStorage, containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(file);
                BlobProperties properties = await blobClient.GetPropertiesAsync();
                return properties;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BlobProperties> GetBlobData(string containerName, string blobName)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                BlobContainerClient containerClient =
                    new BlobContainerClient(connStringStorage, containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                BlobProperties properties = await blobClient.GetPropertiesAsync();
                return properties;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<string>> GetBlobs(string containerName)
        {
            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException("Container Name Missing!");
            }
            try
            {
                BlobContainerClient containerClient =
                    new BlobContainerClient(connStringStorage, containerName);
                List<string> names = new List<string>();
                await foreach (BlobItem b in containerClient.GetBlobsAsync())
                {
                    names.Add(b.Name);
                }
                return names;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BlobProperties> DownloadBlobData(string containerName, string blobName)
        {
            if (string.IsNullOrEmpty(containerName) || string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Parameter Missing!");
            }
            try
            {
                string path = @"D:\Code C#\AzureIOT\AzureIOT\Downloads\Blobs\" + blobName + "-" + containerName;
                BlobContainerClient containerClient =
                    new BlobContainerClient(connStringStorage, containerName);
                BlobClient blobClient = containerClient.GetBlobClient(blobName);
                await blobClient.DownloadToAsync(path);
                BlobProperties properties = await blobClient.GetPropertiesAsync();
                return properties;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
