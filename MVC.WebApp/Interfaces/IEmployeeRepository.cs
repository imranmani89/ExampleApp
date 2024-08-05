
using MVC.WebApp.Entities;

namespace MVC.WebApp.Interfaces;
public interface IEmployeeRepository
{
    List<Employee> GetData();
    Employee? GetById(string id);
}