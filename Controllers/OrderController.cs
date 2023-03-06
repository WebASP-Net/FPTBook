using Microsoft.AspNetCore.Mvc;
using FPTBook.Data;
using FPTBook.Models;


namespace FPTBook.Controllers
{
	public class OrderController : Controller
	{
		private readonly ApplicationDbContext _ord;

		public OrderController(ApplicationDbContext ord)
		{
			_ord = ord;
		}

		public IActionResult Index()
		{ 
			// Take list category
			IEnumerable<Order> lstOrd = _ord.Orders.ToList();
			return View(lstOrd);
		}

		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Order obj)
		{
			if (ModelState.IsValid)
			{
				_ord.Orders.Add(obj);
				_ord.SaveChanges();
				return RedirectToAction("Index");
			}
			else
			{
				return View(obj);
			}
		}

		public IActionResult Edit(int id)
		{
			Order obj = _ord.Orders.Find(id);
			if (obj == null)
			{
				return RedirectToAction("Index");
			}
			return View(obj);
			//return Json(obj);
		}

		[HttpPost]
		public IActionResult Edit(int id,Order obj)
		{
			if (ModelState.IsValid)
			{
				obj.Order_Id = id;
				_ord.Orders.Update(obj);
				_ord.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		public IActionResult Delete(int id)
		{
			Order obj = _ord.Orders.Find(id);
			if (obj != null)
			{
				_ord.Orders.Remove(obj);
				_ord.SaveChanges();
			}
			return RedirectToAction("Index");
		}
	}
}
