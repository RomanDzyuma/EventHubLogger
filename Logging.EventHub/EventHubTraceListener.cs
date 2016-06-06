namespace Logging.EventHub
{
    using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
    using Microsoft.Practices.EnterpriseLibrary.Logging;
    using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
    using Microsoft.ServiceBus.Messaging;

    using System.Configuration;
    using System.Diagnostics;
    using System.Text;

    [ConfigurationElementType(typeof(CustomTraceListener))]

    public class EventHubTraceListener : CustomTraceListener
    {
        private IEventHubClient client;
        private readonly string instanceId;

        public EventHubTraceListener() : this(CreateMockEventHub())
        {
            instanceId = ConfigurationManager.AppSettings["InstanceId"];
            CreateEventHub();
        }

        public EventHubTraceListener(IEventHubClient hubClient)
        {
            client = hubClient;
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
            var eventData = new EventData(Encoding.Default.GetBytes(message));
            client.SendAsync(eventData);
        }


        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if ((Filter == null) || Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data, null))
            {
                if (data is LogEntry)
                {
                    if (Formatter != null)
                    {
                        Write(Formatter.Format(data as LogEntry));
                    }
                    else
                    {
                        base.TraceData(eventCache, source, eventType, id, data);
                    }
                }
                else
                {
                    base.TraceData(eventCache, source, eventType, id, data);
                }
            }
            }
        }
}
