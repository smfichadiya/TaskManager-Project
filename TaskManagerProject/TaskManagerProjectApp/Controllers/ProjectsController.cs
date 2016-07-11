using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagerProject.Domain.Entities;
using TaskManagerProject.Domain.Interfaces;
using TaskManagerProject.Domain.RepositoryEF.Repositories;
using TaskManagerProjectApp.Models;

namespace TaskManagerProjectApp.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ProjectsController : Controller
    {
        IProjectRepository _projectRepository = new ProjectRepository();
        ICustomerRepository _customerRepository = new CustomerRepository();

        // GET: Projects
        [Authorize(Roles ="Admin,User")]
        public ActionResult Index()
        {
            List<Project> projects;

            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                 projects = _projectRepository.GetAll();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                projects = _projectRepository.GetAllFromUser(userId);
            }
          
            return View(projects);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            //usseless
            ViewBag.Customers = _customerRepository.GetAll();

            ViewBag.CustomerId = new SelectList(_customerRepository.GetAll(), "ID", "Name");
            return View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateAsync(Project project)
        {
            if (ModelState.IsValid)
            {
                if (_projectRepository.Create(project))
                    return Json("ok");
            }

            return Json("Bad call");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Project project)
        {
            return View(_projectRepository.GetById(project.ID));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_projectRepository.Delete(id))
            {
                return RedirectToAction("Index"); 
            }
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.CustomerId = new SelectList(_customerRepository.GetAll(), "ID", "Name");
            return View(_projectRepository.GetById(id));
        }

        [Authorize(Roles = "Admin")]
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

        public ActionResult TaskDashboard(int id)
        {
            var dbProject = _projectRepository.GetById(id);

            return View(dbProject);
        }

        public JsonResult GetProjectsByPage(int page,int size)
        {


            var projects = new List<Project>();

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                projects = _projectRepository.GetAll();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                projects = _projectRepository.GetAllFromUser(userId);
            }

            if(projects == null)
            {
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            int startIndex = (page - 1) * size;

            var totalProjects = projects.Count;

            if (page * size > totalProjects)
                size = totalProjects - startIndex;

            if(totalProjects < size)           
                size = totalProjects;
           
             projects = projects
                            .OrderByDescending(p => p.DateCreated)
                            .ToList()
                            .GetRange(startIndex,size);

            var result = new List<PaginationUserViewModel>();

            foreach(var item in projects)
            {
                result.Add(PaginationUserViewModel.CreateFromModel(item));
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }
    }
}