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
    public class UsersController : Controller
    {
        IUserRepository _userRepository = new UserRepository();
        // GET: Users
        public ActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }
    }
}