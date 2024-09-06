using Example.EF.Entities;
using Example.MVC.WebApp.Models;
using Example.MVC.WebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Example.MVC.WebApp.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<string> cities = ["Lahore", "Islamabad", "Sydney", "New York"];
            List<string> countries = ["Pakistan", "USA", "Australia"];
            var model = new EmployeeCreateModel
            {
                Cities = cities.Select(x => new SelectListItem(x,x)),
                Countries = countries.Select(x => new SelectListItem(x,x))
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(EmployeeCreateModel model)
        {
            if (!ModelState!.IsValid)
            {
                return RedirectToAction("Error", "Home");
            }
            var employee = new Employee
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Country = model.Country,
                City = model.City,
                BirthDate = model.BirthDate
            };

            if (_employeeService.Create(employee))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id is null)
            {
                return Json(new { status = "error", description = "id is null" });
            }

            var employee = _employeeService.GetById(id);
            if (employee != null)
            {
                _employeeService.Delete(employee);
                return Json(new {status = "success"});
            }
            return Json(new { status = "error", description = "employee does not exist" });
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            return ViewComponent("EmployeesList");
        }
    }
}