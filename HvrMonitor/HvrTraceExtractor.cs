using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HvrMonitor
{
    public class HvrTraceExtractor
    {
        static Int16 m_maxNumberOfInstance = 5;   // To keep limit on the number of parallel threads that can be allowed.

        #region Get & Set methods
        //
        // Get method for maxNumberOfInstance.
        //
        public Int16 MaxNumberOfInstance
        {
            get
            {
                return m_maxNumberOfInstance;
            }
        }
        #endregion

        #region Entry Point
        //
        // For console application this is the entry point.
        //
        public void RunTask(String FrutiTracesFileName)
        {
            try
            {
                using (StreamReader sr = File.OpenText(FrutiTracesFileName))
                {
                    String input;
                    while ((input = sr.ReadLine()) != null)
                    {
                        input.Trim();
                        if (!input.Equals(" "))
                        {
                            TraceObject frutiTraceObject = new TraceObject();

                            Console.WriteLine("\n---------------------");
                            Console.WriteLine(input);
                            HvrMonitor.ParseTraceLine(input, ref frutiTraceObject);
                            frutiTraceObject.PrintTraceObject();
                            Console.WriteLine("---------------------");
                        }
                    }
                    Console.WriteLine("The end of the stream has been reached.");
                }
            }
            catch (Exception ex)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
                Console.WriteLine("StackTrace: \n{0}", ex.StackTrace);
            }
        }
        #endregion
    }
}
