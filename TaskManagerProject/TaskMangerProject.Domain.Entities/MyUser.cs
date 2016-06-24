using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Entities
{
    public class MyUser : BaseEntity
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string DisplayName { get; set; }

        public virtual ICollection<MyTask> Tasks { get; set; }
    }
}
