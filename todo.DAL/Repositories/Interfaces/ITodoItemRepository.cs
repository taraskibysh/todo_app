using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.DAL.Models;


namespace todo.DAL.Repositories.Interfaces
{
    internal interface ITodoItemRepository
    {
        public Task<IEnumerable<TodoItem?>> GetAllTodoItems();

        public Task<TodoItem?> GetTodoItem(int id);

        public Task<TodoItem?> UpdateTodoItem(TodoItem item);

        public Task DeleteById(int id);

        public Task<TodoItem> AddTodoItem(TodoItem item);

        public Task<TodoItem?> GetByStatus(Status status);
    }

}
