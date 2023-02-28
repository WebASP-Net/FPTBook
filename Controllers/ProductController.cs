using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FPTBook.Models;
using FPTBook.Services;
using FPTBOOK.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FPTBook.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly IFileService _fileService;

        public ProductController(ILogger<ProductController> logger, ApplicationDbContext context, IFileService fileService)
        {
            _logger = logger;
            _context = context;
            _fileService = fileService;
        }

        public IActionResult GetAll()
        {
            return View();
        }

        public IActionResult Index()
        {
            IEnumerable<Product> lstPro = _context.Products.ToList();
            return View(lstPro);
            // Hiện thị danh sách sản phẩm, có nút chọn đưa vào giỏ hàng
            var products = _context.Products.ToList();
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product obj)
        {
            if (ModelState.IsValid)
            {
                var result = _fileService.SaveImage(obj.FileImage);
                if (result.Item1 == 1)
                {
                    var oldImage = obj.ProductImage;
                    obj.ProductImage = result.Item2;
                    var deleteResult = _fileService.DeleteImage(oldImage);
                }
                _context.Products.Add(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(obj);
            }
        }
        public IActionResult Edit(int id)
        {
            Product obj = _context.Products.Find(id);
            if (obj == null)
            {
                return RedirectToAction("Index");
            }
            return View(obj);
            //return Json(obj);
        }
        [HttpPost]
        public IActionResult Edit(int id, Product obj)
        {
            if (ModelState.IsValid)
            {
                var result = _fileService.SaveImage(obj.FileImage);
                if (result.Item1 == 1)
                {
                    var oldImage = obj.ProductImage;
                    obj.ProductImage = result.Item2;
                    var deleteResult = _fileService.DeleteImage(oldImage);
                    obj.ProductId = id;
                }
                _context.Products.Update(obj);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }       
            return View(obj);
        }
        public IActionResult Delete(int id)
        {
            Product obj = _context.Products.Find(id);
            if (obj != null)
            {
                _context.Products.Remove(obj);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}