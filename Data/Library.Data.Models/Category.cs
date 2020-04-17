namespace Library.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Library.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public string Img { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
