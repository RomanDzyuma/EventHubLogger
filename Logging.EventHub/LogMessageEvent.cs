namespace Logging.EventHub
{
    public class LogMessageEvent
    {
        public string MachineName { get; set; }
        
        public string InstanceId { get; set; }

        public string Value { get; set; }
    }
}
