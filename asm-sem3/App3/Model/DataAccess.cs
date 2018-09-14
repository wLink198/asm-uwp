﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.Model
{
    class DataAccess
    {
        public static void InitializeDatabase()
        {
            using (SqliteConnection db =
                new SqliteConnection("Filename=customers_manager.db"))
            {
                db.Open();

                String tableCommand = "CREATE TABLE IF NOT " +
                    "EXISTS customers (id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                    "name NVARCHAR(50) NOT NULL, " +
                    "email NVARCHAR(255), " +
                    "phone NVARCHAR(50), " +
                    "address NVARCHAR(50), " +
                    "thumbnail NVARCHAR(255)" +
                    ") ";

                SqliteCommand createTable = new SqliteCommand(tableCommand, db);

                createTable.ExecuteReader();
            }
        }
    }
}
