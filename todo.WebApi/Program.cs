using Microsoft.EntityFrameworkCore;
using todo.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlServer("server=DESKTOP-Q2PGU9V;Database=todo_db;Trusted_Connection=True;TrustServerCertificate=True;");
});
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
