using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using AWS.Data.AppDynamo.Config;

namespace AWS.Data.AppDynamo.Data
{
    public interface IContext : IDisposable
    {
        DynamoDBContext Context { get; }
        Task<bool> CreateTableAsync(string tableName, ConfigTable configTable);
        Task<bool> CheckingTableExistenceAsync(string tableName);
        Task<TableDescription> GetTableDescription(string tableName);
    }
}