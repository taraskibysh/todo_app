using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo.Models.Models
{
    public class TodoItem
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        [MaxLength(200)]
        public string? Description { get; set; }


        public DateTime? Deadline { get; set; }

        public Status Status { get; set; }

        public ICollection<Step> Steps { get; set; } = new List<Step>();


    }
}
