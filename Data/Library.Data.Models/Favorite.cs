namespace Library.Data.Models
{
    using Library.Data.Common.Models;

    public class Favorite : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int BookId { get; set; }

        public Book Book { get; set; }
    }
}
