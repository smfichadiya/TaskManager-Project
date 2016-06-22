using TaskManagerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerProject.Domain.Interfaces
{
    public interface ITaskRepository : IGenericRepository<Task>
    {
        List<TaskComment> GetComments(int taskId);
        string GetAssigneeName(int taskId);
    }
}
