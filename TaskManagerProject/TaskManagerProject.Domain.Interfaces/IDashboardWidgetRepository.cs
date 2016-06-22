using TaskManagerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Interfaces
{
    public interface IDashboardWidgetRepository : IGenericRepository<DashboardWidget>
    { 
        IQueryable<DashboardWidget> GetAll(int userId);
        DashboardWidget GetById(int userId, int id);
        DashboardWidget GetByPosition(int userId, int position);

        bool Update(int userId, DashboardWidget widget);
        bool SetVisibility(int userId, DashboardWidget widget);
    }
}
