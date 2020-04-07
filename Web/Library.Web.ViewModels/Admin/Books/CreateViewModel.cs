﻿namespace Library.Web.ViewModels.Admin.Books
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    using Library.Data.Models;
    using Library.Services.Mapping;

    public class CreateViewModel : IMapTo<Book>
    {
        public string Title { get; set; }

        public string ShortContent { get; set; }

        public string ImgUrl { get; set; }

        public string FileName { get; set; }

        public int Pages { get; set; }

        [DisplayName("Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoriesDropDownViewModel> Categories { get; set; }

        [DisplayName("Author")]
        public int AutorId { get; set; }

        public IEnumerable<AuthorsDropDownViewModel> Authors { get; set; }

    }
}
