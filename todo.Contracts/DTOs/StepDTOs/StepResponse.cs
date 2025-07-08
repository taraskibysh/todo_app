using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.Contracts.DTOs.TodoItemDTOs;

namespace todo.Contracts.DTOs.StepDTOs
{
    public record StepResponse
    {
        public int id { get; init; }
        public string Title { get; init; }

        public bool IsDone { get; init; }

        public int TodoItemId { get; set; }

    }
}
