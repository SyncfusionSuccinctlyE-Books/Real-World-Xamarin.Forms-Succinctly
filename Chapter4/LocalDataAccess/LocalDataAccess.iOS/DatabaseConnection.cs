using Foundation;
using LocalDataAccess.iOS;
using SQLite;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]
namespace LocalDataAccess.iOS
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {
            string dbName = "CustomersDatabase.db3";
            string documentsPath = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path;

            var path = Path.Combine(documentsPath, dbName);

            if (!File.Exists(path))
                File.Create(path);

            SQLiteConnectionString connectionString =
                new SQLiteConnectionString(path, false, "p@$$w0rd");

            return new SQLiteConnection(connectionString);
        }
    }
}