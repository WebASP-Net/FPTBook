using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FPTBOOK.Data;
using FPTBOOK.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBOOK.Controllers
{
   
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CustomerController(ApplicationDbContext db)
        {
            _db = db;
        }
         public IActionResult Index()
        {
            // lay danh sach khavh hang tu dbcontext
            IEnumerable<Customer> ds = _db.Customers.ToList();
            return View(ds);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customer obj)
        {
            if(ModelState.IsValid){
            _db.Customers.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int id){
            Customer obj = _db.Customers.Find(id);
            if(obj == null){
                return RedirectToAction("Index");
            }
            return View(obj);
        }

         [HttpPost]
         public IActionResult Edit(int id, Customer obj)
         {
            if(ModelState.IsValid){
                obj.cus_id = id;
                _db.Customers.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
         }
    }
}