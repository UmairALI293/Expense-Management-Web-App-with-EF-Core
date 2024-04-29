using Microsoft.EntityFrameworkCore;

namespace TEK_Expense_Tracker.Models
{
    public class ExDbContext:DbContext
    {
        public ExDbContext(DbContextOptions<ExDbContext> options): base(options) { }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
