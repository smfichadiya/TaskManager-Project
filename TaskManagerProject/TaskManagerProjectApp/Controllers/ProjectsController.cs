using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagerProject.Domain.Entities;
using TaskManagerProject.Domain.Interfaces;
using TaskManagerProject.Domain.RepositoryEF.Repositories;

namespace TaskManagerProjectApp.Controllers
{
    public class ProjectsController : Controller
    {
        IProjectRepository _projectRepository = new ProjectRepository();
        ICustomerRepository _customerRepository = new CustomerRepository();

        // GET: Projects
        public ActionResult Index()
        {
            var projects = _projectRepository.GetAll();

            return View(projects);
        }

        public ActionResult Create()
        {
            //usseless
            ViewBag.Customers = _customerRepository.GetAll();

            ViewBag.CustomerId = new SelectList(_customerRepository.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                if (_projectRepository.Create(project))
                    return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Delete(Project project)
        {
            return View(_projectRepository.GetById(project.ID));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_projectRepository.Delete(id))
            {
                return RedirectToAction("Index"); 
            }
            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.CustomerId = new SelectList(_customerRepository.GetAll(), "ID", "Name");
            return View(_projectRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Project project)
        {
            if (ModelState.IsValid)
            {
                if (_projectRepository.Update(project))
                    return RedirectToAction("Index");
                
            }
            return View(project);
         
        }

        public ActionResult Details(int? id)
        {
            var dbProject = _projectRepository.GetById((int)id);
            if(dbProject != null)
            {
                return View(dbProject);
            }
            return RedirectToAction("Index");

        }
    }
}