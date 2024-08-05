using MVC.WebApp.Entities;
using System.Data;
using Microsoft.Data.SqlClient;

namespace MVC.WebApp.Repositories
{
    public class CategoryRepository: BaseRepository
    {

        public CategoryRepository(IConfiguration config) : base(config)
        {

        }


        public List<Category> GetData()
        {
            string commandString = "Select * FROM Categories;";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var cat = new List<Category>();

            foreach (DataRow row in dataTable.Rows)
            {
                var cate = new Category()
                {
                    CategoryID = (int)row["CategoryID"],
                    CategoryName = (string)row["CategoryName"],
                    Description = row["Description"]?.ToString(),

                };

                cat.Add(cate);
            }

            return cat;
        }

    }
}
