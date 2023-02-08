using PetStore;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetStore
{
    public class SqliteDataAccess
    {
        public static List<CustomerModel> LoadCustomers()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                List<CustomerModel> output = cnn.Query<CustomerModel>("SELECT * FROM Customers", new DynamicParameters()).ToList();
                return output;
            }
        }
        public static void SaveCustomer(CustomerModel customer)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Customers (CustomerName) values (@CustomerName)", customer);
            }

        }

        public static List<StoreModel> LoadStores()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<StoreModel>("select * from Stores", new DynamicParameters());
                return output.ToList();
            }
        }

        public static List<ProductModel> LoadProducts()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                List<ProductModel> output = cnn.Query<ProductModel>("SELECT * FROM Products", new DynamicParameters()).ToList();
                return output;
            }
        }

        public static void RemoveProduct(ProductModel ProductID)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("DELETE FROM Products WHERE ProductID = (@ProductID)", ProductID);
            }
            
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
