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
    public class TasksController : Controller
    {
        ITaskRepository _taskRepository = new TaskRepository();
        IProjectRepository _projectRepository = new ProjectRepository();
        IUserRepository _userRepository = new UserRepository();

        // GET: Tasks
        public ActionResult Index()
        {
            List<MyTask> tasks;

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {
                tasks = _taskRepository.GetAll();
            }
            else
            {
                var userId = User.Identity.GetUserId();
                tasks = _taskRepository.GetAllFromUser(userId);
            }

            ViewBag.ProjectId = new SelectList(_projectRepository.GetAll(), "ID", "Name");
            ViewBag.Users = _userRepository.GetAll();
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

        [HttpPost]
        public ActionResult CreateAsync(MyTask task)
        {         
                if (task.UserId == 0)
                    task.UserId = null;

            if (_taskRepository.Create(task))
                return Json("ok");



            return Json("notOk");
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

        [HttpPost]
        public ActionResult DeleteAsync(MyTask task)
        {
            if (_taskRepository.Delete(task.ID))
                return Json("ok");

            return Json("notOk");
        }

        public ActionResult Details(int? id)
        {
            var dbTask = _taskRepository.GetById((int)id);
            if (dbTask != null)
            {
                ViewBag.userLogged = _userRepository.GetByAppUserId(User.Identity.GetUserId());
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

        [HttpPost]
        public ActionResult PostComment(int taskId, int userId, string comment)
        {
            bool result = _taskRepository.AddComment(taskId, userId, comment);

            return RedirectToAction("Details", new { id = taskId });
        }

        [HttpGet]
        public ActionResult DeleteComment(int commentId,int taskId)
        {
            var comment = _taskRepository.GetCommentById(commentId,taskId);
            ViewBag.taskId = taskId;
            return View(comment);
        }

        [HttpPost]
        public ActionResult DeleteComment(string comment, int commentId, int taskId)
        {
            bool result = _taskRepository.DeleteComment(comment, commentId, taskId);

            return RedirectToAction("Details", new { id = taskId });
        }
    }
}