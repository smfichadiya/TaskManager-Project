using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerProject.Domain.Interfaces;
using TaskManagerProject.Domain.Entities;

namespace TaskManagerProject.Domain.RepositoryEF.Repositories
{ 
    public class TaskRepository : ITaskRepository
    {
        Database db = new Database();
        IProjectRepository _projectRepsitory = new ProjectRepository();
        IUserRepository _userRepository = new UserRepository();

        public string MySecretPasswordRecovery()
        {
            return "hajan1";
        }

        public bool Create(Task task)
        {
            //in what project this task belongs?
            if (task.ProjectId <= 0)
                return false;

            var dbProject = _projectRepsitory.GetById(task.ProjectId);

            if (dbProject == null)
                return false;
            else
                task.Project = dbProject;

            //user assigned check
            if (task.UserId <= 0)
                return false;

            var dbUser = _userRepository.GetById(task.UserId);

            if (dbUser == null)
                return false;
            else
                task.User = dbUser;

            //adding the task to DB collection
            db.Tasks.Add(task);

            //save the task
            db.SaveChanges();

            return true;
        }

        public bool Delete(int id)
        {
            var dbTask = GetById(id);

            if (dbTask == null)
                return false;

            db.Tasks.Remove(dbTask);
            db.SaveChanges();

            return true;
        }

        public List<Task> GetAll()
        {
            return db.Tasks.ToList();
        }

        public string GetAssigneeName(int taskId)
        {
            var dbTask = GetById(taskId);

            if (dbTask == null)
                return string.Empty;

            return dbTask.User.DisplayName + "(" + dbTask.User.Email + ")";
        }

        public Task GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public List<TaskComment> GetComments(int taskId)
        {
            var dbTask = GetById(taskId);

            if (dbTask != null)
                return dbTask.Comments.ToList();
            else
                return new List<TaskComment>();
        }

        public bool Update(Task task)
        {
            var dbTask = GetById(task.ID);

            if (dbTask == null)
                return false;

            //all data columns we want to update
            dbTask.Description = task.Description;
            dbTask.Name = task.Name;
            dbTask.Type = task.Type;
            dbTask.StartDateTime = task.StartDateTime;
            dbTask.EndDateTime = task.EndDateTime;

            //commit changes to database
            db.SaveChanges();

            return true;
        }
    }
}
