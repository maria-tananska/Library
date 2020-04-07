﻿namespace Library.Web.ViewModels.Admin.Categories
{
    using System.ComponentModel.DataAnnotations;

    public class CreateViewModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        public string Img { get; set; }
    }
}
