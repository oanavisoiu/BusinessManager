using BM_API.Models;

namespace BM_API.Repositories.RepositoryInterfaces
{
    public interface IToDoRepository:IRepository
    {
        Task<ICollection<ToDo>> GetToDosByCompanyIdAsync(Guid companyId);
        Task<ICollection<ToDo>> GetTodayToDosAsync(Guid companyId);
        Task<ToDo> GetToDoByIdAsync(Guid id);
        Task<ICollection<ToDo>> GetUpcomingToDosAsync(Guid companyId);
    }
}
