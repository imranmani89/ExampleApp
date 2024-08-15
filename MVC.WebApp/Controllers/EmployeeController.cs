using Microsoft.AspNetCore.Mvc;
using Core.Entities;
using Core.Interfaces;


namespace MVC.WebApp.Controllers
{


    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repository.GetData());
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
    }
}
