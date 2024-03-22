using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{
    public class UserInput
    {

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public IFormatProvider FormatProvider = CultureInfo.CurrentCulture;

        public DateTime SetStartDateTime()
        {
            // 2024-03-01 23:59:59
            AnsiConsole.Markup($"\n[blue] start time? format: yyyy-MM-dd HH:mm:ss[/]!\n");
            
            var startTime = Console.ReadLine();

            
            if (DateTime.TryParse(startTime, FormatProvider, DateTimeStyles.None, out var startTimeResult))
            {
                StartTime = startTimeResult;
            }
            else
            {
                AnsiConsole.Markup($"[underline red] Can't parse input! using DateTime.Now[/]!");
                StartTime = DateTime.Now;
            }

           return StartTime;
        }

        public DateTime SetEndDateTime()
        {
            
            AnsiConsole.Markup($"\n[blue] END time? format: yyyy-MM-dd HH:mm:ss[/]!\n");

            var endTime = Console.ReadLine();

            if (DateTime.TryParse(endTime, FormatProvider, DateTimeStyles.None, out var endTimeResult))
            {
                EndTime = endTimeResult;
            }
            else
            {
                AnsiConsole.Markup($"[underline red] Can't parse input! using DateTime.Now[/]!");
                EndTime = DateTime.Now;
            }
            return EndTime;
        }

        public DateTime StartSession(DateTime startTime) => startTime;


        
    }
}
