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
                logger.Write("TEST");
                logger.Write("TEST2");
            }
            else
            {
                Console.WriteLine("Logging is disabled in the configuration");
            }

            Console.ReadLine();
        }
    }
}
