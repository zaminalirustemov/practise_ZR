using Boutique_ZR.Models;
using Microsoft.AspNetCore.Identity;

namespace Boutique_ZR.Services
{
    public class MemberLayoutService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public MemberLayoutService(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<AppUser> GetUser()
        {
            AppUser appUser = null;
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                appUser = await _userManager.FindByNameAsync(_httpContextAccessor.HttpContext.User.Identity.Name);
                return appUser;
            }

            return null;
        }
    }
}
