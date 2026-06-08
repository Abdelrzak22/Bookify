

using Microsoft.EntityFrameworkCore;

namespace Bookify.Web.Core.Models
{

    [Index(nameof(Name),IsUnique =true)]
    public class Category:BaseModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<BookCategories> Books { get; set; } = new List<BookCategories>();


    }
}
