using Microsoft.ServiceBus.Messaging;

namespace Logging.EventHub
{
    public interface IEventHubClient
    {
        void SendAsync(EventData eventData);
    }
}
