using Example.MVC.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Example.MVC.WebApp.ViewComponents
{
    [ViewComponent]
    public class EmployeesListViewComponent : ViewComponent
    {
        private readonly EmployeeService _service;
        
        public EmployeesListViewComponent(EmployeeService service)
        {
            _service = service;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _service.GetAll();
            return View(model);
        }
    }
}
