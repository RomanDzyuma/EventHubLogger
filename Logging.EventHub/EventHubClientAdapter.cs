using Microsoft.ServiceBus.Messaging;

namespace Logging.EventHub
{
    public class EventHubClientAdapter : IEventHubClient
    {
        private readonly EventHubClient _client;

        public EventHubClientAdapter(EventHubClient client)
        {
            _client = client;
        }

        public void SendAsync(EventData eventData)
        {
            _client.SendAsync(eventData);
        }
    }
}
