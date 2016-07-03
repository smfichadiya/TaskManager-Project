
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

            db.MyUsers.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Create(MyUser obj, String AppUserId)
        {
            obj.DateCreated = DateTime.Now;
            obj.IsActive = false;
            obj.AppUserId = AppUserId;

            db.MyUsers.Add(obj);
            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<MyUser> GetAll()
        {
            return db.MyUsers.ToList();
        }

        public MyUser GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public MyUser GetByAppUserId(string id)
        {
            return db.MyUsers.FirstOrDefault(u => u.AppUserId == id);        
        }

        public bool Update(MyUser obj)
        {
            var dbObj = GetById(obj.ID);
            dbObj.IsActive = obj.IsActive;

            db.SaveChanges();
            return true;
        }

        public MyUser Update(int ID, List<int> taskIds)
        {
            var dbUser = GetById(ID);
           
            foreach(var task in dbUser.Tasks)
            {
                db.Tasks.FirstOrDefault(t => t.ID == task.ID).UserId = null;
            }
            db.SaveChanges();


            if (taskIds == null)
            {
                dbUser.Tasks = null;
            }            
            else
            {
                var tasks = db.Tasks.Where(t => taskIds.Contains(t.ID)).ToList();
                dbUser.Tasks = tasks;
            }
           
            db.SaveChanges();

            return dbUser;
        }
    }
}
