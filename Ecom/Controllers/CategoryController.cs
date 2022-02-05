using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.Database.Context;
using Models.Database.Entities;
using Models.Request;

namespace Ecom.Controllers
{
    public class CategoriesController : Controller
    {
        private DatabaseContext _context;
        public CategoriesController(DatabaseContext _context)
        {
            this._context = _context;
        }

        [Route("/Category")]
        public async Task<IActionResult> Index()
        {
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return View("/Auth/Login");
            var categories = await _context.categories.ToListAsync().ConfigureAwait(false);
            return View(categories);
        }

        [Route("/Category/AddCategory")]
        [HttpGet]
        public IActionResult AddCategory()
        {
            var loginString = HttpContext.Session.GetString("user");
            if (loginString == null) return RedirectToAction("Login", "Auth");

            AddCategoryRequest request = new AddCategoryRequest();
            return View(request);
        }

        [Route("/Category/AddCategory")]
        [HttpPost]
        public async Task<IActionResult> AddCategory(AddCategoryRequest request)
        {
            await _context.categories.AddAsync(new Categories()
            {
                catName = request.catName
            }).ConfigureAwait(false);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Category Added successfully";
            return View();
        }
    }
}
