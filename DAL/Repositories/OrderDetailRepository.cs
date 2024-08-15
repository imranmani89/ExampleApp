using Microsoft.Data.SqlClient;
using Core.Entities;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public class OrderDetailRepository : BaseRepository
    {

        public OrderDetailRepository(IConfiguration config) : base(config)
        {
            
        }
        public List<OrderDetail> GetData()
        {
            string commandString = $"Select * from [Order Details]";
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            var obj = new List<OrderDetail>();
            foreach (DataRow row in dataTable.Rows)
            {
                var ord = new OrderDetail()
                {
                    OrderID = (int)row["OrderID"],
                    ProductID = (int)row["ProductID"],
                    UnitPrice = (decimal)row["UnitPrice"],
                    Quantity = (Int16)row["Quantity"],
                    Discount = (float)row["Discount"],
                };
                obj.Add(ord);
            }
            return obj;
        }
    }
}
