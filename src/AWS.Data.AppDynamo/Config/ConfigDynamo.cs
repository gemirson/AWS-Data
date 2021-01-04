using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWS.Data.AppDynamo.Config
{
    public class ConfigDynamo
    {
        public string  Url { get; set; }
        public string  AcessKeyId  { get; set; }
        public string  SecretAccessKey { get; set; }
    }
}
