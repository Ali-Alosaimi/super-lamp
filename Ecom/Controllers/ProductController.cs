using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Database.Context;
using Models.Database.Entities;
using Models.Request;
using Newtonsoft.Json;
using Models.Response.Auth;


namespace Ecom.Controllers
{
    public class ProductController : Controller
    {
        private DatabaseContext _context;
        public ProductController(DatabaseContext _context)
        {
            this._context = _context;
        }

        [Route("Products")]
        public async Task<IActionResult> Index()
        {
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return RedirectToAction("Login", "Auth");

            var products = await _context.products.ToListAsync().ConfigureAwait(false);
            return View(products);

        }
        [Route("/Product/ViewProducts")]
        public async Task<IActionResult> ViewProducts()
        {
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return RedirectToAction("Login", "Auth");

            var products = await _context.products.ToListAsync().ConfigureAwait(false);
            return View(products);

        }

        [Route("/Product/CardDetails")]
        public IActionResult CardDetails()
        {
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return RedirectToAction("Login", "Auth");

            return View();

        }

        [Route("/Product/AddProduct")]
        public async Task<IActionResult> AddProduct()
        {
            AddProductRequest request = new AddProductRequest();
            request.categories = await _context.categories.ToListAsync().ConfigureAwait(false);
            return View(request);
        }
        [Route("/Product/ViewOrders")]
        public async Task<IActionResult> ViewOrders()
        {
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return RedirectToAction("Login", "Auth");


            var user = JsonConvert.DeserializeObject<LoginResponse>(loginString);
            if (user.roleName.ToLower() == "admin")
            {
                var orders = await _context.Orders.ToListAsync();
                List<OrderDetails> OrderDetails = new List<OrderDetails>();
                foreach (var order in orders)
                {
                    var ordersDetails = await _context.OrderDetails.Where(x => x.orderID == order.orderID).ToListAsync();
                    foreach (var item in ordersDetails)
                    {
                        var product = await _context.products.Where(x => x.productId == item.productID).SingleOrDefaultAsync();
                        item.ProductName = product.productName;
                        item.ProductPrice = product.price;
                    }
                    order.OrderDetails = ordersDetails;
                }
                return View(orders);
            }
            else
            {
                var userId = long.Parse(HttpContext.Session.GetString("userId"));


                var orders = await _context.Orders.Where(x => x.createdBy == userId).ToListAsync();
                List<OrderDetails> OrderDetails = new List<OrderDetails>();
                foreach (var order in orders)
                {
                    var ordersDetails = await _context.OrderDetails.Where(x => x.orderID == order.orderID).ToListAsync();
                    foreach (var item in ordersDetails)
                    {
                        var product = await _context.products.Where(x => x.productId == item.productID).SingleOrDefaultAsync();
                        item.ProductName = product.productName;
                        item.ProductPrice = product.price;
                    }
                    order.OrderDetails = ordersDetails;
                }
                return View(orders);
            }
        }

        [Route("/Product/ViewCart")]
        public async Task<IActionResult> ViewCart()
        {
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return RedirectToAction("Login", "Auth");

            AddProductRequest request = new AddProductRequest();
            var userId = long.Parse(HttpContext.Session.GetString("userId"));
            var cart = await _context.ProductCart.Where(x => x.UserID == userId).ToListAsync();
            foreach (var item in cart)
            {
                var response = await _context.products.FirstOrDefaultAsync(x => x.productId == item.productID).ConfigureAwait(false);
                item.ProductName = response.productName;
                item.ProductPrice = response.price;
            }
            return View(cart);
        }
        

        [Route("/Product/AddProduct")]
        [HttpPost]
        public async Task<IActionResult> AddProduct(AddProductRequest request)
        {
            var fileName = "";
            if (request.image != null) {
                var extension = Path.GetExtension(request.image.FileName);
                string folderName = Path.Combine("wwwroot", "Resources", "image");
                fileName = Guid.NewGuid().ToString() + extension;
                var fullPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await request.image.CopyToAsync(stream);
                }
            }

            var product = new Products()
            {
                productName = request.productName,
                price = request.price,
                image = fileName,
                catId= request.catId
            };

            await _context.products.AddAsync(product).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var products = await _context.products.ToListAsync().ConfigureAwait(false);
            return RedirectToAction("ViewProducts", products);
        }
        [Route("/Product/DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(long productId)
        {
            //AddProductRequest request = new AddProductRequest();
            var response = await _context.products.FirstOrDefaultAsync(x => x.productId == productId).ConfigureAwait(false);
            _context.products.Remove(response);
            await _context.SaveChangesAsync();
            var products = await _context.products.ToListAsync().ConfigureAwait(false);
            return RedirectToAction("ViewProducts", products);
        }
        [Route("/Product/EditProduct")]
        public async Task<IActionResult> EditProduct(long productId)
        {
            var response = await _context.products.FirstOrDefaultAsync(x => x.productId == productId).ConfigureAwait(false);
            return RedirectToAction("ViewProductForEdit", response);
        }

        [Route("/Product/ViewProductForEdit")]
        public async Task<IActionResult> ViewProductForEdit(long productId)
        {
            var response = await _context.products.FirstOrDefaultAsync(x => x.productId == productId).ConfigureAwait(false);
            AddProductRequest request = new AddProductRequest();
            request.productId = response.productId;
            request.productName = response.productName;
            request.price = response.price;
            request.catId = response.catId;
            request.categories = await _context.categories.ToListAsync().ConfigureAwait(false);

            return View(request);
        }

        [Route("/Product/RemoveCartItem/{productId}")]
        public async Task<IActionResult> RemoveCartItem(long productId)
        {
            var userId = long.Parse(HttpContext.Session.GetString("userId"));
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return RedirectToAction("Login", "Auth");

            var cart = await _context.ProductCart.Where(x => x.UserID == userId && x.productID == productId).ToListAsync();
            _context.RemoveRange(cart);
            await _context.SaveChangesAsync().ConfigureAwait(false);

            

            AddProductRequest request = new AddProductRequest();
            cart = await _context.ProductCart.Where(x => x.UserID == userId).ToListAsync();
            foreach (var item in cart)
            {
                var response = await _context.products.FirstOrDefaultAsync(x => x.productId == item.productID).ConfigureAwait(false);
                item.ProductName = response.productName;
                item.ProductPrice = response.price;
            }

            TempData["SuccessMessage"] = "Removed from Cart";
            return View("ViewCart",cart);

        }

        [Route("/Product/UpdateProduct")]
        [HttpPost]
        public async Task<IActionResult> UpdateProduct(AddProductRequest request)
        {
            var response = await _context.products.FirstOrDefaultAsync(x => x.productId == request.productId).ConfigureAwait(false);

            var fileName = "";
            if (request.image != null)
            {
                var extension = Path.GetExtension(request.image.FileName);
                string folderName = Path.Combine("wwwroot", "Resources", "image");
                fileName = Guid.NewGuid().ToString() + extension;
                var fullPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    await request.image.CopyToAsync(stream);
                }
            }


            response.productName = request.productName;
            if (fileName != "") {
                response.image = fileName;
            }
            response.price = request.price;
            response.image = fileName;
            response.catId = request.catId;
            await _context.SaveChangesAsync().ConfigureAwait(false);
            var products = await _context.products.ToListAsync().ConfigureAwait(false);
            return RedirectToAction("ViewProducts", products);
        }

        [Route("/Product/AddToCart")]
        public async Task<IActionResult> AddToCart(long productId)
        {
            var userId = HttpContext.Session.GetString("userId");
            var product = await _context.ProductCart
                .Where(x => x.productID == productId && x.UserID == Convert.ToInt64(userId))
                .SingleOrDefaultAsync().ConfigureAwait(false);
            if (product != null)
            {
                product.qty = product.qty + 1;
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            else
            {
                ProductCart productCart = new ProductCart();
                productCart.productID = productId;
                productCart.qty = 1;
                productCart.UserID = long.Parse(userId);
                await _context.ProductCart.AddAsync(productCart);
                await _context.SaveChangesAsync();


            }
            TempData["SuccessMessage"] = "Added To Cart";
            return RedirectToAction("Index", "Home");
        }
        [Route("/Product/PlaceAnOrder")]
        public async Task<IActionResult> PlaceAnOrder()
        {
            var userId = long.Parse(HttpContext.Session.GetString("userId"));
            var cart = await _context.ProductCart.Where(x => x.UserID == userId).ToListAsync();
            if (cart.Count > 0) {

                Orders orders = new Orders();
                orders.createdBy = userId;
                await _context.Orders.AddAsync(orders);
                await _context.SaveChangesAsync();
                foreach (var item in cart)
                {
                    OrderDetails orderDetails = new OrderDetails();
                    orderDetails.productID = item.productID;
                    orderDetails.orderID = orders.orderID;
                    orderDetails.qty = item.qty;
                    await _context.OrderDetails.AddAsync(orderDetails);
                }
                _context.ProductCart.RemoveRange(cart);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Home");
        }
        
    }
}
