namespace Library.Services.Data.Tests.Common
{
    using System;

    using Library.Data;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationDbContexInMemory
    {
        public static ApplicationDbContext InitializeContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
               .Options;

            return new ApplicationDbContext(options);
        }
    }
}
