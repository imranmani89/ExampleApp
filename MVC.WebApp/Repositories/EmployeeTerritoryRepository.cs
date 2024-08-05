using Microsoft.Data.SqlClient;
using MVC.WebApp.Entities;
using System.Data;

namespace MVC.WebApp.Repositories
{
    public class EmployeeTerritoryRepository : BaseRepository
    {
        public EmployeeTerritoryRepository(IConfiguration config) : base(config)
        {

        }

        public List<EmployeeTerritory> GetData()
        {
            string commandString = $"Select * from EmployeeTerritories";
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter=new SqlDataAdapter(command);
            var dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
            var EmpTery = new List<EmployeeTerritory>();
            foreach (DataRow row in  dataTable.Rows)
            {
                var Empterys=new EmployeeTerritory()
                {
                    EmployeeID = (int)row["EmployeeID"],
                    TerritoryID = (string)row["TerritoryID"],
                };
                EmpTery.Add(Empterys);
            }
            return EmpTery;
        }
    }
}
