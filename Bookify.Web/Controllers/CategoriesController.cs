using AutoMapper;
using Bookify.Web.Core.ViewModel;
using Bookify.Web.Data;
using Bookify.Web.filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Web.Controllers
{
    public class CategoriesController : Controller
    {
        public readonly ApplicationDbContext _context;
        private readonly IMapper _mapp;

        public CategoriesController(ApplicationDbContext context,IMapper mapp)
        {
            _context = context;
            _mapp = mapp;
        }

        public IActionResult Index()
        {
            //TO DO: Make ViewModel 
            var Categories = _context.Categories.AsNoTracking().ToList();
            var data=_mapp.Map<IEnumerable<CategoriesViewModel>>(Categories);
            return View(data);
        }
        [HttpGet]
        [ajaxonly]
        public IActionResult Create()
        {
            return PartialView("_Form");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FormViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_Form", model);

            var category = _mapp.Map<Category>(model);
            _context.Add(category);
            _context.SaveChanges();

            return PartialView("_row",
                _mapp.Map<CategoriesViewModel>(category));
        }
        [HttpGet]
        [ajaxonly]
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
                return NotFound();
            var viewmodel = _mapp.Map<FormViewModel>(category);
            
            return PartialView("_Form",viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FormViewModel model)
        {
            if (!ModelState.IsValid)
                return PartialView("_Form", model);

            var category = _context.Categories.Find(model.Id);

            category = _mapp.Map(model, category);
            category.LastUpdatedOn = DateTime.Now;

            _context.SaveChanges();

            return PartialView("_row",
                _mapp.Map<CategoriesViewModel>(category));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var category = _context.Categories.Find(id);
            if (category is null)
                return NotFound();
            category.IsDeleted = !category.IsDeleted;
            category.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok(category.LastUpdatedOn.ToString());
        }

        public IActionResult AllowedItems(FormViewModel modal)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Name == modal.Name);
            var isAllowed = category is null || category.Id.Equals(modal.Id);

            return Json(isAllowed);
        }
    }
}
