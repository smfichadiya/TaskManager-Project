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
    public class CustomersController : Controller
    {
            //something
        ICustomerRepository _customerRepository = new CustomerRepository();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _customerRepository.GetAll();

            return View(customers);
        }

        public ActionResult Create()
        {
            return View();
        }

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

        public ActionResult Edit(int id)
        {
            return View(_customerRepository.GetById(id));
        }

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


        public ActionResult Delete(Customer customer)
        {
            return View(_customerRepository.GetById(customer.ID));
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            if (_customerRepository.Delete(id))
                return RedirectToAction("Index");

            return View();
        }
    }
}