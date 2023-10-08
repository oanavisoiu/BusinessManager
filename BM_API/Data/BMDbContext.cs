using BM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Data
{
    public class BMDbContext : DbContext
    {
        public BMDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}
