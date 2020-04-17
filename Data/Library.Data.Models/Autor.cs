namespace Library.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Library.Data.Common.Models;

    public class Autor : BaseDeletableModel<int>
    {
        public Autor()
        {
            this.Books = new HashSet<Book>();
        }

        [Required]
        [MaxLength(20)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }

        public ICollection<Book> Books { get; set; }
    }
}
