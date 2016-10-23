using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auto
{
    public interface ILogger
    {
        void Debug(string log, params object[] arg);
        void Main(string log, params object[] arg);
    }

    public class ConsoleLogger : ILogger
    {
        public void Debug(string log, params object[] arg)
        {
           // Console.WriteLine(log, arg);
        }

        public void Main(string log, params object[] arg)
        {
            Console.WriteLine(log, arg);
        }
    }

    public class Logger
    {
        public static ILogger Log { get; set; }
        public static ILogger Get
        {
            get
            {
                if (Log == null)
                {
                    Log = new ConsoleLogger();
                }
                return Log;
            }
        }

        public static void Debug(string log, params object[] arg)
        {
            Get.Debug(log, arg);
        }

        public static void Main(string log, params object[] arg)
        {
            Get.Main(log, arg);
        }
    }
}
