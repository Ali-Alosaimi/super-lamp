using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ecom.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Models.Database.Context;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Response.Products;

namespace Ecom.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseContext _context;
        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger,DatabaseContext _context)
        {
            _logger = logger;
            this._context = _context;
        }

        [Route("/Home")]
        public async Task<IActionResult> Index()
        {
            var loginString = HttpContext.Session.GetString("user");
            if(loginString != null)
            {

                var products = await (
                        from prods in _context.products
                        join cats in _context.categories
                        on prods.catId equals cats.catId

                        select new ProductsResponse()
                        {
                            productId = prods.productId,
                            productName = prods.productName,
                            price = prods.price,
                            image = prods.image,
                            catId = cats.catId,
                            catName = cats.catName
                        })
                        .ToListAsync()
                        .ConfigureAwait(false);
                return View(products);
                
            }
            
            else
                return RedirectToAction("Login", "Auth"); 
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
