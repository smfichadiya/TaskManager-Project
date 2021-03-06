﻿using TaskManagerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Interfaces
{
    public interface IProjectRepository : IGenericRepository<Project>
    {
        List<Project> GetByCustomerId(int ID);
        List<Project> GetAllFromUser(string userId);
    }
}
