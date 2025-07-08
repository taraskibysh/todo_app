using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.Models.Models;

namespace todo.Contracts.DTOs.TodoItemDTOs
{
    public record TodoItemRequest
    {
        public string Title { get; init; }
        public string? Description { get; init; }
        public bool IsDone { get; init; }
        public DateTime? Deadline { get; init; }
        public Status Status { get; init; }

        public TodoItemRequest()
        {

        }
    }
}
