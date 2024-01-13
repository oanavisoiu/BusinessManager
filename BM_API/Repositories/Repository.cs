using BM_API.Data;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class Repository: IRepository
    {
        protected readonly BMDbContext _context;
        public Repository(BMDbContext context)
        {
            _context = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Attach(entity);
            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public async Task<bool> SaveChangesAsync()
        {

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
