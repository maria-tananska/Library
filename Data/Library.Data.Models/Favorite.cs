namespace Library.Data.Models
{
    using Library.Data.Common.Models;
    using System.Collections.Generic;

    public class Favorite : BaseDeletableModel<int>
    {
        public Favorite()
        {
            this.Books = new HashSet<Book>();
        }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
