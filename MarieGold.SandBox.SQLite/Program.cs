using System;
using System.Data;
using System.IO;
using Microsoft.Data.Sqlite;

namespace MarieGold.SandBox.SQLite {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            using (var sqlite = new SqliteConnection("Data Source=mariegold.db")) {
                sqlite.Open();
                using (var command = new SqliteCommand()) {
                    command.Connection = sqlite;

                    command.CommandText =
                        "CREATE  TABLE IF NOT EXISTS MARIEGOLD_IMAGES(" +
                        "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "FILENAME TEXT, " +
                        "BASE64 TEXT)";

                    command.ExecuteNonQuery();

                    var filename = "crazy pinky.png";
                    var file = File.ReadAllBytes(filename);

                    command.CommandText =
                        $"INSERT INTO MARIEGOLD_IMAGES(FILENAME, BASE64) VALUES(@FILENAME, @BASE64)";
                    var parameter1 = new SqliteParameter("FILENAME", filename);
                    var parameter2 = new SqliteParameter("BASE64", Convert.ToBase64String(file));

                    command.Parameters.Add(parameter1);
                    command.Parameters.Add(parameter2);

                    command.ExecuteNonQuery();

                    var dt = new DataTable();

                    command.CommandText = "SELECT * FROM MARIEGOLD_IMAGES";
                    using (var reader = command.ExecuteReader()) {
                        dt.Load(reader);
                    }

                    foreach (var var1 in dt.Select()) {
                        var id = var1["ID"];
                        var filename2 = var1["FILENAME"];
                        var base64 = var1["BASE64"];

                        Console.WriteLine($"ID: {id} - FILENAME: {filename2}");

                        File.WriteAllBytes($"{id}_{filename2}",
                            Convert.FromBase64String(base64.ToString() ?? string.Empty));
                    }
                }
            }
        }
    }
}