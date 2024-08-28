using Example.EF.DbContexts;
using Example.EF.Entities;

namespace Example.MVC.WebApp.Services
{
    public class EmployeeService: BaseService<Employee>
    {
        public EmployeeService(ExampleDbContext context):base(context) {
            
        }

        public override Employee? GetById(string id)
        {
            var employee = _context.Employees.ToList();
            
            return employee.FirstOrDefault(x => x.EmployeeId.ToString() == id);

        }

        public IEnumerable<Employee> GetByName(string name) {
            return _context.Employees.Where(x => x.FirstName.Contains(name) || x.LastName.Contains(name)).ToList();
        }

    }
}
