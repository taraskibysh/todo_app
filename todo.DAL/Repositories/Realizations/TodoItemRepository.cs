using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Server;
using todo.DAL.DbContexts;
using todo.Models.Models;
using todo.DAL.Repositories.Interfaces;

namespace todo.DAL.Repositories.Realizations
{
    public class TodoItemRepository : ITodoItemRepository
    {
        private readonly TodoContext _dbContext;
        public TodoItemRepository(TodoContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<IEnumerable<TodoItem?>> GetAllTodoItems()
        {
            return await _dbContext.TodoItems
                .Include(t => t.Steps)
                .ToListAsync();

        }

        public async Task<TodoItem?> GetTodoItem(int id)
        {
            return await _dbContext.TodoItems
                .Include(t => t.Steps).
                FirstOrDefaultAsync(t => t.Id == id);

        }

        public async Task<TodoItem?> UpdateTodoItem(TodoItem item)
        {
            _dbContext.TodoItems.Update(item);
            await _dbContext.SaveChangesAsync();

            return await _dbContext.TodoItems.FindAsync(item.Id);

        }

        public async Task DeleteById(int id)
        {
            var item = await _dbContext.TodoItems.FindAsync(id);
            if (item != null)
            {
                _dbContext.TodoItems.Remove(item);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<TodoItem> CreateTodoItem(TodoItem item)
        {
            var entry =await _dbContext.TodoItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return entry.Entity;
        }


        public Task<TodoItem?> GetByStatus(Status status)
        {
           return _dbContext.TodoItems.FirstOrDefaultAsync(item  => item.Status == status);
        }
    }
}
