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
        MyDatabase db = new MyDatabase();
        ICustomerRepository _customerRepository = new CustomerRepository();

        public bool Create(Project project)
        {
            var dbCustomer = _customerRepository.GetById((int)project.CustomerId);

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
            var dbProject = GetById(id);

            if(dbProject != null)
            {
                dbProject.IsActive = false;
                db.SaveChanges();
                return true;
            }

            return false;
        }

        public List<Project> GetAll()
        {
            return db.Projects.ToList();
        }

        public List<Project> GetByCustomerId(int id)
        {
            var dbProjectsByCustomer = db.Projects.Where(p => p.CustomerId == id).ToList();

            return dbProjectsByCustomer;
        }

        public Project GetById(int id)
        {
            return db.Projects.FirstOrDefault(p => p.ID == id);
        }

        public bool Update(Project project)
        {
            var dbProject = GetById(project.ID);

            if(dbProject != null)
            {
                dbProject.IsActive = project.IsActive;
                dbProject.Name = project.Name;
                dbProject.CustomerId = project.CustomerId;

                db.SaveChanges();

                return true;
            }
            return false;
        }
    }
}
