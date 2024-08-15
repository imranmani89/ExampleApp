using Core.Entities;
using Microsoft.Data.SqlClient;
using Core.Interfaces;
using System.Data;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace DAL.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration config) : base(config)
        {

        }
        public List<Employee> GetData()
        {
            string commandString = "Select * FROM Employees;";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var employees = new List<Employee>();

            foreach (DataRow row in dataTable.Rows)
            {
                var employee = new Employee()
                {
                    EmployeeID = (int)row["EmployeeID"],
                    LastName = (string)row["LastName"],
                    FirstName = (string)row["FirstName"],
                    Title = row["Title"]?.ToString(),
                    TitleOfCourtesy = row["TitleOfCourtesy"]?.ToString(),
                    BirthDate = (row["BirthDate"] as DateTime?),

                    HireDate = (row["HireDate"] as DateTime?),
                    Address = row["Address"]?.ToString(),
                    City = row["City"]?.ToString(),
                    Region = row["Region"]?.ToString(),
                    PostalCode = row["PostalCode"]?.ToString(),
                    Country = row["Country"]?.ToString(),
                    HomePhone = row["HomePhone"]?.ToString(),
                    Extension = row["Extension"]?.ToString(),
                    ReportsTo = (row["ReportsTo"] as int?),
                    PhotoPath = row["PhotoPath"]?.ToString(),

                };

                employees.Add(employee);
            }

            return employees;
        }

        public Employee? GetById(string id)
        {
            string commandString = $"Select * FROM Employees where employeeID = '{id}' ;";

            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            var dataAdapter = new SqlDataAdapter(command);
            var dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            var employees = new List<Employee>();
            if (dataTable.Rows.Count > 0)
            {
                var row = dataTable.Rows[0];
                var employee = new Employee()
                {
                    EmployeeID = (int)row["EmployeeID"],
                    LastName = (string)row["LastName"],
                    FirstName = (string)row["FirstName"],
                    Title = row["Title"]?.ToString(),
                    TitleOfCourtesy = row["TitleOfCourtesy"]?.ToString(),
                    BirthDate = (row["BirthDate"] as DateTime?),

                    HireDate = (row["HireDate"] as DateTime?),
                    Address = row["Address"]?.ToString(),
                    City = row["City"]?.ToString(),
                    Region = row["Region"]?.ToString(),
                    PostalCode = row["PostalCode"]?.ToString(),
                    Country = row["Country"]?.ToString(),
                    HomePhone = row["HomePhone"]?.ToString(),
                    Extension = row["Extension"]?.ToString(),
                    ReportsTo = (row["ReportsTo"] as int?),
                    PhotoPath = row["PhotoPath"]?.ToString(),

                };
                if (employee.ReportsTo is not null)
                {
                    employee.ReportsToEmployee = this.GetById(employee.ReportsTo.ToString());
                }
                return employee;
            }
            return null;
        }

        public Employee Create(Employee employee)
        {


            string commandString = $"Insert into Employees ([LastName],[FirstName],[Title],[BirthDate]) values ( '{employee.LastName}' , '{employee.FirstName}', '{employee.Title}', '{employee.BirthDate.Value.ToString("yyyy-MMM-dd")}')";
            Console.WriteLine(commandString);
            var connection = new SqlConnection(_connectionString);
            var command = new SqlCommand(commandString, connection);

            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            var a = command.ExecuteNonQuery();
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
            
            return a > 0 ? employee : new();
        }
    }
}