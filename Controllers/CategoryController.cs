using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FPTBook.Models;
using FPTBook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _ct;

		public CategoryController(ApplicationDbContext ct)
		{
			_ct = ct;
		}

		public IActionResult Index()
		{

			IEnumerable<Category> lstCat = _ct.Categories.ToList();
			return View(lstCat);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Category obj)
		{
			if (ModelState.IsValid)
			{
				_ct.Categories.Add(obj);
				_ct.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				return View(obj);
			}
		}

		public IActionResult Edit(int id)
		{
			Category obj = _ct.Categories.Find(id);
			if (obj == null)
			{
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		[HttpPost]
		public IActionResult Edit(int id, Category obj)
		{
			if (ModelState.IsValid)
			{
				obj.Cat_Id = id;
				_ct.Categories.Update(obj);
				_ct.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public IActionResult Delete(int id)
		{
			Category obj = _ct.Categories.Find(id);
			if (obj != null)
			{
				_ct.Categories.Remove(obj);
				_ct.SaveChanges();
			}
			return RedirectToAction("Index");
		}
	}
}