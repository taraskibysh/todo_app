using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.Contracts.DTOs.TodoItemDTOs;

namespace todo.BLL.Services.Interfaces
{
    public interface ITodoItemService
    {
        public Task<IEnumerable<TodoItemResponse>> GetAll();

        public Task<TodoItemResponse?> GetTodoItem(int id);

        public Task DeleteTodoItem(int id);

        public Task<TodoItemResponse> CreateTodoItem(TodoItemRequest item);

        public Task<TodoItemResponse> UpdateTodoItem(int id, TodoItemRequest item);

    }
}
