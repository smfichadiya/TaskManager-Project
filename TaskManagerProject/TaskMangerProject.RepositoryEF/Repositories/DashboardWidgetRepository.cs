using TaskManagerProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerProject.Domain.Entities;

namespace TaskManagerProject.Domain.RepositoryEF.Repositories
{
    public class DashboardWidgetRepository : IDashboardWidgetRepository
    {
        Database db = new Database();
        
        public IQueryable<DashboardWidget> GetAll(int userId)
        {
            return db.DashboardWidgets.Where(x => x.UserId == userId).OrderBy(x => x.Position);
        }

        public DashboardWidget GetById(int userId, int id)
        {
            return GetAll(userId).FirstOrDefault(x => x.ID == id);
        }

        public bool SetVisibility(int userId, DashboardWidget widget)
        {
            var dbWidget = GetById(userId, widget.ID);
            dbWidget.IsVisible = widget.IsVisible;
            db.SaveChanges();

            return true;
        }

        public bool Update(int userId, DashboardWidget widget)
        {
            var dbWidget = GetById(userId, widget.ID);
            dbWidget.DisplayName = widget.DisplayName;
            dbWidget.Position = widget.Position;
            dbWidget.Size = widget.Size;

            db.SaveChanges();
            return true;
        }

        public DashboardWidget GetByPosition(int userId, int position)
        {
            return GetAll(userId).FirstOrDefault(x => x.Position == position);
        }

        public List<DashboardWidget> GetAll()
        {
            throw new NotImplementedException();
        }

        public DashboardWidget GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Create(DashboardWidget obj)
        {
            throw new NotImplementedException();
        }

        public bool Update(DashboardWidget obj)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
