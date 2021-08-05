using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LocalDataAccess.Droid;
using SQLite;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection))]
namespace LocalDataAccess.Droid
{
    public class DatabaseConnection : IDatabaseConnection
    {
        public SQLiteConnection DbConnection()
        {

            string dbName = "CustomersDatabase.db3";

            string path = Path.Combine(System.Environment.
              GetFolderPath(System.Environment.
              SpecialFolder.Personal), dbName);

            if (!File.Exists(path))
                File.Create(path);

            SQLiteConnectionString connectionString = 
                new SQLiteConnectionString(path, false, "p@$$w0rd");
            return new SQLiteConnection(connectionString);
        }
    }
}