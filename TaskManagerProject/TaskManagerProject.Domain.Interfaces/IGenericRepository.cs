using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerProject.Domain.Interfaces
{
   public interface IGenericRepository<T>
    {
        List<T> GetAll();
        T GetById(int id);

        bool Create(T obj);
        bool Update(T obj);
        bool Delete(int id);
    }
}
