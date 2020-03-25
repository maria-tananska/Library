using Library.Data.Models;
using Library.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Web.ViewModels.Books
{
    public class BookViewModel : IMapFrom<Book>
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }

        public string Title { get; set; }

        public string AutorFirstName { get; set; }

        public string AutorLastName { get; set; }
    }
}
