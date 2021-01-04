using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using AWS.Data.AppDynamo.Data;
using AWS.Data.AppDynamo.Model;

namespace AWS.Data.AppDynamo.Repository
{
    public class OrderRepository: IOrderRepository
    {
        private readonly IContext _order;

        public OrderRepository(IContext context)
        {
            _order = context;
            
        }
        public async Task AddAsync(Order order)
        {
           await _order.Context.SaveAsync(order);
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var search =  _order.Context.ScanAsync<Order>
                (
                    new[]
                    {
                        new ScanCondition
                        (
                            nameof(Order.Id),
                            ScanOperator.IsNotNull
                        )
                    }
                );
            var result = await search.GetRemainingAsync();
            return result;
        }

        public async Task<Order> GetById(Guid id)
        {
            return await _order.Context.LoadAsync<Order>(id);
        }
        public void Dispose()
        {
            _order?.Dispose();
        }
    }
}