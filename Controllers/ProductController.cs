using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FPTBook.Models;
using FPTBook.Services;
using FPTBook.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
            ViewData["Cat_Id"] = new SelectList(_context.Categories, "Cat_Id", "Cat_Name");
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
                ViewData["Cat_Id"] = new SelectList(_context.Categories, "Cat_Id", "Cat_Id", obj.Cat_Id);
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
            ViewData["Cat_Id"] = new SelectList(_context.Categories, "Cat_Id", "Cat_Name");
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
            ViewData["Cat_Id"] = new SelectList(_context.Categories, "Cat_Id", "Cat_Id", obj.Cat_Id);    
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



         /// Thêm sản phẩm vào cart
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.Products
          .Where(p => p.ProductId == productid)
          .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Xử lý đưa vào Cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity++;
            }
            else
            {
                //  Thêm mới
                cart.Add(new CartItem() { quantity = 1, product = product });
            }

            // Lưu cart vào Session
            SaveCartSession(cart);
            // Chuyển đến trang hiện thị Cart
            return RedirectToAction(nameof(Cart));
        }
        /// xóa item trong cart
        [Route("/removecart/{productid:int}", Name = "removecart")]
        public IActionResult RemoveCart([FromRoute] int productid)
        {

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

        /// Cập nhật
        [Route("/updatecart", Name = "updatecart")]
        [HttpPost]
        public IActionResult UpdateCart([FromForm] int productid, [FromForm] int quantity)
        {
            // Cập nhật Cart thay đổi số lượng quantity ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Đã tồn tại, tăng thêm 1
                cartitem.quantity = quantity;
            }
            SaveCartSession(cart);
            // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
            return Ok();
        }


        // Hiện thị giỏ hàng
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        [Route("/checkout")]
        public IActionResult CheckOut()
        {
            // Xử lý khi đặt hàng
            return View();
        }
        // Key lưu chuỗi json của Cart
        public const string CARTKEY = "cart";

        // Lấy cart từ Session (danh sách CartItem)
        List<CartItem> GetCartItems()
        {

            var session = HttpContext.Session;
            string jsoncart = session.GetString(CARTKEY);
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);
            }
            return new List<CartItem>();
        }

        // Xóa cart khỏi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Lưu Cart (Danh sách CartItem) vào session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }

    }
}