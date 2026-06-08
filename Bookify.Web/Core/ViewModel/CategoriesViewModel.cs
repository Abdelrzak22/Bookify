namespace Bookify.Web.Core.ViewModel
{
    public class CategoriesViewModel
    {
        public int Id { get; set; }
      
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
