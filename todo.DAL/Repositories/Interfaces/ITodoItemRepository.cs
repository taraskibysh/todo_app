using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.Models.Models;


namespace todo.DAL.Repositories.Interfaces
{
    public interface ITodoItemRepository
    {
        public Task<IEnumerable<TodoItem?>> GetAllTodoItems();

        public Task<TodoItem?> GetTodoItem(int id);

        public Task<TodoItem?> UpdateTodoItem(TodoItem item);

        public Task DeleteById(int id);

        public Task<TodoItem> CreateTodoItem(TodoItem item);

        public Task<TodoItem?> GetByStatus(Status status);
    }

}
