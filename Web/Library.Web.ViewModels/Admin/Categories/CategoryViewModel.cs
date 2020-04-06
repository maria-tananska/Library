namespace Library.Web.ViewModels.Admin.Categories
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
