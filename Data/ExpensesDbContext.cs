using Expenses.Models;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Data;

public class ExpensesDbContext : DbContext
{
    public ExpensesDbContext(DbContextOptions<ExpensesDbContext> options) : base(options)
    {
    }

    public DbSet<Expense> Expenses { get; set; }
}
