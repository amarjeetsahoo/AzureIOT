using Azure.Data.Tables;
using AzureIOT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureIOT.Repositories
{
    public class TableStorageRepository
    {
        private static string connStringStorage = "";

        public static async Task AddTable(string tableName)
        {
            var data = new TableServiceClient(connStringStorage);
            var client = data.GetTableClient(tableName);
            await client.CreateIfNotExistsAsync();
        }

        public static async Task<TableStorageModel> Updatetable(TableStorageModel employee, string tableName)
        {
            var data = new TableServiceClient(connStringStorage);
            var client = data.GetTableClient(tableName);
            await client.UpsertEntityAsync(employee);
            return employee;
        }
     
        public static async Task<TableStorageModel> GetTableData(string tableName, string partitionKey, string id)
        {
            var data = new TableServiceClient(connStringStorage);
            var client = data.GetTableClient(tableName);
            var tableData = await client.GetEntityAsync<TableStorageModel>(partitionKey, id);
            return tableData;
        }

        public static TableClient GetTable(string tableName)
        {
            var data = new TableServiceClient(connStringStorage);
            var client = data.GetTableClient(tableName);
            return client;
        }

        public static async Task<IEnumerable<TableStorageModel>> GetAllTableData(string tableName)
        {
            var _tableClient = GetTable(tableName);
            IList<TableStorageModel> modelList = new List<TableStorageModel>();
            var data = _tableClient.QueryAsync<TableStorageModel>(filter: "", maxPerPage: 10);
            await foreach (var rec in data)
            {
                modelList.Add(rec);
            }
            return modelList;
        }

            public static async Task DeleteTableData(string tableName, string partitionKey, string id)
        {
            var data = new TableServiceClient(connStringStorage);
            var client = data.GetTableClient(tableName);
            await client.DeleteEntityAsync(partitionKey, id);
            return;
        }

        public static async Task DeleteTable(string tableName)
        {
            var data = new TableServiceClient(connStringStorage);
            var client = data.GetTableClient(tableName);
            await client.DeleteAsync();
            return;
        }
    }
}
