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

            CodingSession session = new CodingSession();
            UserInput userInput = new UserInput();



            var userInputStart = Console.ReadLine();
            session.StartTime = userInput.StartSession(userInputStart);

            AnsiConsole.Markup($"[underline red]{DateTime.Now}[/]!");


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

    }
}
