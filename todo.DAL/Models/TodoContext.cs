using Microsoft.EntityFrameworkCore
namespace todo.DAL.Models;

public class TodoContext : DbContext
{
    public DbSet<Task> Tasks { get; set; }

    public DbSet<Step> Steps { get; set; }

    public TodoContext(DbContextOptions options) : base(options)
    {

    }


}