using Logging.EventHub;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logging.Demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var logger = new LogWriterFactory().Create();
            if (logger.IsLoggingEnabled())
            {
                logger.Write(new LogMessageEvent { InstanceId = "instance1", MachineName = "machine1", Value = "value1" });
                logger.Write(new LogMessageEvent2 { InstanceId = "instance1", MachineName = "machine1", Value2 = "value1" });
            }
            else
            {
                Console.WriteLine("Logging is disabled in the configuration");
            }

            Console.ReadLine();
        }
    }
}
