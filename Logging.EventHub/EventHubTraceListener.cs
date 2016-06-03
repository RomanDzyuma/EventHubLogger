using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.ServiceBus.Messaging;

using Newtonsoft.Json;

using System;
using System.Configuration;
using System.Text;

namespace Logging.EventHub
{
    public class EventHubTraceListener : CustomTraceListener
    {
        private IEventHubClient client;
        private readonly string instanceId;

        public EventHubTraceListener() : this(CreateEventHub())
        {
            this.instanceId = ConfigurationManager.AppSettings["InstanceId"];
            CreateEventHub();
        }

        public EventHubTraceListener(IEventHubClient hubClient)
        {
            this.client = hubClient;
        }

        private static IEventHubClient CreateMockEventHub()
        {
            return new MockEventHubClient();
        }


        private static IEventHubClient CreateEventHub()
        {
            var eventHubConnectionString = ConfigurationManager.AppSettings["EventHubConnectionString"];
            var eventHubName = ConfigurationManager.AppSettings["EventHubName"];

            return new EventHubClientAdapter(EventHubClient.CreateFromConnectionString(eventHubConnectionString));
        }
        
        public override void Write(string message)
        {
            Log(message);
        }

        public override void WriteLine(string message)
        {
            Log(message);
        }



        private void Log(string message)
        {
            var eventData = new EventData(
                Encoding.Default.GetBytes(
                    JsonConvert.SerializeObject(new LogMessageEvent
                      {
                          InstanceId = instanceId,
                          MachineName = Environment.MachineName,
                          Value = message
                      })));
            client.SendAsync(eventData);
        }
    }
}
