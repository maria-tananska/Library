namespace Library.Data.Models
{
    using System.Collections.Generic;

    using Library.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }

        public string Name { get; set; }

        public string Img { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
