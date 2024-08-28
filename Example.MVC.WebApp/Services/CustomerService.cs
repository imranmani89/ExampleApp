using Example.EF.DbContexts;
using Example.EF.Entities;

namespace Example.MVC.WebApp.Services
{
    public class CustomerService: BaseService<Customer>
    {

        public CustomerService(ExampleDbContext context): base(context) {
        }

        public override Customer? GetById(string id) { 
            return _context.Customers.SingleOrDefault(x => x.CustomerId == id);
        }

        public IEnumerable<Customer> GetByName(string name) {
            return _context.Customers.Where(x => x.CompanyName.Contains(name) || x.ContactTitle.Contains(name)).ToList();
        }

    }
}
