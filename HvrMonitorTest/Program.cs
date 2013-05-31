using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace HvrMonitorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string txt = "[<109886 EventID:01100> 08/05/2013 02:56:32:267]";

            string re1 = ".*?";	// Non-greedy match on filler
            string re2 = "(?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))(?![\\d])";	// Uninteresting: day
            string re3 = ".*?";	// Non-greedy match on filler
            string re4 = "(?:(?:[0-2]?\\d{1})|(?:[3][01]{1}))(?![\\d])";	// Uninteresting: day
            string re5 = ".*?";	// Non-greedy match on filler
            string re6 = "((?:(?:[0-2]?\\d{1})|(?:[3][01]{1})))(?![\\d])";	// Day 1

            Regex r = new Regex(re1 + re2, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            Match m = r.Match(txt);
            if (m.Success)
            {
                String day1 = m.Groups[1].ToString();
                Console.Write("(" + day1.ToString() + ")" + "\n");
            }
            Console.ReadLine();
        }
    }
}
