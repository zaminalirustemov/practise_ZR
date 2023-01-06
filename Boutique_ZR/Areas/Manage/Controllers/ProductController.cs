using Boutique_ZR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Boutique_ZR.Areas.Manage.Controllers;
[Area("Manage")]
public class ProductController : Controller
{
    private readonly BoutiqueDbContext _boutiqueDb;

    public ProductController(BoutiqueDbContext boutiqueDb)
    {
        _boutiqueDb = boutiqueDb;
    }
    //Read-----------------------------------
    public IActionResult Index()
    {
        List<Product> products = _boutiqueDb.Products.Include(x => x.Category).Include(x => x.Tag).ToList();
        return View(products);
    }

}

