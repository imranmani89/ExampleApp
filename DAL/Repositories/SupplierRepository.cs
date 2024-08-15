using System.Data;
using Core.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public  class SupplierRepository : BaseRepository
    {
        public SupplierRepository(IConfiguration config) : base(config)
        {

        }
        public  List<Supplier> GetData()
        {
            string commandString = "Select * FROM Suppliers";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var Sup = new List<Supplier>();

            foreach (DataRow row in dataTable.Rows)
            {
                var Sups = new Supplier()
                {
                    SupplierID=(int)row["SupplierID"],
CompanyName=(string)row["CompanyName"],
ContactName=row["ContactName"]?.ToString(),
ContactTitle=row["ContactTitle"]?.ToString(),
Address=row["Address"]?.ToString(),
City=row["City"]?.ToString(),
Region=row["Region"]?.ToString(),
PostalCode=row["PostalCode"]?.ToString(),
Country=row["Country"]?.ToString(),
Phone=row["Phone"]?.ToString(),
Fax=row["Fax"]?.ToString(),
HomePage=row["HomePage"]?.ToString(),


                };

                Sup.Add(Sups);
            }

            return Sup;
        }

    }
}
