using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace HvrMonitor
{
    public class HvrMonitor
    {
        public static void Main(String[] args)
        {
            ExtractTracesFromFile fromFileObject = new ExtractTracesFromFile();
            fromFileObject.RunTask();
            Console.ReadLine();
        }

        // Parses a fruti trace line into an trace object.
        public static void ParseTraceLine(String FrutiTraceLine, ref TraceObject FrutiTraceObject)
        {
            //
            // Fruti Trace Format: [<Trace# EventId:Value> Time] | [FunctionName (FileName)] Trace Message.
            //
            try
            {
                String[] traceParts = FrutiTraceLine.Split(new Char[] { '|' });

                Int16 eventId;
                DateTime eventTime;
                ExtractIdAndTime(traceParts[0], out eventId, out eventTime);

                String functionName;
                String fileName;
                ExtractFunctionAndFileNames(traceParts[1], out functionName, out fileName);

                String messageTracePart = traceParts[1].Substring(traceParts[1].IndexOf(")]") + 2);
                messageTracePart.Trim();

                String vmName;
                Guid vmGuid;
                String traceMessage;
                ExtractVmNameAndGuid(messageTracePart, out vmName, out vmGuid, out traceMessage);

                // File fruti trace object with extracted fields.
                FrutiTraceObject.EventId = eventId;
                FrutiTraceObject.EventTime = eventTime;
                FrutiTraceObject.FunctionName = functionName;
                FrutiTraceObject.FileName = fileName;
                FrutiTraceObject.VmName = vmName;
                FrutiTraceObject.VmGuid = vmGuid;
                FrutiTraceObject.TraceMessage = traceMessage;
            }
            catch (Exception ex)
            {
                // Syntax error in the regular expression.
                Console.WriteLine("HvrMonitor::ParseTraceLine:{0}", ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        // TODO: Remove Print Statements From Extraction Methods.
        #region Extraction Methods
        private static void ExtractIdAndTime(String FrutiTracePart, out Int16 EventId, out DateTime EventTime)
        {
            EventId = 0;
            EventTime = DateTime.UtcNow;

            try
            {
                // Regex to extract EventId.
                String eventIdPattern = @"\[.*EventID:(?<EventId>\d{5})[^|]*\]";

                // Regex to extract EventTime.
                String dayPattern = @"(?:[0-2]\d{1}|[3][01]{1})[-:./\\]";                       // Regex for Day.
                String monthPattern = @"(?:[0]\d{1}|[1][0-2]{1})[-:./\\]";                      // Regex for Month.
                String yearPattern = @"(?:[123]\d{3})";                                         // Regex for Year.
                String timePattern = @"(?:[01]\d|[2][0-3]):(?:[0-5]\d):(?:[0-5]\d)";            // Regex for Time.

                String eventTimePattern = dayPattern + monthPattern + yearPattern + @"\s" + timePattern;

                MatchCollection matchedPart = Regex.Matches(FrutiTracePart, eventIdPattern);
                foreach (Match match in matchedPart)
                {
                    String eventId = match.Groups["EventId"].Value;
                    eventId.Trim();
                    EventId = Convert.ToInt16(eventId);
                    // Console.WriteLine("EventId: {0}", eventId);

                    String eventTime = Regex.Match(match.Value, eventTimePattern).Value;
                    eventTime.Trim();
                    DateTime.TryParse(eventTime, out EventTime);
                    // Console.WriteLine("EventTime: {0}", eventTime);
                }
            }
            catch (Exception ex)
            {
                // Syntax error in the regular expression.
                Console.WriteLine("HvrMonitor::ExtractIdAndTime:{0}", ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static void ExtractFunctionAndFileNames(String FrutiTracePart, out String FunctionName, out String FileName)
        {
            FunctionName = "";
            FileName = "";

            try
            {
                // Regex to extract function and file names.
                String functionAndFileNamesPattern = @"\[(?<FunctionName>\w+::\w+)\s\((?<FileName>.+)\)\]";

                MatchCollection matchedPart = Regex.Matches(FrutiTracePart, functionAndFileNamesPattern);
                foreach (Match match in matchedPart)
                {
                    FunctionName = match.Groups["FunctionName"].Value;
                    FunctionName.Trim();
                    // Console.WriteLine("FunctionName: {0}", FunctionName);

                    FileName = match.Groups["FileName"].Value;
                    FileName.Trim();
                    // Console.WriteLine("FileName: {0}", FileName);
                }
            }
            catch (Exception ex)
            {
                // Syntax error in the regular expression.
                Console.WriteLine("HvrMonitor::ExtractFunctionAndFileNames:{0}", ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }

        private static void ExtractVmNameAndGuid(String FrutiTracePart, out String VmName, out Guid VmGuid, out String TraceMessage)
        {
            VmName = "";
            VmGuid = Guid.Empty;
            TraceMessage = "";

            try
            {
                // Regex to extract VmGuid.
                String hexPattern = @"[0-9a-fA-F]";
                String vmGuidPattern = System.String.Format("(?<VmGuid>{0}{{8}}-{0}{{4}}-{0}{{4}}-{0}{{4}}-{0}{{12}})", hexPattern);

                String tempGuid = Regex.Match(FrutiTracePart, vmGuidPattern).Groups["VmGuid"].Value;
                tempGuid.Trim();
                Guid.TryParse(tempGuid, out VmGuid);
                // Console.WriteLine("VmGuid: {0}", VmGuid);

                // Not extracting VmName as some of the traces will not have that information.
                String[] traceParts = FrutiTracePart.Split(new String[] {tempGuid}, StringSplitOptions.None);
                TraceMessage = traceParts[1].Trim();
                // Console.WriteLine("TraceMessage: {0}", TraceMessage);
            }
            catch (Exception ex)
            {
                // Syntax error in the regular expression.
                Console.WriteLine("HvrMonitor::ExtractVmNameAndGuid:{0}", ex.Message);
                Console.WriteLine(ex.StackTrace);
            }
        }
        #endregion

        #region Temporary Functionality
        public static void Show(string entries)
        {
            Console.WriteLine("<{0}>", entries);
        }
        #endregion
    }
}
