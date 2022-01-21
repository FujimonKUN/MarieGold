using System;
using System.Data;
using System.Linq;
using Microsoft.Data.Sqlite;

namespace MarieGold.SandBox {
    class Program {
        static void Main(string[] args) {
            using (var sqlite = new SqliteConnection("Data Source=:memory:")) {
                sqlite.Open();

                using (var command = new SqliteCommand()) {
                    command.Connection = sqlite;

                    command.CommandText =
                        "CREATE TABLE MARIEGOLD(ID INTEGER PRIMARY KEY AUTOINCREMENT, FILENAME TEXT, BASE64 TEXT)";
                    command.ExecuteNonQuery();

                    command.Transaction = sqlite.BeginTransaction();
                    foreach (var i in Enumerable.Range(0, 114514)) {
                        command.CommandText = "INSERT INTO MARIEGOLD(FILENAME, BASE64) VALUES(@FILENAME, @BASE64)";

                        command.Parameters.AddWithValue("@FILENAME", $"OMAE NO KAWARAI_{i}");
                        command.Parameters.AddWithValue("@BASE64", "HEY GUYS");

                        command.ExecuteNonQuery();

                        command.Parameters.Clear();
                    }

                    command.Transaction?.Commit();
                }

                using (var command = new SqliteCommand()) {
                    command.Connection = sqlite;
                    command.CommandText = "SELECT * FROM MARIEGOLD WHERE ID >= 10";

                    var dt = new DataTable();
                    using (var reader = command.ExecuteReader()) {
                        dt.Load(reader);
                    }

                    foreach (var data in dt.Select()) {
                        Console.WriteLine($"ID: {data["ID"]} - Filename: {data["FILENAME"]}");
                    }
                }
            }
        }
    }
}