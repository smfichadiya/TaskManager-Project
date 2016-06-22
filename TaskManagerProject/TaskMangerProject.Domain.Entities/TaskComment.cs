using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Entities
{
    public class TaskComment
    {
        public int ID { get; set; }
        public string Comment { get; set; }

        public int? UserId { get; set; }
        //public virtual User User { get; set; }

        public virtual int TaskId { get; set; }
        public virtual Task Task { get; set; }
    }
}
