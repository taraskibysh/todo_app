using Microsoft.EntityFrameworkCore;
using todo.Models.Models;

namespace todo.DAL.DbContexts;

public class TodoContext : DbContext
{
    public DbSet<TodoItem> TodoItems { get; set; }

    public DbSet<Step> Steps { get; set; }

    public TodoContext(DbContextOptions options) : base(options)
    {

    }


}