using TaskManagerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerProject.Domain.Interfaces
{
    public interface ITaskRepository : IGenericRepository<MyTask>
    {
        List<TaskComment> GetComments(int taskId);
        string GetAssigneeName(int taskId);
        bool ChangeStatusOfTask(int taskId, StatusOfTask newStatus);
    }
}
