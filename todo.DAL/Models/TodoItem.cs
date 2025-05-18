using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo.DAL.Models
{
    public class TodoItem
    {
        public int Id { get; set; }

        public int Title { get; set; }

        public string Description { get; set; }

        public DateTime deadline { get; set; }

        public Status Status { get; set; }


    }
}
