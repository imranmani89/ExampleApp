using Microsoft.Data.SqlClient;
using Core.Entities;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace DAL.Repositories
{
    public  class TerritoryRepository : BaseRepository
    {
        public TerritoryRepository(IConfiguration config) : base(config)
        {

        }

        public  List<Territory> GetData()
        {
            string commandString = "Select * FROM Territories";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var Tery = new List<Territory>();

            foreach (DataRow row in dataTable.Rows)
            {
                var Terys = new Territory()
                {
                    TerritoryID = (string)row["TerritoryID"],
                    TerritoryDescription = (string)row["TerritoryDescription"],
                    RegionID = (int)row["RegionID"],

                };

                Tery.Add(Terys);
            }

            return Tery;
        }

    }
}
