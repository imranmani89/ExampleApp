using Microsoft.AspNetCore.Mvc.Rendering;

namespace Example.MVC.WebApp.Models
{
    public class EmployeeCreateModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public IEnumerable<SelectListItem> Cities { get; set; }
        public IEnumerable<SelectListItem> Countries { get; set; }
    }
}
