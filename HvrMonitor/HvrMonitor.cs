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
            try
            {
                String[] traceParts = FrutiTraceLine.Split(new Char[] { '|' });

                Int16 eventId;
                DateTime eventTime;
                ExtractIdAndTime(traceParts[0], out eventId, out eventTime);
                FrutiTraceObject.EventId = eventId;
                FrutiTraceObject.EventTime = eventTime;
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression.
                Console.WriteLine("HvrMonitor::ParseTraceLine:",ex.Message);
            }
        }

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
                    Console.WriteLine("EventId: {0}", eventId);

                    String eventTime = Regex.Match(match.Value, eventTimePattern).Value;
                    eventTime.Trim();
                    DateTime.TryParse(eventTime, out EventTime);
                    Console.WriteLine("EventTime: {0}", eventTime);
                }
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression.
                Console.WriteLine("HvrMonitor::ExtractIdAndTime:", ex.Message);
            }
        }
    }
}
