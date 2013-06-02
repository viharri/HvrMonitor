using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace HvrMonitor
{
    public class ExtractTracesFromFile
    {
        static Int16 m_maxNumberOfInstance = 5;   // To keep limit on the number of parallel threads that can be allowed.
        String m_frutiTracesFileName;             // Contains the file name from which traces are to be extracted.

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

        //
        // Get & Set methods for frutiTracesFileName.
        //
        public String FileName
        {
            get
            {
                return m_frutiTracesFileName;
            }
            set
            {
                m_frutiTracesFileName = value;
            }
        }
        #endregion

        #region Entry Point
        //
        // For console application this is the entry point.
        //
        public void RunTask()
        {
            try
            {
                Console.WriteLine("Enter the name of the file:");
                m_frutiTracesFileName = Console.ReadLine();

                if (!File.Exists(m_frutiTracesFileName))
                {
                    Console.WriteLine("{0} does not exist.", m_frutiTracesFileName);
                    return;
                }
                using (StreamReader sr = File.OpenText(m_frutiTracesFileName))
                {
                    String input;
                    TraceObject frutiTraceObject = new TraceObject();
                    while ((input = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("\n---------------------");
                        Console.WriteLine(input);
                        HvrMonitor.ParseTraceLine(input, ref frutiTraceObject);
                        Console.WriteLine("---------------------");
                    }
                    Console.WriteLine("The end of the stream has been reached.");
                }
            }
            catch (Exception ex)
            {
                // Let the user know what went wrong.
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(ex.Message);
            }
        }
        #endregion
    }
}
