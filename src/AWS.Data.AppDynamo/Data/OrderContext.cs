using System;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using AWS.Data.AppDynamo.Config;
using AWS.Data.AppDynamo.Model;
using Microsoft.Extensions.Logging;

namespace AWS.Data.AppDynamo.Data
{
    public class OrderContext:IContext
    {
        private readonly AmazonDynamoDBClient _client;
        private readonly ILogger<OrderContext> _logger; 

        public OrderContext(DynamoDBFactory dynamoDbFactory, ILogger<OrderContext> logger)
        {
            Context = dynamoDbFactory.Context;
            _client = dynamoDbFactory.DynamoDbClient;
            _logger = logger;
            _logger.LogDebug(default(EventId), $"NLog injected into {nameof(OrderContext)}");
        }

        public async  Task<bool> CheckingTableExistenceAsync(string tableName)
        {
            var response = await _client.ListTablesAsync();
            return response.TableNames.Contains(tableName);
        }

        public async Task<TableDescription> GetTableDescription(string tableName)
        {
            TableDescription result = null;

            // If the table exists, get its description.
            try
            {
                var response = await _client.DescribeTableAsync(tableName);
                result = response.Table;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId),
                    $"Found fails to {nameof(CreateTableAsync)} in GetTableDescription{ex.Message}");
                result = null;
                throw;
            }

            return result;
        }

        public DynamoDBContext Context { get; }

        public async Task<bool> CreateTableAsync(string tableName, ConfigTable configTable)
        {
            var response = true;
            try
            {
                var isTable = await CheckingTableExistenceAsync(tableName);
                if (isTable) throw new Exception("Found fails created table: Table exists");

                // Build the 'CreateTableRequest' structure for the new table
                var request = new CreateTableRequest
                {
                    TableName = tableName,
                    AttributeDefinitions = configTable.tableAttributes,
                    KeySchema = configTable.tableKeySchema,
                    // Provisioned-throughput settings are always required,
                    // although the local test version of DynamoDB ignores them.
                    ProvisionedThroughput = configTable.provisionedThroughput
                };
                await _client.CreateTableAsync(request);
            }
            catch (AmazonDynamoDBException ex)
            {
                _logger.LogError(default(EventId),
                    $"Found fails to {nameof(CreateTableAsync)} in AddAsync {ex.Message}");
                response = false;
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(default(EventId),
                    $"Found fails to {nameof(CreateTableAsync)} in AddAsync {ex.Message}");
                response = false;
                throw;
            }
           
            return response;

        }
        public void Dispose()
        {
            Context?.Dispose();
            _client?.Dispose();
        }
    }
}