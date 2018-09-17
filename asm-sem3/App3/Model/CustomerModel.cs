using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App3.Model
{
    public class CustomerModel
    {
        private static ObservableCollection<Entity.Customer> listCustomer;

        public static ObservableCollection<Entity.Customer> GetCustomers(List<string> filter)
        {
            DataAccess.InitializeDatabase();

            if (listCustomer == null)
            {
                listCustomer = new ObservableCollection<Entity.Customer>();

            }
            using (SqliteConnection db = new SqliteConnection("Filename=customers_manager.db"))
            {
                db.Open();

                SqliteCommand selectCommand = new SqliteCommand();
                selectCommand.Connection = db;
                if (filter.Count == 0)
                {
                    selectCommand.CommandText = "SELECT * FROM customers";
                }
                else
                {
                    selectCommand.CommandText = "SELECT * FROM customers WHERE " + filter[1].ToLower() + " LIKE '" + filter[0] + "'";
                }
                SqliteDataReader sqliteData = selectCommand.ExecuteReader();
                Entity.Customer customer;
                while (sqliteData.Read())
                {
                    customer = new Entity.Customer
                    {
                        Id = Convert.ToInt16(sqliteData["id"]),
                        Name = Convert.ToString(sqliteData["name"]),
                        Email = Convert.ToString(sqliteData["email"]),
                        Phone = Convert.ToString(sqliteData["phone"]),
                        Address = Convert.ToString(sqliteData["address"]),
                        Thumbnail = Convert.ToString(sqliteData["thumbnail"]),
                    };
                    listCustomer.Add(customer);
                }
                db.Close();
            }
            if (listCustomer == null)
            {
                listCustomer = new ObservableCollection<Entity.Customer>();
            }
            return listCustomer;
        }

        public static void SetCustomers(ObservableCollection<Entity.Customer> customers)
        {
            listCustomer = customers;
        }

        public static void AddCustomer(Entity.Customer customer)
        {
            DataAccess.InitializeDatabase();
            using (SqliteConnection db = new SqliteConnection("Filename=customers_manager.db"))
            {
                db.Open();

                SqliteCommand insertCommand = new SqliteCommand();
                insertCommand.Connection = db;

                // Use parameterized query to prevent SQL injection attacks
                insertCommand.CommandText = "INSERT INTO customers (name, email, phone, address, thumbnail) VALUES (@name, @email, @phone, @address, @thumbnail);";
                insertCommand.Parameters.AddWithValue("@name", customer.Name);
                insertCommand.Parameters.AddWithValue("@email", customer.Email);
                insertCommand.Parameters.AddWithValue("@phone", customer.Phone);
                insertCommand.Parameters.AddWithValue("@address", customer.Address);
                insertCommand.Parameters.AddWithValue("@thumbnail", customer.Thumbnail);

                insertCommand.ExecuteReader();

                db.Close();
            }
            if (listCustomer == null)
            {
                listCustomer = new ObservableCollection<Entity.Customer>();
            }
            listCustomer.Add(customer);
        }
    }
}
