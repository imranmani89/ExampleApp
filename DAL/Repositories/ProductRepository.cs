using Microsoft.Data.SqlClient;
using Core.Entities;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public  class ProductRepository : BaseRepository
    {
        public ProductRepository(IConfiguration config) : base(config)
        {

        }
        public  List<Product> GetData()
        {
            string connecion = "Select * from [Products]";
            var sqlConnection=new SqlConnection(_connectionString);
            var command= new SqlCommand(connecion, sqlConnection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var prods = new List<Product>();
            foreach (DataRow row in dataTable.Rows)
            {
                var prod = new Product()
                {
                    ProductID=(int)row["ProductID"],
                    ProductName = (string)row["ProductName"],
                    SupplierID = row["SupplierID"] as int?,
                    CategoryID= row["CategoryID"] as int?,
                    QuantityPerUnit = row["SupplierID"]?.ToString(),
                    UnitPrice = row["UnitPrice"] as decimal?,
                    UnitsInStock = row["UnitsInStock"] as int?,
                    UnitsOnOrder = row["UnitsOnOrder"] as int?,
                    ReorderLevel = row["ReorderLevel"] as int?,
                    Discontinued = (bool)row["Discontinued"],

                };
                prods.Add(prod);
            }
            return prods;


        }
    }
}
