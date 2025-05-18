using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using todo.DAL.Models;

namespace todo.Contracts.DTOs.TodoItemDTOs
{
    public class TodoItemResponse
    {
        public int Id { get; set; }

        public int Title { get; set; }

        public string Description { get; set; }

        public StatusDTO.StatusDTO Status { get; set; }
    }
}
