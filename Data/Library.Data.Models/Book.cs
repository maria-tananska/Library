namespace Library.Data.Models
{
    using Library.Data.Common.Models;

    public class Book : BaseDeletableModel<int>
    {
        public string Title { get; set; }

        public string ShortContent { get; set; }

        public string ImgUrl { get; set; }

        public string FileName { get; set; }

        public int Pages { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int AutorId { get; set; }

        public Autor Autor { get; set; }
    }
}
