using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AWS.Data.AppDynamo.Config;
using AWS.Data.AppDynamo.Data;
using AWS.Data.AppDynamo.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AWS.Data.AppDynamo.Setup
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<ConfigDynamo>(config.GetSection("Dynamo"));
            services.AddScoped<DynamoDBFactory>();
            services.AddScoped<IContext, OrderContext>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            return services;
        }
    }
}
