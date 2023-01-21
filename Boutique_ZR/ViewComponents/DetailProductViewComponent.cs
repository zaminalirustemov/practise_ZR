using Boutique_ZR.Models;
using Boutique_ZR.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boutique_ZR.ViewComponents
{
    public class DetailProductViewComponent:ViewComponent
    {
        private readonly BoutiqueDbContext _boutiqueDb;

        public DetailProductViewComponent(BoutiqueDbContext boutiqueDb)
        {
            _boutiqueDb = boutiqueDb;
        }

        public IViewComponentResult Invoke()
        {
            
            return View();
        }
    }
}
