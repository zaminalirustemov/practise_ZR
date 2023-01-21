using Microsoft.AspNetCore.Mvc;

namespace Boutique_ZR.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
