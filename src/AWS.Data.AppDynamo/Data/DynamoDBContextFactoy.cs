using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using AWS.Data.AppDynamo.Config;
using Microsoft.Extensions.Options;

namespace AWS.Data.AppDynamo.Data
{
    public class DynamoDBFactory
    {
        public AmazonDynamoDBClient DynamoDbClient { get; }
        public DynamoDBContext Context { get; }

        public DynamoDBFactory(IOptions<ConfigDynamo> configDynamo)
        {
            DynamoDbClient = new AmazonDynamoDBClient(configDynamo.Value.AcessKeyId,configDynamo.Value.SecretAccessKey,
                new AmazonDynamoDBConfig
                {
                    ServiceURL = configDynamo.Value.Url, //default localstack url
                    UseHttp = true,
                });
            Context = new DynamoDBContext(DynamoDbClient, new DynamoDBContextConfig
            {
                TableNamePrefix = "test_"
            });
        }




    }

}