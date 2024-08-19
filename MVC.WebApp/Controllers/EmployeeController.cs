using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;
using MVC.WebApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Example.EF.DbContexts;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.EntityFrameworkCore;


namespace MVC.WebApp.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;
        private readonly ExampleDbContext _context;

        public EmployeeController(IEmployeeRepository repository, ExampleDbContext context)
        {
            _repository = repository;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetData());
        }

        [HttpGet("GetAllByEF")]
        public IActionResult GetbyEF()
        {
            var result = _context
                            .Customers
                            .Include(c => c.Orders)
                            .ToList();
            return Ok(result);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var employee = _repository.GetById(id);
            if (employee is not null)
            {
                return Ok(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Create([FromBody]EmployeeUpsertDto employeedto)
        {
            var employee = new Employee
            {
                FirstName = employeedto.FirstName,
                LastName = employeedto.LastName,
                BirthDate = DateTime.Parse(employeedto.BirthDate),
                Title = employeedto.Title,
            };

            var resultemployee = _repository.Create(employee);

            if (resultemployee is not null)
            {
                return Created($"baseurl", resultemployee.EmployeeID);
            }
            return BadRequest();
        }

    }
}
