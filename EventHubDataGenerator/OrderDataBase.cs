namespace EventHubDataGenerator
{
    public class OrderDataBase
    {
        public OrderDataBase(int id, DataType type)
        {
            OrderId = id;
            Type = type;
        }

        public int OrderId { get; private set; }

        public DataType Type { get; private set; }
    }
}