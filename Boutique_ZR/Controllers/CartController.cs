using Boutique_ZR.Models;
using Boutique_ZR.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Boutique_ZR.Controllers
{
    public class CartController : Controller
    {
        private readonly BoutiqueDbContext _boutiqueDb;
        private readonly UserManager<AppUser> _userManager;

        public CartController(BoutiqueDbContext boutiqueDb, UserManager<AppUser> userManager)
        {
            _boutiqueDb = boutiqueDb;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            List<CartItemViewModel> cartItems = new List<CartItemViewModel>();
            List<CheckoutItemViewModel> checkoutItems = new List<CheckoutItemViewModel>();
            CheckoutItemViewModel checkout = null;
            List<BasketItem> memberbasketItems = null;
            string cartItemsStr = HttpContext.Request.Cookies["CartItems"];
            if (member == null)
            {

                if (cartItemsStr != null)
                {
                    cartItems = JsonConvert.DeserializeObject<List<CartItemViewModel>>(cartItemsStr);

                    foreach (var item in cartItems)
                    {
                        checkout = new CheckoutItemViewModel
                        {
                            Product = _boutiqueDb.Products.FirstOrDefault(x => x.Id == item.ProductId),
                            Count = item.Count
                        };
                        checkoutItems.Add(checkout);
                    }
                }
            }
            else
            {
                memberbasketItems = _boutiqueDb.BasketItems.Include(x => x.Product).Where(x => x.AppUserId == member.Id).ToList();

                foreach (var item in memberbasketItems)
                {
                    checkout = new CheckoutItemViewModel
                    {
                        Product = item.Product,
                        Count = item.Count
                    };
                    checkoutItems.Add(checkout);
                }

            }


            return View(checkoutItems);
        }
        public async Task<IActionResult> DeleteCartItems()
        {
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            List<CartItemViewModel> cartItems = new List<CartItemViewModel>();
            List<CheckoutItemViewModel> checkoutItems = new List<CheckoutItemViewModel>();

            if (member == null)
            {
                HttpContext.Response.Cookies.Delete("CartItems");
            }
            else
            {
                List<BasketItem> basketItems = _boutiqueDb.BasketItems.Where(x => x.AppUserId == member.Id).ToList();


                _boutiqueDb.BasketItems.RemoveRange(basketItems);
                _boutiqueDb.SaveChanges();


            }


            return RedirectToAction(nameof(Index));
        }
    }
}
