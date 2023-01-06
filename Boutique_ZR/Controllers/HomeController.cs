using Boutique_ZR.Models;
using Boutique_ZR.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Boutique_ZR.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoutiqueDbContext _boutiqueDb;

        public HomeController(BoutiqueDbContext boutiqueDb)
        {
            _boutiqueDb = boutiqueDb;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                tag=_boutiqueDb.Tags.ToList(),
                Category=_boutiqueDb.Categories.ToList(),
                Product=_boutiqueDb.Products.Include(x=>x.Category).Include(x => x.Tag).ToList(),

            };
            return View(homeViewModel);
        }

        public IActionResult Detail(int id)
        {
            Product product = _boutiqueDb.Products.Include(x=>x.Category).Include(x=>x.Tag).FirstOrDefault(x=>x.Id==id);
            if (product is null) return View("Error");
            return View(product);
        }


    }
}