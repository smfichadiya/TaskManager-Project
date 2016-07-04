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
    [Authorize(Roles = "Admin,User")]
    public class CustomersController : Controller
    {
        ICustomerRepository _customerRepository = new CustomerRepository();
        IProjectRepository _projectRepository = new ProjectRepository();

        // GET: Customers
       
        public ActionResult Index()
        {
            var customers = _customerRepository.GetAll();

            return View(customers);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (_customerRepository.Create(customer))
                    return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            return View(_customerRepository.GetById(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                if (_customerRepository.Update(customer))
                    return RedirectToAction("Index");
            }

            return View(customer);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(Customer customer)
        {
            return View(_customerRepository.GetById(customer.ID));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_customerRepository.Delete(id))
                return RedirectToAction("Index");

            return View();
        }

        public ActionResult Details(int? id)
        {
            var dbCustomer = _customerRepository.GetById((int)id);
            if(dbCustomer != null)
            {
                return View(dbCustomer);
            }
            return RedirectToAction("Index");
        }

    }
}