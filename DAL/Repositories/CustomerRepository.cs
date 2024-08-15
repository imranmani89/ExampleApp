using Microsoft.Data.SqlClient;
using Core.Entities;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository(IConfiguration config): base(config)
        {
            
        }


        public List<Customer> GetData()
        {
            string commandString = "Select * FROM Customers;";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var custs = new List<Customer>();

            foreach (DataRow row in dataTable.Rows)
            {
                var cust = new Customer()
                {
                    CustomerID = (string)row["CustomerID"],
                    CompanyName = (string)row["CompanyName"],
                    ContactName = row["ContactName"]?.ToString(),
                    ContactTitle = row["ContactTitle"]?.ToString(),
                    Address = row["Address"]?.ToString(),
                    City = row["City"]?.ToString(),
                    Region = row["Region"]?.ToString(),
                    PostalCode = row["PostalCode"]?.ToString(),
                    Country = row["Country"]?.ToString(),
                    Phone = row["Phone"]?.ToString(),
                    Fax = row["Fax"]?.ToString(),
                };


                custs.Add(cust);
            }

            return custs;
        }

    }
}
