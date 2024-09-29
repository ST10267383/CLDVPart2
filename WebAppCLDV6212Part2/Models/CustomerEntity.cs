using Azure;
using Azure.Data.Tables;
using System;
namespace WebAppCLDV6212Part2.Models
{
    public class CustomerEntity : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }
    }
}
