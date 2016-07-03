using TaskManagerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Interfaces
{
    public interface IUserRepository : IGenericRepository<MyUser>
    {
        MyUser GetByAppUserId(String id);

        MyUser Update(int ID, List<int> taskIds);
    }
}
