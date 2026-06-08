using AutoMapper;
using Bookify.Web.Core.ViewModel;
using Bookify.Web.Data;
using Bookify.Web.filter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookify.Web.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AuthorsController(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public IActionResult Index()
        {
            //TO DO: Make ViewModel 
            var authors = _context.Authors.AsNoTracking().ToList();
            var data = _mapper.Map<IEnumerable<AuthorViewModel>>(authors);
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
        public IActionResult Create(AuthorFormViemModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var author = _mapper.Map<Author>(model);
            _context.Add(author);
            _context.SaveChanges();
            var data = _mapper.Map<AuthorViewModel>(author);

            return PartialView("_Authorrow", data);
        }
        [HttpGet]
        [ajaxonly]
        public IActionResult Edit(int id)
        {
            var author = _context.Authors.Find(id);
            if (author is null)
                return NotFound();
            var viewmodel = _mapper.Map<AuthorFormViemModel>(author);

            return PartialView("_Form", viewmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AuthorFormViemModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var author = _context.Authors.Find(model.Id);
            author = _mapper.Map(model, author);
            author.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();


            var data = _mapper.Map<AuthorViewModel>(author);
            return PartialView("_Authorrow", data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ToggleStatus(int id)
        {
            var author = _context.Authors.Find(id);
            if (author is null)
                return NotFound();
            author.IsDeleted = !author.IsDeleted;
            author.LastUpdatedOn = DateTime.Now;
            _context.SaveChanges();
            return Ok(author.LastUpdatedOn.ToString());
        }

        public IActionResult AllowedItems(AuthorViewModel modal)
        {
            var author = _context.Authors.SingleOrDefault(c => c.Name == modal.Name);
            var isAllowed = author is null || author.Id.Equals(modal.Id);

            return Json(isAllowed);
        }
    }
}
