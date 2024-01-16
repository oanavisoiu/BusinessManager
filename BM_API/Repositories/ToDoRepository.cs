using BM_API.Data;
using BM_API.Models;
using BM_API.Repositories.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Repositories
{
    public class ToDoRepository:Repository,IToDoRepository
    {
        private readonly BMDbContext _bmDbContext;
        public ToDoRepository(BMDbContext bmDbContext):base(bmDbContext)
        {
            _bmDbContext = bmDbContext;
        }
        public async Task<ICollection<ToDo>> GetToDosByCompanyIdAsync(Guid companyId)
        {
            return _bmDbContext.ToDos.Where(x => x.CompanyId.Equals(companyId)).ToList();
        }
        public async Task<ToDo> GetToDoByIdAsync(Guid id)
        {
            return await _bmDbContext.ToDos.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
        public async Task<ICollection<ToDo>> GetTodayToDosAsync(Guid companyId)
        {
            DateTime today = DateTime.Now;
            var day = today.Day;
            var month = today.Month;
            var year = today.Year;

            ICollection<ToDo> toDos = await GetToDosByCompanyIdAsync(companyId);

            return toDos.Where(x =>
                (x.StartDate.Date <= today.Date && x.EndDate.Date >= today.Date) ||

                (x.StartDate.Day == day && x.StartDate.Month == month && x.StartDate.Year == year) ||

                (x.EndDate.Day == day && x.EndDate.Month == month && x.EndDate.Year == year)
            ).ToList();
        }
        public async Task<ICollection<ToDo>> GetUpcomingToDosAsync(Guid companyId)
        {
            DateTime upcoming = DateTime.Now.AddDays(3);
            ICollection<ToDo> toDos = await GetToDosByCompanyIdAsync(companyId);
            return toDos.Where(x => (x.StartDate.Day > DateTime.Now.Day && x.StartDate.Day <= upcoming.Day && x.StartDate.Month == upcoming.Month && x.StartDate.Year == upcoming.Year)).ToList();
        }
    }
}
