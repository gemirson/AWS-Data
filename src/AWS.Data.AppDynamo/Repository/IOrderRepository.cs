using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AWS.Data.AppDynamo.Model;

namespace AWS.Data.AppDynamo.Repository
{
    public interface IOrderRepository:IDisposable
    {
        Task AddAsync(Order order);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order> GetById(Guid Id);
    }
}