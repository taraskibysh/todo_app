using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo.Models.Models
{
    public class Step
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string? Title { get; set; }
        public bool IsDone { get; set; }

        public int TodoItemId { get; set; }
        public TodoItem TodoItem { get; set; }
    }
}
