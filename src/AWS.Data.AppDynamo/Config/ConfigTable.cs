using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace AWS.Data.AppDynamo.Config
{
    public class ConfigTable
    {
        public List<AttributeDefinition> tableAttributes { get; set; }
        public List<KeySchemaElement> tableKeySchema { get; set; }
        public ProvisionedThroughput provisionedThroughput { get; set; }
    }
}