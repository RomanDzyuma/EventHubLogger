using System;

namespace EventHubDataGenerator
{
    public class OrderSentData : OrderDataBase
    {
        public OrderSentData(int id) : base(id, DataType.Sent)
        {
        }
        public DateTime? SentTime { get; set; }
    }
}
