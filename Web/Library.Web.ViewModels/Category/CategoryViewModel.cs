namespace Library.Web.ViewModels.Category
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class CategoryViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Img { get; set; }
    }
}
