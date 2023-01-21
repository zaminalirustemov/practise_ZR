using Boutique_ZR.Models;
using Boutique_ZR.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Boutique_ZR.Controllers
{
    public class AccountController : Controller
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly BoutiqueDbContext _boutiqueDbContext;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager,BoutiqueDbContext boutiqueDbContext,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _boutiqueDbContext = boutiqueDbContext;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterViewModel)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = null;
            user = _boutiqueDbContext.AppUsers.FirstOrDefault(x => x.NormalizedUserName == memberRegisterViewModel.Username.ToUpper());
            if (user is not null)
            {
                ModelState.AddModelError("Username", "Already exist");
                return View();
            }
            user = _boutiqueDbContext.AppUsers.FirstOrDefault(x => x.NormalizedEmail == memberRegisterViewModel.Email.ToUpper());
            if (user is not null)
            {
                ModelState.AddModelError("Email", "Already exist");
                return View();
            }

            user = new AppUser
            {
                Fullname = memberRegisterViewModel.Fullname,
                UserName = memberRegisterViewModel.Username,
                Email = memberRegisterViewModel.Email,
            };

            var result = await _userManager.CreateAsync(user, memberRegisterViewModel.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            await _userManager.AddToRoleAsync(user, "Member");
            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("index", "home");

        }

        //login------------------------------------------------------------------------------------------
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(MemberLoginViewModel memberLoginVM)
        {
            if (!ModelState.IsValid) return View();
            AppUser user = await _userManager.FindByNameAsync(memberLoginVM.UserName);
            if (user is null)
            {
                ModelState.AddModelError("", "Username or password is false");
                return View();
            }

            var result = await _signInManager.PasswordSignInAsync(user, memberLoginVM.Password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is false");
                return View();
            }

            return RedirectToAction("index", "home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("login", "account");
        }






        //////CreateRole------------------------------------------------

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role1 = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role1);
        //    await _roleManager.CreateAsync(role2);
        //    await _roleManager.CreateAsync(role3);

        //    return Ok("Rollar yaradildi");
        //}

    }
}
