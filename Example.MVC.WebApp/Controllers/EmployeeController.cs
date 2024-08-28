using Example.EF.Entities;
using Example.MVC.WebApp.Models;
using Example.MVC.WebApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace Example.MVC.WebApp.Controllers
{
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
            var model = _employeeService.GetAll();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new EmployeeCreateModel();
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

            var employee = _employeeService.GetById(id);
            if (employee != null)
            {
                _employeeService.Delete(employee);
                return RedirectToAction("Index", new { Controller = "Employee", Area = "" });
            }
            return RedirectToAction("Error", new { Controller = "Home", Area = "" });
        }
    }
}
