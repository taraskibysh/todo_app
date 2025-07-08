using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using todo.DAL.DbContexts;
using todo.DAL.Repositories.Realizations;
using todo.Models.Models;

namespace todo.tests.Repository
{
    public class StepRepositoryTests
    {

        private readonly TodoContext _context;
        private readonly StepRepository _repository;

        public StepRepositoryTests()
        {

            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;


            _context = new TodoContext(options);
            _repository = new StepRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }


        public async Task DeleteStep_Valid_RemovesStep()
        {
            var item = new Step {Id = 1, IsDone = false, Title = "Title", TodoItemId = 1 };
            _context.Steps.Add(item);

            await _context.SaveChangesAsync();

            await _repository.DeleteStep(1);

            Assert.Empty(_context.Steps);


        }
        [Fact]
        public async Task DeleteStep_Invalid_DoesNothing()
        {
            await _repository.DeleteStep(999); 
            Assert.Empty(_context.Steps);     
        }

        [Fact]
        public async Task GetAllSteps_ReturnsStepsForTodoItem()
        {
            int todoId = 1;
            var todo = new TodoItem { Title = "title", Id = todoId, Status = Status.Done };
            var steps = new List<Step>
            {
                new Step { Id = 1, Title = "Step 1", TodoItemId = todoId, TodoItem = todo},
                new Step { Id = 2, Title = "Step 2", TodoItemId = todoId, TodoItem = todo},
                new Step { Id = 3, Title = "Step 3", TodoItemId = 2} 
            };
            _context.Steps.AddRange(steps);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAllSteps(todoId);

            Assert.Equal(2, result.Count());
            Assert.All(result, s => Assert.Equal(todoId, s.TodoItemId));
        }

        [Fact]
        public async Task GetStepById_ReturnsCorrectStep()
        {

            var todo = new TodoItem
            {
                Id = 1, Title = "todo", Status = Status.Done
            };

            _context.TodoItems.Add(todo);

            var step = new Step { Title = "Get Me", IsDone = true, TodoItemId = 1, TodoItem = todo};
            _context.Steps.Add(step);
            
            await _context.SaveChangesAsync();

            var result = await _repository.GetStepById(1, 1);

            Assert.NotNull(result);
            Assert.Equal(step.Title, result.Title);
        }

        [Fact]
        public async Task GetStepById_ReturnsNull_IfNotMatch()
        {
            var step = new Step { Id = 1, Title = "Mismatch", TodoItemId = 2 };
            _context.Steps.Add(step);
            await _context.SaveChangesAsync();

            var result = await _repository.GetStepById(1, 1);

            Assert.Null(result);
        }

        [Fact]
        public async Task AddStep_AddsToDbAndReturnsStep()
        {
            var todo = new TodoItem { Id = 1, Title = "Todo", Description = "desc", Status = Status.Todo };
            _context.TodoItems.Add(todo);
            await _context.SaveChangesAsync();

            var step = new Step { Title = "New Step", TodoItemId = todo.Id };

            var result = await _repository.AddStep(step);

            Assert.NotNull(result);
            Assert.Equal("New Step", result.Title);
            Assert.Single(_context.Steps);
        }
    }
}
