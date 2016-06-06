using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;

namespace EventHubDataGenerator
{
    public class Program
    {
        private const int NumberOfOrders = 20;
        private static readonly DateTime StartTimeOfOrders = DateTime.Today;

        public static void Main(string[] args)
        {
            var dataToSend = GenerateData();

            var stopwatch = Stopwatch.StartNew();
           
            var client = CreateEventhub();
            ////SendData(client, dataToSend);
            ////PrintData(client, dataToSend);
            WriteToFile(dataToSend);

            stopwatch.Stop();
            Console.WriteLine($"Generated {NumberOfOrders} items. Time: {stopwatch.Elapsed}");
            Console.ReadLine();
        }

        private static void WriteToFile(IEnumerable<OrderDataBase> dataToSend)
        {
            var content = JsonConvert.SerializeObject(dataToSend);
            File.WriteAllText("EventHubData.json", content);
        }

        private static void PrintData(EventHubClient client, IEnumerable<OrderDataBase> dataToSend)
        {
            try
            {
                var tasks = new List<Task>();

                foreach (var orderData in dataToSend)
                {
                    var serializedString = JsonConvert.SerializeObject(orderData);
                    Console.WriteLine(serializedString);
                }

                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception)
            {
                Console.WriteLine("Error on data send");
            }
        }

        private static void SendData(EventHubClient client, IEnumerable<OrderDataBase> dataToSend)
        {
            try
            {
                var tasks = new List<Task>();

                foreach (var orderData in dataToSend)
                {
                    var serializedString = JsonConvert.SerializeObject(orderData);
                    tasks.Add(client.SendAsync(new EventData(Encoding.UTF8.GetBytes(serializedString))
                    {
                        PartitionKey = orderData.OrderId.ToString(CultureInfo.InvariantCulture)
                    }));
                }

                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception)
            {
                Console.WriteLine("Error on data send");
            }
        }

        private static EventHubClient CreateEventhub()
        {
            var eventHubConnectionString = ConfigurationManager.AppSettings["Microsoft.ServiceBus.ConnectionString"];
            return EventHubClient.CreateFromConnectionString(eventHubConnectionString);
        }

        private static readonly Func<int, DateTime, OrderDataBase>[] OrderDataFactory =
        {
            (id, dt) => new OrderValidatedData(id) { ValidatedTime = dt },
            (id, dt) => new OrderCreatedData(id) { CreatedTime = dt },
            (id, dt) => new OrderSentData(id) { SentTime = dt}
        };

        private static IEnumerable<OrderDataBase> GenerateData()
        {
            var data = new List<OrderDataBase>();

            var dt = StartTimeOfOrders;

            var rand = new Random();
            for (int id = 1; id <= NumberOfOrders; id++)
            {
                var rnd = rand.Next(0, OrderDataFactory.Length);
                for (int orderDataType = 0; orderDataType <= rnd; orderDataType++)
                {
                    data.Add(OrderDataFactory[orderDataType](id, dt.AddSeconds(0.5 * rnd)));
                }

                dt = dt.AddSeconds(rand.Next(0, 5));
            }

            return data;
        }
    }
}
