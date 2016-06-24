
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerProject.Domain.Interfaces;
using TaskManagerProject.Domain.Entities;

namespace TaskManagerProject.Domain.RepositoryEF.Repositories
{
    public class UserRepository : IUserRepository
    {
        MyDatabase db = new MyDatabase();

        public bool Create(MyUser obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<MyUser> GetAll()
        {
            return db.MyUsers.Where(u => u.IsActive).ToList();
        }

        public MyUser GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(MyUser obj)
        {
            throw new NotImplementedException();
        }
    }
}
