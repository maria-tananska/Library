namespace Library.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class DashboardController : AdministrationController
    {
       public IActionResult Index()
       {
            return this.View();
       }
    }
}
