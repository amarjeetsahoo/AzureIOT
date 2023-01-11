using Azure;
using Azure.Data.Tables;
using System;

namespace AzureIOT.Models
{
    public class TableStorageModel: ITableEntity
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
