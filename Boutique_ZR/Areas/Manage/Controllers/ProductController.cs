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
        List<Product> products = _boutiqueDb.Products
                                            .Include(x => x.Category)
                                            .Include(x => x.Tag)
                                            .ToList();
        return View(products);
    }

    //Cread-----------------------------------
    public IActionResult Create()
    {
        ViewBag.Category = _boutiqueDb.Categories.ToList();
        ViewBag.Tag = _boutiqueDb.Tags.ToList();
        return View();
    }
    [HttpPost]
    public IActionResult Create(Product product)
    {
        ViewBag.Category = _boutiqueDb.Categories.ToList();
        ViewBag.Tag = _boutiqueDb.Tags.ToList();

        if (product.ImageFile != null)
        {
            if (product.ImageFile.ContentType != "image/jpeg" && product.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "Image format must be jpg and png!");
                return View();
            }

            if (product.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "Image must be less than 2 MB!");
                return View();
            }
            product.ImageUrl = product.ImageFile.FileName ;

        }

        _boutiqueDb.Products.Add(product);
        _boutiqueDb.SaveChanges();
        return RedirectToAction(nameof(Index));
    }


    //Edit-------------------------------------

    public IActionResult Edit(int id)
    {
        ViewBag.Category = _boutiqueDb.Categories.ToList();
        ViewBag.Tag = _boutiqueDb.Tags.ToList();
        Product product = _boutiqueDb.Products.FirstOrDefault(x=>x.Id==id);

        if (product is null) return View("Error");

        return View(product);
    }

    [HttpPost]
    public IActionResult Edit(Product newProduct)
    {
        ViewBag.Category = _boutiqueDb.Categories.ToList();
        ViewBag.Tag = _boutiqueDb.Tags.ToList();
        Product exsistProduct = _boutiqueDb.Products.FirstOrDefault(x => x.Id ==newProduct.Id );

        if (exsistProduct is null) return View("Error");

        exsistProduct.TagId = newProduct.TagId;
        exsistProduct.CategoryId = newProduct.CategoryId;
        exsistProduct.Name = newProduct.Name;
        exsistProduct.Description = newProduct.Description;
        exsistProduct.CostPrice = newProduct.CostPrice;
        exsistProduct.DiscountPrice = newProduct.DiscountPrice;
        exsistProduct.SalePrice = newProduct.SalePrice;
        exsistProduct.SKU = newProduct.SKU;

        _boutiqueDb.SaveChanges();

        return RedirectToAction("Index");
    }

}

