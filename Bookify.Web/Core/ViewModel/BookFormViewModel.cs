using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UoN.ExpressiveAnnotations.NetCore.DependencyInjection;

using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Bookify.Web.Core.ViewModel
{
    public class BookFormViewModel
    {

        public int Id { get; set; }
        [MaxLength(500,ErrorMessage ="this Title can not be above 500 ")]
        [Required(ErrorMessage ="the title is required")]
        [Remote("AllowedItems", "Books",
    AdditionalFields = nameof(Id) + "," + nameof(AuthorId),
    ErrorMessage = "This book already exists for the selected author.")]
        public string Title { get; set; } = null!;


        [Display(Name ="Author")]
        [Required(ErrorMessage = "the title is author")]
        [Remote("AllowedItems", "Books",
    AdditionalFields = nameof(Id) + "," + nameof(Title),
    ErrorMessage = "This book already exists for the selected author.")]
        public int AuthorId { get; set; }
        public IEnumerable<SelectListItem>? Author { get; set; }

        [Required(ErrorMessage = "the title is Description")]

        public string Description { get; set; } = null!;
        [Display(Name ="Image")]
        public IFormFile? Image { get; set; } 
        public string? ImageUrl { get; set; }
        public string? ImageThumbnailUrl { get; set; }

        [MaxLength(200, ErrorMessage = "this Publisher Name can not be above 200 ")]

        [Required(ErrorMessage ="the title is publisher")]

        public string Publisher { get; set; } = null!;
        [Display(Name = "Publishing Date")]

        [AssertThat("PublishingDate <= Today()")]
        public DateTime PublishingDate { get; set; } = DateTime.Now;
        [MaxLength(50, ErrorMessage = "this Hall Name can not be above 50 ")]
        public string Hall { get; set; } = null!;

        [Display(Name = "Is Availble For Rental")]

        public bool IsAvailbleForRental { get; set; }

        [Display(Name ="Categories")]
        public IList<int> SelectedCategories { get; set; } = new List<int>();
        public IEnumerable<SelectListItem>? Categories { get; set; }

    }
}
