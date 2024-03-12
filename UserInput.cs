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
            AnsiConsole.Markup($"\n[blue] start time? format: yyyy-MM-dd HH:mm:ss[/]!\n");
            
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


        public void ShowMenu()
        {
            AnsiConsole.Markup($"[underline red]MENU[/]");
            Console.WriteLine("\n---------------------");
            Console.WriteLine("\nPlease choose one:");
            Console.WriteLine("1 - start new session");
            Console.WriteLine("2 - end session");
            Console.WriteLine("3 - enter start/end times manually");
            Console.WriteLine("4 - see X number of entries");

            var command = Console.ReadLine();

            switch (int.Parse(command))
            {
                case 1:

                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                default:
                    Console.WriteLine("invalid command\n");
                    break;
            }

        }
    }
}
