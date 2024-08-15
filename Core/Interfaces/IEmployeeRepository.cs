
using Core.Entities;

namespace Core.Interfaces;
public interface IEmployeeRepository
{
    List<Employee> GetData();
    Employee? GetById(string id);

    Employee Create(Employee employee);
}