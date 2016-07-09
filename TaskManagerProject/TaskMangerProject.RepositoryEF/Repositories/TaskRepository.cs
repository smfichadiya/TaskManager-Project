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
        MyDatabase db = new MyDatabase();
        IProjectRepository _projectRepsitory = new ProjectRepository();
        IUserRepository _userRepository = new UserRepository();

        public string MySecretPasswordRecovery()
        {
            return "hajan1";
        }

        public bool Create(MyTask task)
        {
            //in what project this task belongs?
            if (task.ProjectId <= 0)
                return false;

            task.DateCreated = DateTime.Now;
           

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

        public List<MyTask> GetAll()
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

        public MyTask GetById(int id)
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

        public bool Update(MyTask task)
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
            dbTask.status = task.status;
            dbTask.ProjectId = task.ProjectId;
            
            //commit changes to database
            db.SaveChanges();

            return true;
        }

        public bool ChangeStatusOfTask(int taskId, StatusOfTask newStatus)
        {
            var dbTask = GetById(taskId);
            dbTask.status = newStatus;

            db.SaveChanges();

            return true;
        }

        public List<MyTask> GetAllFromUser(string userId)
        {
            return db.Tasks.Where(t => t.User.AppUserId == userId).ToList();
        }

        public bool AddComment(int taskId, int userId, string comment)
        {
            string userEmail = _userRepository.GetById(userId).Email;

            TaskComment taskComment = new TaskComment
            {
                Comment = comment,
                TaskId = taskId,
                UserId = userId,
                userEmail = userEmail
            };

            db.Tasks.FirstOrDefault(t => t.ID == taskId).Comments.Add(taskComment);
            db.SaveChanges();

            return true;
        }

        public List<TaskComment> GetAllCommentsOfTask(int taskId)
        {
            throw new NotImplementedException();
        }

        public TaskComment GetCommentById(int commentId,int taskId)
        {
            return db.Tasks.FirstOrDefault(t => t.ID == taskId).Comments.FirstOrDefault(c => c.ID == commentId);
        }

        public bool DeleteComment(string comment, int commentId, int taskId)
        {
            var dbComment = GetCommentById(commentId, taskId);
            db.Tasks.FirstOrDefault(t => t.ID == taskId).Comments.Remove(dbComment);
            db.SaveChanges();

            return true;
        }
    }
}




