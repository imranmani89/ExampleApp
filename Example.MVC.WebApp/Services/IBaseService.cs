using System.Runtime.InteropServices;

namespace Example.MVC.WebApp.Services
{
    public interface IBaseService<T> where T : class
    {
        IEnumerable<T> GetAll();
        T? GetById(string id);
        bool Create(T entity);
        abstract bool Delete(T entity);

    }
}
