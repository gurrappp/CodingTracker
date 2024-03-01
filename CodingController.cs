using Microsoft.Data.Sqlite;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker
{


    public class CodingController
    {
        public static void Main(string[] args)
        {
            SetupDatabase();
            StartMessage();
            ShowSessions();

            CodingSession session = new CodingSession();
            UserInput userInput = new UserInput();


            userInput.SetStartDateTime();

            session.StartTime = userInput.StartSession(userInput.StartTime);


            Console.ReadLine();

        }

        public static void SetupDatabase()
        {
            NameValueCollection sAll = ConfigurationManager.AppSettings;
            string dbString = ConfigurationManager.AppSettings.Get("DatabasePath");

            using (var connection = new SqliteConnection(dbString))
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
        public static void Menu()
        {
            AnsiConsole.Markup($"[underline red] Can't parse input! using DateTime.Now[/]!");

        }

        public static void ShowSessions()
        {
            var grid = new Grid();

            // Add columns 
            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();
            grid.AddColumn();

            // Add header row 
            grid.AddRow(new string[] { "Id", "Duration", "StartTime", "EndTime" });
            grid.AddRow(new string[] { "Row 1", "Row 2", "Row 3" , "Row 4"});

            // Write to Console
            AnsiConsole.Write(grid);

        }
    }
}
