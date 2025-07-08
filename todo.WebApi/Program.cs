using Microsoft.EntityFrameworkCore;
using todo.BLL.Mappers;
using todo.BLL.Services.Interfaces;
using todo.BLL.Services.Realizations;
using todo.DAL.DbContexts;
using todo.DAL.Repositories.Interfaces;
using todo.DAL.Repositories.Realizations;
using todo.Models.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<TodoContext>(options =>
{
    options.UseSqlServer("server=DESKTOP-Q2PGU9V;Database=todo_db;Trusted_Connection=True;TrustServerCertificate=True;");
});
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddAutoMapper(typeof(StepMapper).Assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITodoItemService, TodoItemService>();
builder.Services.AddScoped<IStepService, StepService>();
builder.Services.AddScoped<ITodoItemRepository, TodoItemRepository>();
builder.Services.AddScoped<IStepRepository, StepRepository>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors();

app.Run();
