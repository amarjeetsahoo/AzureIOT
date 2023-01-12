using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureIOT.Repositories;
using Azure.Data.Tables;
using AzureIOT.Models;

namespace AzureIOT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableStorageController : ControllerBase
    {
        [HttpPost("AddTable")]
        public async Task AddTable(string tableName)
        {
            await TableStorageRepository.AddTable(tableName);
            return;
        }

        [HttpPut("UpdateTable")]
        public async Task<TableStorageModel> Updatetable(TableStorageModel employee, string tableName)
        {
            employee.PartitionKey = tableName;
            string Id = Guid.NewGuid().ToString();
            employee.Id = Id;
            employee.RowKey = Id;
            employee.Timestamp = DateTime.Now;
            var data = await TableStorageRepository.Updatetable(employee, tableName);
            return data;
        }

        [HttpGet("GetTableData")]
        public async Task<TableStorageModel> GetTableData(string tableName, string partitionKey, string id)
        {
            var data = await TableStorageRepository.GetTableData(tableName, partitionKey, id);
            return data;
        }

        [HttpGet("GetTable")]
        public TableClient GetTable(string tableName)
        {
            var data = TableStorageRepository.GetTable(tableName);
            return data;
        }

        [HttpGet("GetAllTableData")]
        public async Task<IEnumerable<TableStorageModel>> GetAllTableData(string tableName)
        {
            var data = await TableStorageRepository.GetAllTableData(tableName);
            return data;
        }

        [HttpDelete("DeleteTableData")]
        public async Task DeleteTableData(string tableName, string partitionKey, string id)
        {
            await TableStorageRepository.DeleteTableData(tableName, partitionKey, id);
            return;
        }

        [HttpDelete("DeleteTable")]
        public async Task DeleteTable(string tableName  )
        {
            await TableStorageRepository.DeleteTable(tableName);
            return;
        }
    }
}
