using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TaskManagerProject.Domain.Interfaces;
using TaskManagerProject.Domain.RepositoryEF.Repositories;

namespace TaskManagerProjectApp.Controllers
{
    public class HomeController : Controller
    {
        IProjectRepository _projectRepository = new ProjectRepository();

        // GET: Home
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {            
                return RedirectToAction("AdminPanel");
            }
            else if(User.Identity.IsAuthenticated && User.IsInRole("User"))
            {            
                return RedirectToAction("UserPanel");
            }
            else if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("UnregisteredPanel");
            }
            return View();
        }

        [Authorize]
        public ActionResult UnregisteredPanel()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles = "Admin,User")]
        public ActionResult UserPanel()
        {
            var userId = User.Identity.GetUserId();
            var projects = _projectRepository.GetAllFromUser(userId);
            ViewBag.ProjectId = new SelectList(projects,"ID","Name");

            return View();
        }



    }
}