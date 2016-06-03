using Microsoft.ServiceBus.Messaging;

namespace Logging.EventHub
{
    public class EventHubClientAdapter : IEventHubClient
    {
        private EventHubClient client;

        public EventHubClientAdapter(EventHubClient client)
        {
            this.client = client;
        }

        public void SendAsync(EventData eventData)
        {
            client.SendAsync(eventData);
        }
    }
}
