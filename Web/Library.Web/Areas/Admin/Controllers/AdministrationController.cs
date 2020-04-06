﻿namespace Library.Web.Areas.Administration.Controllers
{
    using Library.Common;
    using Library.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Admin")]
    public class AdministrationController : BaseController
    {
    }
}