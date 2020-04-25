namespace Library.Web.ViewModels.Admin.Author
{
    using Library.Data.Models;
    using Library.Services.Mapping;

    public class DeleteModel : IMapFrom<Autor>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
    }
}
