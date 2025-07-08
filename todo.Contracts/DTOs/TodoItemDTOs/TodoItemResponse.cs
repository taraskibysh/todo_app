using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.Contracts.DTOs.StepDTOs;
using todo.Models.Models;

namespace todo.Contracts.DTOs.TodoItemDTOs
{
    public record TodoItemResponse
    {
        public int Id { get; init; }
        public string Title { get; init; }
        public string? Description { get; init; }
        public bool IsDone { get; init; }
        public DateTime? Deadline { get; init; }
        public Status Status { get; init; }

        public ICollection<StepResponse> Steps { get; set; }
        public TodoItemResponse()
        {
        }
    }

}
