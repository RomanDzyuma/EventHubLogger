using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace Logging.EventHub
{
    public interface IEventHubClient
    {
        void SendAsync(EventData eventData);
    }
}
