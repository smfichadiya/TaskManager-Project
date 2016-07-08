using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TaskManagerProjectApp.Models;
using TaskManagerProject.RepositoryEF;
using Microsoft.AspNet.Identity.EntityFramework;
using TaskManagerProject.Domain.Interfaces;
using TaskManagerProject.Domain.RepositoryEF.Repositories;
using TaskManagerProject.Domain.Entities;

namespace TaskManagerProjectApp.Controllers
{
    public class UsersController : Controller
    {
        IUserRepository _userRepository = new UserRepository();
        IProjectRepository _projectsRepository = new ProjectRepository();

        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManger;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManger ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManger = value;
            }
        }

       

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var users = _userRepository.GetAll();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Index(string userId)
        {
                var user = UserManager.Users.FirstOrDefault(u => u.Id == userId);

                //Adding Role USER         
                var role = RoleManager.FindByName("User");
                if (role == null)
                {
                    role = new IdentityRole("User");
                    RoleManager.Create(role);
                }

                if (!UserManager.IsInRole(user.Id, role.Name))
                {
                    UserManager.AddToRole(user.Id, role.Name);
                }

                user.EmailConfirmed = true;
                var result = UserManager.Update(user);

                var myUser = _userRepository.GetByAppUserId(user.Id);
                myUser.IsActive = true;
                _userRepository.Update(myUser);

                return Json("true");
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                  var myUser = new MyUser
                    {
                        AppUserId = user.Id,
                        DateCreated = DateTime.Now,
                        DisplayName = user.Email,
                        Email = user.Email,
                        IsActive = true
                    };
                    _userRepository.Create(myUser);

                    return RedirectToAction("Index");
                }
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var user = _userRepository.GetById(id);
            var projects = _projectsRepository.GetAll();
            ViewBag.projects = projects;
            return View(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditUserViewModel user)
        {
            var dbUser = _userRepository.Update(user.ID,user.TaskIds);

            return RedirectToAction("Edit");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(MyUser myUser)
        {
            var dbUser = _userRepository.GetById(myUser.ID);
                   
            var rolesForUser = UserManager.GetRoles(dbUser.AppUserId);
          

            if (rolesForUser.Contains("Admin"))
            {
                return RedirectToAction("Index");
            }

            return View(_userRepository.GetById(myUser.ID));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var dbUser = _userRepository.GetById(id);

            if (_userRepository.Delete(id))
            {
                UserManager.RemoveFromRole(dbUser.AppUserId, "User");
                return RedirectToAction("Index");
            }
               

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult ShowReport()
        {
            var users = _userRepository.GetAll().OrderByDescending(u => u.Tasks.Count);

            return View(users);
        }


        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }


}