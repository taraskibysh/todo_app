using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace todo.DAL.Models
{
    public class Task
    {
        public int id { get; }

        public int title { get; set; }

        public string description { get; set; }

        public Status status { get; set; }


    }
}
