namespace Bookify.Web.Core.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; } = null!;
    }
}
