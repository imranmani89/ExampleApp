using Microsoft.Data.SqlClient;
using MVC.WebApp.Entities;
using System.Data;

namespace MVC.WebApp.Repositories
{
    public  class RegionRepository : BaseRepository
    {
        public RegionRepository(IConfiguration config) : base(config)
        {

        }
        public  List<Region> GetData()
        {
            string commandString = "Select * FROM Region";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var Reg = new List<Region>();

            foreach (DataRow row in dataTable.Rows)
            {
                var Regs = new Region()
                {
                    RegionID = (int)row["RegionID"],
                    RegionDescription = (string)row["RegionDescription"],   

                };

                Reg.Add(Regs);
            }

            return Reg;
        }

    }
}
