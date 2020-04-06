namespace Library.Web.Areas.Administration.Controllers
{
    using Library.Services.Data;
    using Library.Web.ViewModels.Administration.Dashboard;

    using Microsoft.AspNetCore.Mvc;

    [Area("Admin")]
    public class DashboardController : Controller
    {
       public IActionResult Index()
       {
            return this.View();
       }
    }
}
