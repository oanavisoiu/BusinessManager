using BM_API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BM_API.Data
{
    public class BMDbContext : IdentityDbContext<User>
    {
        public BMDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<CompanySupplier> CompanySuppliers { get; set; }
        public DbSet<Budget> Budgets { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<BudgetType> BudgetTypes { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
    }
}
