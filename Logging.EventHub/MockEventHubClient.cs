using System;
using System.Text;
using Microsoft.ServiceBus.Messaging;

namespace Logging.EventHub
{
    public class MockEventHubClient : IEventHubClient
    {
        public void SendAsync(EventData eventData)
        {
            Console.WriteLine(Encoding.Default.GetString(eventData.GetBytes()));
        }
    }
}
