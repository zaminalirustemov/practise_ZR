using Boutique_ZR.Models;
using Boutique_ZR.Services;
using Boutique_ZR.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Boutique_ZR.Controllers
{
    public class HomeController : Controller
    {
        private readonly BoutiqueDbContext _boutiqueDb;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;

        public HomeController(BoutiqueDbContext boutiqueDb, UserManager<AppUser> userManager,IEmailSender emailSender)
        {
            _boutiqueDb = boutiqueDb;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public IActionResult Index()
        {
            HomeViewModel homeViewModel = new HomeViewModel
            {
                tag = _boutiqueDb.Tags.ToList(),
                Category = _boutiqueDb.Categories.ToList(),
                Product = _boutiqueDb.Products.Include(x => x.Category).Include(x => x.Tag).ToList(),

            };
            return View(homeViewModel);
        }

        public IActionResult Detail(int id)
        {
            Product product = _boutiqueDb.Products.Include(x => x.Category).Include(x => x.Tag).FirstOrDefault(x => x.Id == id);
            if (product is null) return View("Error");
            return View(product);
        }

        public async Task<IActionResult> AddTOBasket(int productId)
        {
            if (!_boutiqueDb.Products.Any(x => x.Id == productId)) return NotFound();

            List<CartItemViewModel> cartItems = new List<CartItemViewModel>();
            CartItemViewModel cartItem = null;
            AppUser member = null;
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                member = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            }
            string cartItemsStr = HttpContext.Request.Cookies["CartItems"];

            if (member == null)
            {

                if (cartItemsStr != null)
                {
                    cartItems = JsonConvert.DeserializeObject<List<CartItemViewModel>>(cartItemsStr);

                    cartItem = cartItems.FirstOrDefault(x => x.ProductId == productId);

                    if (cartItem is not null) cartItem.Count++;
                    else
                    {
                        cartItem = new CartItemViewModel
                        {
                            ProductId = productId,
                            Count = 1
                        };
                        cartItems.Add(cartItem);
                    }
                }
                else
                {
                    cartItem = new CartItemViewModel
                    {
                        ProductId = productId,
                        Count = 1
                    };
                    cartItems.Add(cartItem);
                }


                cartItemsStr = JsonConvert.SerializeObject(cartItems);

                HttpContext.Response.Cookies.Append("CartItems", cartItemsStr);
            }
            else
            {
                BasketItem memberBasketItem = _boutiqueDb.BasketItems.FirstOrDefault(x => x.AppUserId == member.Id && x.ProductId == productId);


                if (memberBasketItem is not null)
                {
                    memberBasketItem.Count++;
                }
                else
                {
                    memberBasketItem = new BasketItem
                    {
                        AppUserId = member.Id,
                        ProductId = productId,
                        Count = 1
                    };
                    _boutiqueDb.BasketItems.Add(memberBasketItem);
                }

                await _boutiqueDb.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GetCartItems()
        {
            List<CartItemViewModel> cartItems = new List<CartItemViewModel>();
            string cartItemsStr = HttpContext.Request.Cookies["CartItems"];

            if (cartItemsStr is not null)
            {
                cartItems = JsonConvert.DeserializeObject<List<CartItemViewModel>>(cartItemsStr);
            }


            return Json(cartItems);
        }


        public IActionResult SendMail()
        {
            _emailSender.Send("2g17rck@code.edu.az", "Bu bir test mailidir", "Tebrikler, mail ugurla gonderilib");
            return Content("Email gonderildi.");
        }
    }
}