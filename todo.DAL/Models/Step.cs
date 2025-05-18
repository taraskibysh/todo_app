using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo.DAL.Models
{
    public class Step
    {
        public int Id { get;}
        public string title { get; set; }
        public bool isDone { get; set; }
    }
}
