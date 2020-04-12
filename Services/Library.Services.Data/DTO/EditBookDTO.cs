namespace Library.Services.Data.DTO
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class EditBookDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string ShortContent { get; set; }

        public string Img { get; set; }

        public string FileName { get; set; }

        public int Pages { get; set; }

        public int CategoryId { get; set; }

        public int AutorId { get; set; }
    }
}
