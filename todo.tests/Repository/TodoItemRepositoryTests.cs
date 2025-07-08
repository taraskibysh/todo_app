using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.DAL.DbContexts;
using todo.DAL.Repositories.Realizations;
using todo.Models.Models;
using Xunit;
using Microsoft.EntityFrameworkCore.InMemory;


namespace todo.tests.Repository
{
    public class TodoItemRepositoryTests : IDisposable
    {
        private readonly TodoContext _context;
        private readonly TodoItemRepository _repository;

        public TodoItemRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new TodoContext(options);
            _repository = new TodoItemRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task CreateTodoItem_AddsItem()
        {
            var item = new TodoItem { Title = "Test", Description = "Description", Status = Status.Done};

            var result = await _repository.CreateTodoItem(item);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
            Assert.Single(_context.TodoItems);
        }

        [Fact]
        public async Task GetAllTodoItems_ReturnsAll()
        {
            _context.TodoItems.Add(new TodoItem { Title = "1", Description = "desc", Status = Status.Done });
            _context.TodoItems.Add(new TodoItem { Title = "2", Description = "desc", Status = Status.Done });
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllTodoItems();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetTodoItem_ReturnsCorrectItem()
        {
            var item = new TodoItem { Title = "Test", Description = "desc", Status = Status.InProgress };
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            var result = await _repository.GetTodoItem(item.Id);

            Assert.NotNull(result);
            Assert.Equal(item.Id, result.Id);
        }

        [Fact]
        public async Task GetTodoItem_ReturnsNull_IfNotFound()
        {
            var result = await _repository.GetTodoItem(999);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteById_RemovesItem()
        {
            var item = new TodoItem { Title = "To Delete", Description = "desc", Status = Status.Done };
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            await _repository.DeleteById(item.Id);

            Assert.Empty(_context.TodoItems);
        }

        [Fact]
        public async Task DeleteById_DoesNothing_IfNotFound()
        {
            var before = _context.TodoItems.Count();
            await _repository.DeleteById(12345);
            var after = _context.TodoItems.Count();
            Assert.Equal(before, after);
        }

        [Fact]
        public async Task UpdateTodoItem_UpdatesCorrectly()
        {
            var item = new TodoItem { Title = "Before", Description = "desc", Status = Status.Done };
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            item.Title = "After";
            var result = await _repository.UpdateTodoItem(item);

            Assert.Equal("After", result.Title);
        }

        [Fact]
        public async Task GetByStatus_ReturnsCorrectItem()
        {
            var item = new TodoItem { Title = "Done", Description = "desc", Status = Status.Done };
            _context.TodoItems.Add(item);
            await _context.SaveChangesAsync();

            var result = await _repository.GetByStatus(Status.Done);

            Assert.NotNull(result);
            Assert.Equal(Status.Done, result.Status);
        }
    }
}
