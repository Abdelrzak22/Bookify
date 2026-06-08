using Microsoft.EntityFrameworkCore;

namespace Bookify.Web.Core.Models
{
    [Index(nameof(AuthorId),nameof(Title), IsUnique =true)]
    public class Book:BaseModel
    {
        public int Id { get; set; }
        [MaxLength(500)]
        public string Title { get; set; } = null!;
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;

        [MaxLength(200)]

        public string Publisher { get; set; } = null!;
        public DateTime PublishingDate { get; set; }
        [MaxLength(50)]
        public string Hall { get; set; }=null!;
        public bool IsAvailbleForRental { get; set; }

        public ICollection<BookCategories> Categories { get; set; } = new List<BookCategories>();
    }
}
