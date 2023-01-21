using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boutique_ZR.Areas.Manage.Controllers;

[Area("Manage")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}

