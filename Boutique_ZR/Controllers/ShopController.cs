using Microsoft.AspNetCore.Mvc;

namespace Boutique_ZR.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
