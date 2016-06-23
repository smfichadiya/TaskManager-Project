
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
        Database db = new Database();

        public bool Create(User obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            return db.Users.Where(u => u.IsActive).ToList();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(User obj)
        {
            throw new NotImplementedException();
        }
    }
}
