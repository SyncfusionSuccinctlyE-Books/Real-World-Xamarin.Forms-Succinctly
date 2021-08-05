using System;
using System.Collections.Generic;
using System.Text;

namespace LocalDataAccess
{
    public interface IDatabaseConnection
    {
        SQLite.SQLiteConnection DbConnection();
    }
}
