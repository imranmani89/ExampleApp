using Microsoft.AspNetCore.Mvc;
using Example.EF.Entities;
using MVC.WebApp.Models;
using Example.EF.DbContexts;
using Microsoft.EntityFrameworkCore;


namespace MVC.WebApp.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly ExampleDbContext _context;

        public EmployeeController(ExampleDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _context
                            .Employees
                            .OrderBy(x => x.LastName)
                            .ThenBy(x => x.FirstName)
                            .ToListAsync();

            return Ok(result);
        }


        [HttpGet]
        [Route("{id}")]
        public IActionResult GetById(string id)
        {
            var employee = _context.Employees.FirstOrDefault(x => x.EmployeeId.ToString() == id);
            if (employee is not null)
            {
                return Ok(employee);
            }

            return NotFound();
        }

        [HttpPost()]
        public IActionResult Create([FromBody] EmployeeUpsertDto employeedto)
        {
            var employee = new Employee
            {
                FirstName = employeedto.FirstName,
                LastName = employeedto.LastName,
                BirthDate = DateTime.Parse(employeedto.BirthDate),
                Title = employeedto.Title,
            };

            var resultemployee = _context.Employees.Add(employee);

            _context.SaveChanges();

            if (resultemployee is not null)
            {
                return Created($"baseurl", resultemployee.Entity.EmployeeId);
            }
            return BadRequest();
        }

    }
}