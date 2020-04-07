namespace Library.Web.ViewModels.Admin.Books
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class CategoriesDropDownViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
