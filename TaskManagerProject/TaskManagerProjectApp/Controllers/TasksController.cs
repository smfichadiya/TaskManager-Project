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
    public class TasksController : Controller
    {
        ITaskRepository _taskRepository = new TaskRepository();
        IProjectRepository _projectRepository = new ProjectRepository();
        IUserRepository _userRepository = new UserRepository();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = _taskRepository.GetAll();
            return View(tasks);
        }

        public ActionResult Create()
        {
            ViewBag.ProjectId = new SelectList(_projectRepository.GetAll(), "ID", "Name");
            ViewBag.Users = _userRepository.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Create(MyTask task)
        {
            if (ModelState.IsValid)
            {
                if (task.UserId == 0)
                    task.UserId = null;

                if (_taskRepository.Create(task))
                    return RedirectToAction("Index");
            }
            ViewBag.ProjectId = new SelectList(_projectRepository.GetAll(), "ID", "Name");
            ViewBag.Users = _userRepository.GetAll();

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.ProjectId = new SelectList(_projectRepository.GetAll(), "ID", "Name");
            ViewBag.UserId = new SelectList(_userRepository.GetAll(), "ID", "DisplayName");
            return View(_taskRepository.GetById(id));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(MyTask task)
        {
            if (ModelState.IsValid)
            {
                if (_taskRepository.Update(task))
                    return RedirectToAction("Index");
            }

            return View(task);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Delete(MyTask task)
        {
            return View(_taskRepository.GetById(task.ID));
        }


        [Authorize(Roles = "Admin")]
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

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public ActionResult ChangeStatusOfTask(ChangeStatusViewModel model)
        {
            bool result = _taskRepository.ChangeStatusOfTask(model.TaskId, model.newStatus);

            return result == true ? Json("true") : Json("false");
        }
    }
}