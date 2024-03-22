using Microsoft.Data.Sqlite;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using static System.Collections.Specialized.BitVector32;
using System.Collections;

namespace CodingTracker
{


    public class CodingController
    {

        public UserInput userInput;
        public CodingSession codingSession;
        public static string? connectionString;

        public CodingController()
        {
            userInput = new UserInput();
            codingSession = new CodingSession();
            connectionString = ConfigurationManager.AppSettings.Get("DatabasePath");
        }

        public static void Main(string[] args)
        {
            var codingController = new CodingController();
            
            SetupDatabase();
            StartMessage();
            
            codingController.ShowMenu();

        }

        public static void SetupDatabase()
        {
            NameValueCollection sAll = ConfigurationManager.AppSettings;
            
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS coding_habits (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Duration Text,
                        StartTime Text,
                        EndTime Text
                        )";

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void StartMessage()
        {
            //var font = FigletFont.Load("starwars.flf");
            Console.Clear();
            AnsiConsole.Write(
                new FigletText("CODING TRACKER")
                    .Centered()
                    .Color(Color.Red));
        }

        public void ShowMenu()
        {
            //Console.Clear();
            bool closeApp = false;
            while (!closeApp)
            {
                AnsiConsole.Markup($"[underline red]MENU[/]");
                Console.WriteLine("\n---------------------");
                Console.WriteLine("\nPlease choose one:");
                Console.WriteLine("1 - start new session");
                Console.WriteLine("2 - end session");
                Console.WriteLine("3 - enter start/end times manually");
                Console.WriteLine("4 - see X number of entries");
                Console.WriteLine("5 - see current session duration");

                Console.WriteLine("0 - close app");

                var command = Console.ReadLine();
                StartMessage();
                switch (int.Parse(command))
                {
                    case 0:
                        closeApp = true;
                        break;
                    case 1:
                        StartSession();
                        break;
                    case 2:
                        EndSession();
                        break;
                    case 3:
                        SetStartEndTimes();
                        break;
                    case 4:
                        ShowSessions();
                        break;
                    case 5:
                        if (!codingSession.HasStarted)
                        {
                            Console.WriteLine("Session not started yet");
                            break;
                        }
                        ShowCurrentSessionDuration();
                        break;
                    default:
                        Console.WriteLine("invalid command\n");
                        break;
                }
            }
        }

        public void ShowCurrentSessionDuration()
        {
            TimeSpan value = DateTime.Now.Subtract(codingSession.StartTime);
            DateTime date = DateTime.Parse(value.ToString());
            
            Console.WriteLine($"Current duration is : {date.ToString("HH:mm:ss")}\n");
        }

        public static void ShowSessions()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = "SELECT * FROM coding_habits";
                IEnumerable<dynamic> query = connection.Query<CodingSession>(sql);
                
                // Create a table
                var table = new Table();
                table.AddColumns("Id", "Duration", "StartTime", "EndTime");
                
                foreach (CodingSession rows in query)
                {
                    //fields = rows as IDictionary<string, object>;

                    table.AddRow(rows.Id.ToString(), rows.Duration ?? "", rows.StartTime.ToString(), rows.EndTime.ToString());
                    
                }
                
                table.Border = TableBorder.MinimalDoubleHead;

                table.Centered();
                
                // Render the table to the console
                AnsiConsole.Write(table);
            }
        }

        public void StartSession()
        {
            //userInput.SetStartDateTime();
            codingSession.HasStarted = true;
            userInput.StartTime = DateTime.Now;
            codingSession.StartTime = userInput.StartSession(userInput.StartTime);

        }

        public void EndSession()
        {

            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = "INSERT INTO coding_habits (Duration, StartTime, EndTime) VALUES (@Duration, @StartTime, @EndTime )";
                codingSession.EndTime = DateTime.Now;
                TimeSpan value = codingSession.EndTime.Subtract(codingSession.StartTime);
                DateTime date = DateTime.Parse(value.ToString());
                codingSession.Duration = date.ToString("HH:mm:ss");

                connection.Execute(sql, codingSession);
            }

            codingSession.HasStarted = false;

        }

        public void SetStartEndTimes()
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                var sql = "INSERT INTO coding_habits (Duration, StartTime, EndTime) VALUES (@Duration, @StartTime, @EndTime )";

                codingSession.StartTime = userInput.SetStartDateTime();
                codingSession.EndTime = userInput.SetEndDateTime();

                TimeSpan value = codingSession.EndTime.Subtract(codingSession.StartTime);
                DateTime date = DateTime.Parse(value.ToString());
                codingSession.Duration = date.ToString("HH:mm:ss");

                connection.Execute(sql, codingSession);
            }
        }
    }
}
