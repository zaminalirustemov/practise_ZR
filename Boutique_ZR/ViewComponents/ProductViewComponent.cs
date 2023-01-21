using Boutique_ZR.Models;
using Boutique_ZR.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boutique_ZR.ViewComponents
{
    public class ProductViewComponent :ViewComponent
    {
        private readonly BoutiqueDbContext _boutiqueDb;

        public ProductViewComponent(BoutiqueDbContext boutiqueDb)
        {
            _boutiqueDb = boutiqueDb;
        }

        public IViewComponentResult Invoke()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                tag = _boutiqueDb.Tags.ToList(),
                Category = _boutiqueDb.Categories.ToList(),
                Product = _boutiqueDb.Products.Include(x => x.Category).Include(x => x.Tag).ToList(),

            };
            return View(homeViewModel);
        }
    }
}
