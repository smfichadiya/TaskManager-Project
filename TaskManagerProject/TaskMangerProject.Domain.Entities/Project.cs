using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerProject.Domain.Entities
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<MyTask> Tasks { get; set; }

        public virtual int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
