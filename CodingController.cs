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
            //ShowSessions();

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

            AnsiConsole.Write(
                new FigletText("CODING TRACKER")
                    .Centered()
                    .Color(Color.Red));
        }

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
                default:
                    Console.WriteLine("invalid command\n");
                    break;
            }

        }

        public static void ShowSessions()
        {
            //var grid = new Grid();

            //// Add columns 
            //grid.AddColumn();
            //grid.AddColumn();
            //grid.AddColumn();
            //grid.AddColumn();

            //// Add header row 
            //grid.AddRow(new string[] { "Id", "Duration", "StartTime", "EndTime" });
            //grid.AddRow(new string[] { "Col 1", "Col 2", "Col 3" , "Col 4"});

            using (var connection = new SqlConnection(connectionString))
            {
                var sql = "SELECT * FROM coding_habits";
                var habits = connection.Query(sql);

                foreach (var habit in habits)
                {
                    Console.WriteLine($"{habit} {habit.CompanyName}");
                }
            }

            //// Write to Console
            //AnsiConsole.Write(grid);

            // Create a table
            var table = new Table();

            // Add some columns
            table.AddColumn("Id");
            table.AddColumn("Duration");
            table.AddColumn("StartTime");
            table.AddColumn("EndTime");

            // Add some rows
            table.AddRow("Baz", "[green]Qux[/]", "3" ,"4");
            table.Border = TableBorder.MinimalDoubleHead;

            table.Centered();

            // Render the table to the console
            AnsiConsole.Write(table);

        }

        public void StartSession()
        {
            //userInput.SetStartDateTime();

            userInput.StartTime = DateTime.Now;
            codingSession.StartTime = userInput.StartSession(userInput.StartTime);


            Console.ReadLine();
        }

        public void EndSession()
        {
            codingSession.EndTime = DateTime.Now;

        }

        public void SetStartEndTimes()
        {
            userInput.SetStartEndDateTime();

        }
    }
}
