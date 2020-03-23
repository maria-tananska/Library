namespace Library.Services.Data
{
    using System.Collections.Generic;

    public interface IBookService
    {
        IEnumerable<T> All<T>();
    }
}
