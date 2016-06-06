using System;

namespace EventHubDataGenerator
{
    public class OrderCreatedData : OrderDataBase
    {
        public OrderCreatedData(int id) : base(id, DataType.Created)
        {
        }
        public DateTime? CreatedTime { get; set; }
    }
}
