using Microsoft.Data.SqlClient;
using Core.Entities;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public  class ShipperRepository : BaseRepository
    {
        public ShipperRepository(IConfiguration config) : base(config)
        {

        }
        public  List<Shipper> GetData()
        {
            string commandString = "Select * FROM Shippers";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);
            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var ship = new List<Shipper>();

            foreach (DataRow row in dataTable.Rows)
            {
                var ships = new Shipper()
                {
                    ShipperID = (int)row["ShipperID"],
                    CompanyName = (string)row["CompanyName"],
                    Phone = row["Phone"]?.ToString(),

                };

                ship.Add(ships);
            }

            return ship;
        }

    }
}
