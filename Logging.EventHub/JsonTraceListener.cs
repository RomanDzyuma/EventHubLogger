using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.EventHub
{
    public class JsonTraceListener : CustomTraceListener
    {
        private readonly CustomTraceListener listener;

        public JsonTraceListener()
            : this(new EventHubTraceListener())
        {

        }

        public JsonTraceListener(CustomTraceListener listener)
        {
            this.listener = listener;
        }

        public override void Write(string message)
        {
            listener.Write(message);
        }

        public override void WriteLine(string message)
        {
            listener.WriteLine(message);
        }


        public override void Write(object o)
        {
            var message = JsonConvert.SerializeObject(o);
            listener.Write(message);
        }
    }
}
