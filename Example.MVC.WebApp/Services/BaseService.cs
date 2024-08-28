
using Example.EF.DbContexts;
using Example.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example.MVC.WebApp.Services
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        protected readonly ExampleDbContext _context;

        protected BaseService(ExampleDbContext context)
        {
            _context = context;
        }

        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return _context.SaveChanges() > 0;
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return _context.SaveChanges() > 0;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList<T>();

        }

        public abstract T? GetById(string id);
    }
}
