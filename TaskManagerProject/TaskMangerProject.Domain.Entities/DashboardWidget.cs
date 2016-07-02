using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Entities
{
    public class DashboardWidget : BaseEntity
    {
        public int Position { get; set; }
        public WidgetSize Size { get; set; }
        public string DisplayName { get; set; }
        public bool IsVisible { get; set; }

        public virtual int UserId { get; set; }
        public virtual MyUser User { get; set; }
    }

    public enum WidgetSize
    {
        Small = 0,
        Medium = 1,
        Big = 2
    }
}
