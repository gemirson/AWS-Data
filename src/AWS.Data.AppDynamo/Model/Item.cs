using System;

namespace AWS.Data.AppDynamo.Model
{
    public class Item
    {
        public Guid Id { get; private set; }
        public string Name { get;private set; }
        public decimal Price { get; private set; }

        public Item(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }


    }
}