using Microsoft.AspNetCore.Mvc;

namespace Bookify.Web.Core.ViewModel
{
    public class FormViewModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Remote("AllowedItems", "Categories", AdditionalFields = "Id",
    ErrorMessage = "This value already exists")]
        public string Name { get; set; } = null!;
    }
}
