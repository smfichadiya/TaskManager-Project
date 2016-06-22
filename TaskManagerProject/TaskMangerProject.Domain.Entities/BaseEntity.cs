using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Entities
{
    public class BaseEntity
    {
        public virtual int ID { get; set; }

        public virtual bool IsActive { get; set; }

        public virtual DateTime DateCreated { get; set; }
    }
}
