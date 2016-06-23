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
    public class TasksController : Controller
    {
        ITaskRepository _taskRepository = new TaskRepository();
        IProjectRepository _projectRepository = new ProjectRepository();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = _taskRepository.GetAll();
            return View(tasks);
        }

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_projectRepository.GetAll(), "ID", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            if (ModelState.IsValid)
            {
                if (_taskRepository.Create(task))
                    return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            ViewBag.ProjectId = new SelectList(_projectRepository.GetAll(), "ID", "Name");
            return View(_taskRepository.GetById(id));
        }

        [HttpPost]
        public ActionResult Edit(Task task)
        {
            if (ModelState.IsValid)
            {
                if (_taskRepository.Update(task))
                    return RedirectToAction("Index");
            }

            return View(task);
        }

        public ActionResult Delete(Task task)
        {
            return View(_taskRepository.GetById(task.ID));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_taskRepository.Delete(id))
                return RedirectToAction("Index");

            return View();
        }

        public ActionResult Details(int? id)
        {
            var dbTask = _taskRepository.GetById((int)id);
            if (dbTask != null)
            {
                return View(dbTask);
            }
            return RedirectToAction("Index");
        }
    }
}