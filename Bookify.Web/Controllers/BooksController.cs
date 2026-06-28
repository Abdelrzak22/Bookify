using AutoMapper;
using Bookify.Web.Core.ViewModel;
using Bookify.Web.Data;
using Bookify.Web.setting;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System.Threading.Tasks;

namespace Bookify.Web.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly Cloudinary _Cloudinary;
        private readonly IWebHostEnvironment _webHost;
        private List<string> _AllowedExtenstion = new() { ".jpg", ".png", ".jpeg" };
        private int _maxsize = 2097152;
        public BooksController(ApplicationDbContext context,IMapper Mapper,IWebHostEnvironment webHost,IOptions<CloudinarySettings> cloudinary)
        {
            _context = context;
            _mapper = Mapper;
            _webHost = webHost;
            Account account = new()
            {
                Cloud = cloudinary.Value.CloudName,
                ApiKey = cloudinary.Value.APIkey,
                ApiSecret = cloudinary.Value.APISecret

            };
            _Cloudinary=new Cloudinary(account);    
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {

            var viewmodel = PopulateViewModel();
            return View("Form",viewmodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", PopulateViewModel(model));
            }
            var book = _mapper.Map<Book>(model);
            if(model.Image is not null)
            {
                var Extention = Path.GetExtension(model.Image.FileName);
                if (!_AllowedExtenstion.Contains(Extention))
                {
                    ModelState.AddModelError(nameof(model.ImageUrl), errorMessage: "the only allowed Extenstions is .jpg , .png ,.jpeg");
                    return View("Form", PopulateViewModel(model));

                }

                if(model.Image.Length>_maxsize)
                {
                    ModelState.AddModelError(nameof(model.ImageUrl), errorMessage: "the max size is 2 MB");
                    return View("Form", PopulateViewModel(model));
                }

                var imageName = $"{Guid.NewGuid()}{Extention}";
                var path = Path.Combine($"{_webHost.WebRootPath}/Images/Books", imageName);
                var thumbpath = Path.Combine($"{_webHost.WebRootPath}/Images/Books/thumb", imageName);
                using var Stream = System.IO.File.Create(path);
                await model.Image.CopyToAsync(Stream);
                Stream.Dispose();

                //var stream = model.Image.OpenReadStream();
                //var imageparams = new ImageUploadParams
                //{
                //    File = new FileDescription(imageName, stream),
                //    UseFilename=true                };
                //var result = await _Cloudinary.UploadAsync(imageparams);
                //book.ImageUrl = result.SecureUrl.ToString();
                //book.ImageThumbnailUrl = thumbnial(book.ImageUrl);

                book.ImageUrl = $"/Images/Books/{imageName}";
                book.ImageThumbnailUrl = $"/Images/Books/thumb/{imageName}";
                using var imge = Image.Load(model.Image.OpenReadStream());
                var ratio =(float) imge.Width / 200;
                var height = imge.Height / ratio;
                imge.Mutate(i => i.Resize(width: 200, height:(int) height));
                imge.Save(thumbpath);
               
                
            }
            foreach (var Category in model.SelectedCategories)
                book.Categories.Add(new BookCategories { CategoryId = Category });
            _context.Add(book);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            
        }

        public IActionResult Edit(int id)
        {

            var book = _context.Books.Include(b=>b.Categories).SingleOrDefault(x=>x.Id == id);  
            if (book is null)
                return NotFound();

            var model = _mapper.Map<BookFormViewModel>(book);
            var viewmodel = PopulateViewModel(model);
            viewmodel.SelectedCategories=book.Categories.Select(b=>b.CategoryId).ToList();
            return View("Form",viewmodel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(BookFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", PopulateViewModel(model));
            }
            var book = _context.Books.Include(b => b.Categories).SingleOrDefault(x => x.Id == model.Id);
            if (book is null)
                return NotFound();

            string imagepublicid = null;
            if (model.Image is not null)
            {
                if (!string.IsNullOrEmpty(book.ImageUrl))
                {
                    var OldPath = $"{_webHost.WebRootPath}{book.ImageUrl}";
                    var OldThumbmailPath = $"{_webHost.WebRootPath}{book.ImageUrl}";
                    if (System.IO.File.Exists(OldPath))
                        System.IO.File.Delete(OldPath);
                    if (System.IO.File.Exists(OldThumbmailPath))
                        System.IO.File.Delete(OldThumbmailPath);
                }
                var Extention = Path.GetExtension(model.Image.FileName);
                if (!_AllowedExtenstion.Contains(Extention))
                {
                    ModelState.AddModelError(nameof(model.ImageUrl), errorMessage: "the only allowed Extenstions is .jpg , .png ,.jpeg");
                    return View("Form", PopulateViewModel(model));

                }

                if (model.Image.Length > _maxsize)
                {
                    ModelState.AddModelError(nameof(model.ImageUrl), errorMessage: "the max size is 2 MB");
                    return View("Form", PopulateViewModel(model));
                }

                var imageName = $"{Guid.NewGuid()}{Extention}";

                // var path = Path.Combine($"{_webHost.WebRootPath}/Images/Books", imageName);
                // using var Stream = System.IO.File.Create(path);
                //await  model.Image.CopyToAsync(Stream);
                // model.ImageUrl = imageName;


                //var stream = model.Image.OpenReadStream();
                //var imageparams = new ImageUploadParams
                //{
                //    File = new FileDescription(imageName, stream),
                //    UseFilename = true
                //};
                //var result = await _Cloudinary.UploadAsync(imageparams);
                //model.ImageUrl = result.SecureUrl.ToString();
                //imagepublicid = result.PublicId;





                
                var path = Path.Combine($"{_webHost.WebRootPath}/Images/Books", imageName);
                var thumbpath = Path.Combine($"{_webHost.WebRootPath}/Images/Books/thumb", imageName);
                using var Stream = System.IO.File.Create(path);
                await model.Image.CopyToAsync(Stream);
                Stream.Dispose();

               

                model.ImageUrl = $"/Images/Books/{imageName}";
                model.ImageThumbnailUrl = $"/Images/Books/thumb/{imageName}";
                using var imge = Image.Load(model.Image.OpenReadStream());
                var ratio = (float)imge.Width / 200;
                var height = imge.Height / ratio;
                imge.Mutate(i => i.Resize(width: 200, height: (int)height));
                imge.Save(thumbpath);

            }
            else if ( !string.IsNullOrEmpty(book.ImageUrl))
                model.ImageUrl = book.ImageUrl;

                book = _mapper.Map(model, book);
            book.LastUpdatedOn = DateTime.Now;
            //book.ImageThumbnailUrl = thumbnial(book.ImageUrl!);
            //book.ImagePublicId = imagepublicid;

            foreach (var Category in model.SelectedCategories)
                book.Categories.Add(new BookCategories { CategoryId = Category });
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }


        public IActionResult AllowedItems(BookFormViewModel modal)
        {
            var book = _context.Books.SingleOrDefault(c => c.Title == modal.Title&&c.AuthorId==modal.AuthorId);
            var isAllowed = book is null || book.Id.Equals(modal.Id);

            return Json(isAllowed);
        }
        private BookFormViewModel PopulateViewModel(BookFormViewModel? model = null)
        {
            BookFormViewModel ViewModel = model is null ? new BookFormViewModel(): model;

            var categories = _context.Categories.Where(e => !e.IsDeleted).OrderBy(x => x.Name).ToList();
            var authors = _context.Authors.Where(e => !e.IsDeleted).OrderBy(x => x.Name).ToList();

            ViewModel.Author = _mapper.Map<IEnumerable<SelectListItem>>(authors);
            ViewModel.Categories = _mapper.Map<IEnumerable<SelectListItem>>(categories);

            return ViewModel;

        }

        private string thumbnial(string url)
        {

            var seperator = "image/upload/";
            var urlParts = url.Split(seperator);
            var thumb = $"{urlParts[0]}{seperator}c_auto,h_200,w_200/{urlParts[1]}";
            return thumb;
        }
    }
}
