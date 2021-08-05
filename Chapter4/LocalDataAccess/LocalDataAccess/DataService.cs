using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace LocalDataAccess
{
    public class DataService
    {

        private SQLiteConnection database;
        private static object collisionLock = new object();

        public DataService()
        {

            database =
                DependencyService.Get<IDatabaseConnection>().
                DbConnection();
            database.CreateTable<Customer>();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            lock (collisionLock)
            {
                return database.Table<Customer>().AsEnumerable();
            }
        }

        public Customer GetCustomer(int id)
        {
            lock (collisionLock)
            {
                return database.Table<Customer>().
                  FirstOrDefault(customer => customer.Id == id);
            }
        }

        public IEnumerable<Customer> GetFilteredCustomers(string countryName)
        {
            lock (collisionLock)
            {
                var query = from cust in database.Table<Customer>()
                            where cust.Country == countryName
                            select cust;
                return query.AsEnumerable();
            }
        }

        // Direct SQL

        //public IEnumerable<Customer> GetFilteredCustomers(string countryName)
        //{
        //    lock (collisionLock)
        //    {
        //        return database.Query<Customer>(
        //          $"SELECT * FROM Item WHERE Country = '{countryName}'").AsEnumerable();
        //    }
        //}

        public int SaveCustomer(Customer customerInstance)
        {
            lock (collisionLock)
            {
                if (customerInstance.Id != 0)
                {
                    database.Update(customerInstance);
                    return customerInstance.Id;
                }
                else
                {
                    database.Insert(customerInstance);
                    return customerInstance.Id;
                }
            }
        }

        public void SaveAllCustomers(IEnumerable<Customer> customerCollection)
        {
            lock (collisionLock)
            {
                foreach (var customerInstance in customerCollection)
                {
                    if (customerInstance.Id != 0)
                    {
                        database.Update(customerInstance);
                    }
                    else
                    {
                        database.Insert(customerInstance);
                    }
                }
            }
        }

        public int DeleteCustomer(Customer customerInstance)
        {
            var id = customerInstance.Id;
            if (id != 0)
            {
                lock (collisionLock)
                {
                    database.Delete<Customer>(id);
                }
            }
            
            return id;
        }
    }
}
