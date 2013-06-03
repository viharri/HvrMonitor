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
            HvrMonitor monitorObject = new HvrMonitor();
            monitorObject.TracesFromFileRunTask(args);
            // Console.ReadLine();
        }
    }
}
