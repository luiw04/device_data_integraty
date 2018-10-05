using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace Faberton.Energy.Worker.Helpers
{
    public static class AzTableStorage
    {
        private static CloudStorageAccount GetStorageAccount() =>
            CloudStorageAccount.Parse(AzureFunctions.GetSetting("AzureWebJobsStorage"));

        public static CloudTableClient GetTableClient() =>
            GetStorageAccount().CreateCloudTableClient();

        public static async Task<CloudTable> GetTableAsync(this CloudTableClient tableClient, string tableName)
        {
            var table = tableClient.GetTableReference(tableName);
            await table.CreateIfNotExistsAsync();

            return table;
        }
    }
}
