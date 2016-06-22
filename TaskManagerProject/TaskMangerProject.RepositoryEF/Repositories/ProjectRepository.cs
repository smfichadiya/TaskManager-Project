using TaskManagerProject.Domain.Interfaces;
using TaskManagerProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TaskManagerProject.Domain.RepositoryEF.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        Database db = new Database();
        ICustomerRepository _customerRepository = new CustomerRepository();

        public bool Create(Project project)
        {
            var dbCustomer = _customerRepository.GetById(project.CustomerId);

            if (dbCustomer != null)
            {
                project.DateCreated = DateTime.Now;
                project.IsActive = true;

                db.Projects.Add(project);
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<Project> GetAll()
        {
            return db.Projects.ToList();
        }

        public Project GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Project project)
        {
            throw new NotImplementedException();
        }
    }
}
