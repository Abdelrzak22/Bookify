using Bookify.Web.Core.ViewModel;
using Bookify.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //TO DO: Make ViewModel 
            var Categories = _context.Categories;
            return View(Categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form",model);

            var category = new Category { Name = model.Name };
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
                return NotFound();
            var viewmodel = new CategoryViewModel
            {
                Id = id,
                Name = category.Name
            };
            return View("Form",viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CategoryViewModel model)
        {
            if (!ModelState.IsValid)
                return View("Form", model);
            var category = _context.Categories.Find(model.Id);
            category.Name = model.Name;
            category.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
