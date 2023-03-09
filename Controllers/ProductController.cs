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
             // Showing a product list, a button to be put in the cart
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



         /// Add products to cart
        [Route("addcart/{productid:int}", Name = "addcart")]
        public IActionResult AddToCart([FromRoute] int productid)
        {

            var product = _context.Products
          .Where(p => p.ProductId == productid)
          .FirstOrDefault();
            if (product == null)
                return NotFound("Không có sản phẩm");

            // Handling into cart ...
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.product.ProductId == productid);
            if (cartitem != null)
            {
                // Existed, increased by 1
                cartitem.quantity++;
            }
            else
            {
                //  Add new
                cart.Add(new CartItem() { quantity = 1, product = product });
            }

            // Save cart into session
            SaveCartSession(cart);
            // Switch to the Cart page
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
                // Existed, increased by 1
                cart.Remove(cartitem);
            }

            SaveCartSession(cart);
            return RedirectToAction(nameof(Cart));
        }

    [Route ("/updatecart", Name = "updatecart")]
    [HttpPost]
    public IActionResult UpdateCart ([FromForm] int productid, [FromForm] int quantity) {
        // Cập nhật Cart thay đổi số lượng quantity ...
        var cart = GetCartItems ();
        var cartitem = cart.Find (p => p.product.ProductId == productid);
        if (cartitem != null) {
            // Đã tồn tại, tăng thêm 1
            cartitem.quantity = quantity;
        }
        SaveCartSession (cart);
        // Trả về mã thành công (không có nội dung gì - chỉ để Ajax gọi)
        return Ok();
    }

        // Show shopping cart
        [Route("/cart", Name = "cart")]
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

    [Route("/checkout")]
    public IActionResult CheckOut()
    {
       return View(GetCartItems());
    }
    
     [Route("/checkout")]
        [HttpPost]
        public IActionResult CheckOut(string country,string first_name,string last_name,string address,string city,string phone_number,string email_address)
        {
            Order order = new Order()
            {
                country = country,
                cus_first_name = first_name,
                cus_last_name = last_name,
                cus_address = address,
                cus_city = city,
                cus_phone = phone_number,
                cus_email=email_address,
                OrderDate = DateTime.Now,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            int orderId = order.order_id;
            foreach (var cartItem in GetCartItems())
            {
                OrderDetail orderDetail = new OrderDetail()
                {
                    Order_Id = orderId,
                    Pro_Id = cartItem.product.ProductId,
                    Quantity = cartItem.quantity,
                };
                _context.OrderDetails.Add(orderDetail);
                _context.SaveChanges();
            }
            ClearCart();
            return Redirect("~/Home/Index");
        }
// Cart's JSON chain storage key
        public const string CARTKEY = "cart";

        // Get Cart from Session (Cartitem list)
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

        // Delete Cart from session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CARTKEY);
        }

        // Save Cart (Cartitem list) into session
        void SaveCartSession(List<CartItem> ls)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(ls);
            session.SetString(CARTKEY, jsoncart);
        }
    }
}