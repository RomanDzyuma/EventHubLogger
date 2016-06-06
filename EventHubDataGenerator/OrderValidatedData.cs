using System;

namespace EventHubDataGenerator
{
    public class OrderValidatedData : OrderDataBase
    {
        public OrderValidatedData(int id) : base(id, DataType.Validated)
        {
        }

        public DateTime? ValidatedTime { get; set; }
    }
}
