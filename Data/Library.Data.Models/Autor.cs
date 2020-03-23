namespace Library.Data.Models
{
    using Library.Data.Common.Models;
    using System.Collections.Generic;

    public class Autor : BaseDeletableModel<int>
    {
        public Autor()
        {
            this.Books = new HashSet<Book>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
