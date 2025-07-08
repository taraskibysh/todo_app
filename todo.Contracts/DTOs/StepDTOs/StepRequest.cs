using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.Models.Models;

namespace todo.Contracts.DTOs.StepDTOs
{
    public record StepRequest
    {
        public String title { get; init; }

        public bool IsDone { get; init; }
    }
}
