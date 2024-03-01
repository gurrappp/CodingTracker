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

        public void SetStartDateTime()
        {
            // 2024-03-01 23:59:59
            AnsiConsole.Markup($"[blue] start time? format: yyyy-MM-dd HH:mm:ss[/]!\n");
            
            var readLine = Console.ReadLine();

            

            if (DateTime.TryParse(readLine, FormatProvider, DateTimeStyles.None, out var result))
            {
                StartTime = result;
            }
            else
            {
                AnsiConsole.Markup($"[underline red] Can't parse input! using DateTime.Now[/]!");
                StartTime = DateTime.Now;
            }
        }

        public DateTime StartSession(DateTime startTime) => startTime;
        
    }
}
