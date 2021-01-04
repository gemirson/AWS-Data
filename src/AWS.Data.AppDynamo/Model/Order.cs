using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.SignalR;

namespace AWS.Data.AppDynamo.Model
{
    public class Order
    {
        [DynamoDBHashKey]
        public Guid Id  { get; private set; }
        [DynamoDBRangeKey]
        public decimal Total { get; private set; }
        public ICollection<Item> Itens { get; private set; }

        public Order(Guid id, decimal total, ICollection<Item> itens)
        {
            Id = id;
            Total = total;
            Itens = itens;
        }
    }
   
}