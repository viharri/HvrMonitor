using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HvrMonitor
{
    class HvrMonitorEntryPoint
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("{0}", DateTime.UtcNow);
            HvrMonitor monitorObject = new HvrMonitor();
            monitorObject.TracesFromFileRunTask(args);
            Console.WriteLine("{0}", DateTime.UtcNow);
            // Console.ReadLine();
        }
    }
}
